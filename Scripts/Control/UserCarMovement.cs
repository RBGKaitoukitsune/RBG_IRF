using UnityEngine;
using System.Collections;

public class UserCarMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate(){
	
		
//		if(Input.GetKeyDown("space")){
//			
//			GameObject car = GameObject.Find ("UserCar");
//		
//			GameObject P = (GameObject)Instantiate(GameObject.Find("ProjectileSphere"),
//				car.transform.position + car.transform.forward.normalized * 1 + car.transform.up.normalized * 5,
//				Quaternion.AngleAxis(0, Vector3.up));
			
			//P.rigidbody.velocity
		//}
	
		if(networkView.isMine)
			
			
		{
		
			if(Input.GetKey("space"))
			{
				GameObject reststartpos = GameObject.Find("SpawnPoint");
			
				transform.position = new Vector3(reststartpos.transform.position.x, reststartpos.transform.position.y, reststartpos.transform.position.z);
				transform.rotation = new Quaternion(0,0,0,0);
				rigidbody.velocity.Set(0,0,0);
			}
		
			if(Input.GetKey("up"))
			{
				
			rigidbody.AddRelativeForce(Vector3.forward * 20f);
			}
			
			if(Input.GetKey("down"))
			{
		
				rigidbody.AddRelativeForce(Vector3.back * 10f);
			}
			
			if(Input.GetKey("left"))
			{
				//rigidbody.transform.Rotate(Vector3.up, -0.1f);
				rigidbody.AddRelativeTorque(Vector3.up * -10f);		
			}
			
			if(Input.GetKey("right"))
			{
				//rigidbody.transform.Rotate(Vector3.up, 0.1f);
				rigidbody.AddRelativeTorque(Vector3.up * 10f);
			}
			
			
			
		}
		
		
	}
}
