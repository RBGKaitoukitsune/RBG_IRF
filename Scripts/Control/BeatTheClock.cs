using UnityEngine;
using System.Collections;

public class BeatTheClock : MonoBehaviour {
	
	float timeToBeat;
	float timeBeat;
	string timeToBeatGUI;
	
	//const float secondsPerMinute = 60;
	bool triggered;
	bool lost;
	bool paused;
	
	// Use this for initialization
	void Start () 
	{
		triggered = false;
		lost = false;
		paused = false;
		timeToBeat = 65.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		timeToBeat -= Time.deltaTime;
		//guiText.text = FormatTime(timeToBeat);
	
		if(timeToBeat <= 0.0f)
		{
			paused = togglePause();
			lost = true;
			
		}
		
	}
	
	// When the buggy passes through the trigger, increment laps
	void OnTriggerEnter(Collider collision)
	{
		
		if(collision.gameObject.name == "VehiclePhysics")
		{
			triggered = true;
			timeBeat = Time.deltaTime + timeToBeat;
			paused = togglePause();
		}
		
	}
	
	void OnGUI()
	{

		if(!triggered)
		{
			timeToBeatGUI = string.Format ("{0:00} Secs", timeToBeat); 
			GUI.Box(new Rect(570,0,200,25), "Time to Beat:  " + timeToBeatGUI);
		}
		else if(triggered)
		{
			timeToBeatGUI = string.Format ("{0:00} Secs", timeBeat); 
			GUI.Box(new Rect(570,25,200,25), "Time Left:  " + timeToBeatGUI);
			
			GUI.Box(new Rect(570,50,200,25), "YOU BEAT THE TIME!");
		}
		
		if(!triggered)
		{
			if(lost)
			{
				GUI.Box(new Rect(570,70,200,25), "YOU FAILED!");
			}
		}
	}
	
	// toggle pause variable
	bool togglePause()
	{
       if(Time.timeScale == 0f)
       {
         Time.timeScale = 1f;
         return(false);
       }
       else
       {
         Time.timeScale = 0f;
         return(true);    
       }
	}
}
