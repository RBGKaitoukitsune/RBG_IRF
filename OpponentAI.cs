using UnityEngine;
using System.Collections;

public class OpponentAI : MonoBehaviour {
	
	//float maxSpeed = 5;
	//float maxForce = 5;
	//float vol = 5;
//	Vector3 steer;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject wayPt = GameObject.Find("Target");
		Vector3 newDir = (wayPt.transform.position - rigidbody.position).normalized;
		//(wayPt.transform.position - rigidbody.position).normalized;
		newDir.y = 0;
		rigidbody.transform.forward = rigidbody.transform.forward * 0.5f + newDir * 0.5f;
		// move toward target
		rigidbody.AddRelativeForce(Vector3.forward * 15.0f);
	
	}
	
	void SeekSteering(){
	
//		GameObject wayPt = GameObject.Find("BuggyPrime");
//		Vector3 newDir = (wayPt.transform.position - rigidbody.position).normalized;
//		//(wayPt.transform.position - rigidbody.position).normalized;
//		newDir.y = 0;
//		rigidbody.transform.forward = rigidbody.transform.forward * 0.5f + newDir * 0.5f;
//		// move toward target
//		rigidbody.AddRelativeForce(Vector3.forward * 15.0f);
		
		//Vector3.Lerp(newDir, rigidbody.position, Time.deltaTime);
		
//		Vector3 location = transform.position;
//		
//		Vector3 desired = Vector3.Lerp(wayPt.transform.position, location, Time.deltaTime);
//		
//		float d = desired.magnitude;
//		
//		if(d > 0){
//		
//			desired.Normalize();
//			
//			//desired * maxSpeed;
//			
//			steer = Vector3.Lerp(desired, location, Time.deltaTime);
//		}		
		
	}
}
