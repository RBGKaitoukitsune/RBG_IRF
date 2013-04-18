using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeAttack : MonoBehaviour {
	
	//Time Attack Variables
	public List<float> times = new List<float>();
	public List<GameObject> checkpoints = new List<GameObject>();
	public List<GameObject> waypoints = new List<GameObject>();
	public Transform Grid;
	public GameObject playerModel;
	public GameObject camera;
	
	public List<GameObject> AIModels = new List<GameObject>();
	GameObject checkpoint;
	public bool timeattack;
	public bool beattheclock;
	public bool firstload;
	public bool isPausable;
	public float starttime,laptime,fasttime,totaltime, currenttime, endtime;
	float displayDays,displayHours, displayMinutes, displaySeconds;
	int laps;
	string currentTimeText;
	string lastTimeText;
	string fastTimeText;
	
	
	//Beat the clock variables
	public bool triggered;
	public bool lost;
	public bool paused;
	public float timeToBeat;
	public float timeBeat;
	public string timeToBeatGUI;
	
	

	public void spawnRacers(List<GameObject> AiRacers, GameObject player)
	{
		int counter=0;
		Transform[] transfrom = Grid.GetComponentsInChildren<Transform>();
		foreach (GameObject Ai in AiRacers)
		{
			
			Instantiate(AiRacers[counter],transfrom[counter].position,Grid.transform.rotation);
			Debug.Log("Spawned AI");
			counter++;
		}	
		Instantiate(player, transfrom[counter].position, Grid.transform.rotation);
		Debug.Log("Spawned Player");
		Instantiate(camera);
		
		    
   
    }
			
		
	
		
	
	
	public void Lap(GameObject chk_point)
	{	
	if (checkpoints.Count >= 4 )// a lap can only be completed if the array has all theck points in it
		{
			if(chk_point == checkpoints[0]) //have we reached the first checkpoint again, if yes, we have just completed a lap
			{				
				//add lap to counter
				laps++;
				//clear checkpoint list
				checkpoints.Clear();
				//resize the list
				checkpoints.TrimExcess();
					// add time to list
				SubmitTime();
				//new start time must be set
				starttime=Time.time;	
				
				// re add this check point
				checkpoints.Add(chk_point);				
				
			}

		}
	else
	{
		checkpoints.Add(chk_point);	
	}
		}
	void  SubmitTime()// adds the current time to the list		
	{		
	times.Add(laptime);	
	}
	void UpdateFastTime(float curtime)
	{
	
		foreach(float time in times)
		{
			if (laptime > time)
			{
			Debug.Log("Change time");
			fasttime = curtime;	
			}
		}
		
	}
	void UpdateTextFile(string filename)
		
	{
		
		
	}
	void Start()
	{
		spawnRacers(AIModels,playerModel);
		if(Application.loadedLevelName == "Desert")
		{
		beattheclock=true;
			return;			
		}
		Time.timeScale=1;
		firstload = true;
		
		triggered = false;
		lost = false;
		paused = false;
		isPausable=false;
	
	
	}		
	
	
	void pauseScreen()
	{
					if (GUI.Button(new Rect(0,0,200,25),"Restart"))
				{	
					Debug.Log("paused");
					Time.timeScale=1;
					if(Application.loadedLevelName == "Desert")
				Application.LoadLevel(2);
					if(Application.loadedLevelName == "Jungle")
						Time.timeScale = 1;
						Application.LoadLevel(1);
					
				}
				if (GUI.Button(new Rect(0,50,200,25), "Main Menu"))
				{
				Time.timeScale = 1;	
				Application.LoadLevel(0);	
				}
		
		
	}

	

	// Use this for initialization
	void OnGUI () 
	{
		if (firstload)
		{
		
			if(GUI.Button(new Rect(100,10,200,200),"Time Attack"))
			{
				timeattack=true;
				firstload=false;
				isPausable=true;
				starttime=Time.time;
				
			}
			if(GUI.Button(new Rect(700,10,200,200),"Beat The Clock"))
			{
				beattheclock=true;
				firstload=false;
				isPausable=true;
				starttime=Time.time;
				
			}				
					
		}
			if(paused)
			{
			pauseScreen();	
			}
	
		
		//Time Attack GUI
		
		if(timeattack == true && firstload == false)
			
			
		{
			if(!triggered)
			{
		GUI.Box(new Rect(0,0,200,200), "Time Attack");
		currentTimeText = string.Format ("{0:00}:{1:00}",(totaltime/60.0f), (totaltime%60.0f));
		lastTimeText = string.Format ("{0:00}:{1:00}",(laptime/60.0f), (laptime%60.0f)); 
		fastTimeText = string.Format ("{0:00}:{1:00}",(fasttime/60.0f), (fasttime%60.0f)); 
			
		GUI.Box(new Rect(0,50,200,25), "Elapsed Time:  " + currentTimeText);
		GUI.Box(new Rect(0,100,200,25), "Lap Time:  "+ lastTimeText);
		GUI.Box(new Rect(0,150,200,25), "Fastest Time:  "+ fastTimeText);
				
			}
			else
			{
				pauseScreen();				
			}
		}
		
		
		
		//beat the clock GUI
		if(beattheclock == true && firstload == false)
			
			
		{
		if(!triggered)
		{
			timeToBeatGUI = string.Format ("{0:00} Secs", timeToBeat); 
			GUI.Box(new Rect(570,0,200,25), "Time to Beat:  " + timeToBeatGUI);
		}
		else 
			if(triggered)
		{
			timeToBeatGUI = string.Format ("{0:00} Secs", timeBeat); 
			GUI.Box(new Rect(570,25,200,25), "Time Left:  " + timeToBeatGUI);			
			GUI.Box(new Rect(570,50,200,25), "YOU BEAT THE TIME!");
			pauseScreen();
				
		}
		
		if(!triggered)
		{
			if(lost)
			{
				GUI.Box(new Rect(570,70,200,25), "YOU FAILED!");
				pauseScreen();
				
			}
		}

			
			
			
		}
		
	  
	}
	
	public bool togglePause()
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
	
	
	
	// Update is called once per frame
	void Update () 
	{		
		totaltime = Time.time;
		laptime=totaltime-starttime;
		if(beattheclock)
		{
				timeToBeat -= Time.deltaTime;
		//guiText.text = FormatTime(timeToBeat);
		}
		if(Input.GetKey(KeyCode.Escape) && isPausable)
		{
		togglePause();	
		paused=!paused;
		}
	
		if(timeToBeat <= 0.0f)
		{
			paused = togglePause();
			lost = true;
			
		}

		}
	}


