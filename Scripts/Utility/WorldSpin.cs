using UnityEngine;
using System.Collections;

public class WorldSpin : MonoBehaviour {
	
	public Transform world;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		world.Rotate(0, Time.deltaTime * -30f, 0);
	
	}
}
