using UnityEngine;
using System.Collections;

public class Betasteering : MonoBehaviour {
	public bool debugmode;
	
	// hold these so we can make the wheels do what we want
	
	public WheelCollider frontright;
	public WheelCollider frontleft;
	public WheelCollider backright;
	public WheelCollider backleft;
	public float maxSpeed;
	
	
	// hold these so we can make the wheels rotate and steer
	public Transform wheelFL;
	public Transform wheelFR;
	public Transform wheelRL;
	public Transform wheelRR;
	
	public float speedmultiplier; // acceleration rate
	float steer; // the way we are turning
	
	public float steer_max = 10;// max turning degree
	
	float forward; // which way we a going 
	float brake; // how we stop
	float avgrpm; // tell us how fast we are going
	string rpmtext;// for the GUI
	
	// Use this for initialization
	void Start () 
	{
		
		rpmtext = avgrpm.ToString();
	
	}
	
	
	void FixedUpdate () {
		avgrpm =(backleft.rpm+backright.rpm)/2;
		//avgrpm = avgrpm/100;
		
		steer = Mathf.Clamp(Input.GetAxis("Horizontal"),-1f, 1f);
		forward = Mathf.Clamp(Input.GetAxis("Forward"), -1f, 1f);
		frontleft.steerAngle= steer*(speedmultiplier/5);
		frontright.steerAngle= steer*(speedmultiplier/5);
		
		
	if(!backleft.isGrounded && !backright.isGrounded ) // In the air
		{
			backleft.motorTorque=0;
			backright.motorTorque=0;		
			
			
		}
	else
		{
		if(forward>0 && avgrpm<maxSpeed )// make it go
		{
		backleft.motorTorque = forward*speedmultiplier;
		backright.motorTorque = forward*speedmultiplier;
		
			
		}

		
		if (Input.GetKeyDown("s")) // Slow Down
			
		{
			if(avgrpm>10)
			{
			backleft.brakeTorque+=30;
			backright.brakeTorque+=30;
			}
			else
			{
				backleft.brakeTorque=0;
			backright.brakeTorque=0;
		backleft.motorTorque = forward*speedmultiplier;
		backright.motorTorque = forward*speedmultiplier;
				
			}
		}
			
			
		}
		
		if (Input.GetKeyUp("s"))// Release the brake
		{
			backleft.brakeTorque=0;
			backright.brakeTorque=0;
			
		}


		
			if(Input.GetKeyDown(KeyCode.Space))//Reset car at current location
		{
			Vector3 temp = transform.forward;
 			transform.rotation = new Quaternion(0,temp.y,0,0);
			transform.forward=temp;
			transform.position = new Vector3 (transform.position.x,transform.position.y+4,transform.position.z);	
		}
		
		
		//Animation
		
		//Steering
		wheelFL.localEulerAngles = new Vector3(90, steer_max * steer * 1.5f, -90);
		wheelFR.localEulerAngles = new Vector3(90, steer_max * steer * 1.5f, 90);
		
		//Rotate the Wheels
		//Inverse the animation for the wheels on the right side
		
		wheelFL.Rotate(0f, frontleft.rpm * 4f * Time.fixedTime, 0f);
		wheelFR.Rotate(0f, frontright.rpm * -4f * Time.fixedTime, 0f);
		wheelRL.Rotate(0f, backleft.rpm * 6f * Time.deltaTime, 0f);
		wheelRR.Rotate(0f, backright.rpm * -6f * Time.deltaTime, 0f);
	
	}
	
	void OnGUI()
	{
		if(debugmode)
		{
		GUI.Box(new Rect(0,100,200,25), backleft.rpm.ToString());
		GUI.Box(new Rect(0,150,200,25), backleft.brakeTorque.ToString());
		GUI.Box(new Rect(0,200,200,25), forward.ToString());
		GUI.Box(new Rect(0,300,200,25), steer.ToString());
		GUI.Box(new Rect(0,350,200,25), transform.up.ToString());
		}
		
	}
}
