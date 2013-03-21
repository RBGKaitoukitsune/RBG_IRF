using UnityEngine;
using System.Collections;

public class VehicleControlPhysics : MonoBehaviour {
	
	public WheelCollider frontWheel1;
	public WheelCollider frontWheel2;
	public WheelCollider rearWheel1;
	public WheelCollider rearWheel2;
	
	public Transform wheelFL;
	public Transform wheelFR;
	public Transform wheelRL;
	public Transform wheelRR;
	
	public float steer_max = 10;
	public float motor_max = 20;
	public float brake_max = 150;
	public AudioClip EngineSound;
	public float EngineSoundVolume = 0.5f;
	
	private AudioSource EngineAudio;
	
	private float steer = 0;
	private float forward = 0;
	private float back = 0;
	private float motor = 0;
	private float brake = 0;
	private bool reverse = false;
	private float speed = 0;
	
	// Initialization
	void Start () {
		
		rigidbody.centerOfMass = new Vector3(0f, -0.05f, 0f);
	
		EngineAudio = gameObject.AddComponent<AudioSource>();
		EngineAudio.clip = EngineSound;
		EngineAudio.loop = true;
		EngineAudio.playOnAwake = true;
		EngineAudio.volume = EngineSoundVolume;
	}
	
	void Update () {

//		float carSpeed = rigidbody.velocity.magnitude;
//		float carSpeedFactor = Mathf.Clamp01(carSpeed / 10);
//		EngineAudio.pitch = Mathf.Abs(carSpeedFactor);//.Lerp(0, EngineSoundVolume, carSpeedFactor);
		
//		if(Input.GetKeyDown(KeyCode.UpArrow)){
//		
//			EngineAudio.Play();
//			float carSpeed = rigidbody.velocity.sqrMagnitude;
//			float carSpeedFactor = Mathf.Clamp01(carSpeed / 10);
//			EngineAudio.pitch = Mathf.Abs(carSpeedFactor / motor_max) + 1.0f;
//		}
//		
//		if(Input.GetKeyUp(KeyCode.UpArrow)){
//		
//			EngineAudio.Stop();
//			
//		}
		
	}
	
	void FixedUpdate () {
	
		speed = rigidbody.velocity.sqrMagnitude;
		steer = Mathf.Clamp(Input.GetAxis("Horizontal"),-1f, 1f);
		forward = Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f);
		back = -1f * Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 0f);
		
		if(speed <= 1){
			if(back > 0){ 
				reverse = true;
			}
			else if(forward > 0){

//				float carSpeed = rigidbody.velocity.sqrMagnitude;
//				float carSpeedFactor = Mathf.Clamp01(carSpeed / 10);
//				EngineAudio.pitch = Mathf.Abs(carSpeedFactor / motor_max) + 1.0f;
				//EngineAudio.pitch = Mathf.Abs(motor / motor_max) + 1.0 ;
				reverse = false;
			}
			//if(forward > 0) { reverse = false; }
		}
		
		if(reverse) {
			motor = -1 * back;
			brake = forward;
			EngineAudio.pitch = Mathf.Abs(motor / motor_max) + 1.0f ;
		}
		else{
			motor = forward;
			brake = back;
			EngineAudio.pitch = Mathf.Abs(motor / motor_max) + 1.0f ;
		}
		
		// Torque for vehicle
		rearWheel1.motorTorque = motor_max * motor;
		rearWheel2.motorTorque = motor_max * motor;
		rearWheel1.brakeTorque = brake_max * brake;
		rearWheel2.brakeTorque = brake_max * brake;
		
		// Steering
		frontWheel1.steerAngle = steer_max * steer;
		frontWheel2.steerAngle = steer_max * steer;
		wheelFL.localEulerAngles = new Vector3(90, steer_max * steer * 1.5f, -90);
		wheelFR.localEulerAngles = new Vector3(90, steer_max * steer * 1.5f, 90);
		
		// Due to wheel orientations, the direction of rotation has to be 
		// reversed for the right-hand wheels.
		wheelFL.Rotate(0f, frontWheel1.rpm * 4f * Time.fixedTime, 0f);
		wheelFR.Rotate(0f, frontWheel2.rpm * -4f * Time.fixedTime, 0f);
		wheelRL.Rotate(0f, rearWheel1.rpm * 6f * Time.deltaTime, 0f);
		wheelRR.Rotate(0f, rearWheel2.rpm * -6f * Time.deltaTime, 0f);
	}
	
		// Speed
		void OnGUI(){
			GUI.Box(new Rect(0, 100, 200, 50),"km/h: "+rigidbody.velocity.magnitude * 3.6f);
		}
}
