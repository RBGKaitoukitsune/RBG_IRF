using UnityEngine;
using System.Collections;

public class BreakLights : MonoBehaviour {
	
	public Material lights;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {

		// If the key is pressed and held, the break lights will turn on.
		// The lights will turn off when the key is released.
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			renderer.material.color = Color.red;
		}
		else if(Input.GetKeyUp(KeyCode.DownArrow)){
			renderer.material = lights;
		}
		
		// This checks for the W and S keys
		if(Input.GetKeyDown(KeyCode.S)){
			renderer.material.color = Color.red;
		}
		else if(Input.GetKeyUp(KeyCode.S)){
			renderer.material = lights;
		}
	}
}
