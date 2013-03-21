using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {
	public GameObject player;
	public GameObject cam;
	public Transform spawnObject;
	public ArrayList[] checkPointArray;
	float btnx;
	float btny;
	float btnW;
	float btnH;
	bool refreshing;
	float length;
	HostData[] hostdata;
	bool hostdatacreate;
	string gamename = "IRF_RACE";
	// Use this for initialization
	void Start()
	{
		btnx = 0;
		btny = 10;
		btnW = 400;
		btnH = 200;
		
		
	}
	
	
	void StartSever()
	{
		bool useNat = !Network.HavePublicAddress();
		
	Network.InitializeServer(32,25001,useNat);	
		MasterServer.RegisterHost(gamename,"IRF Game", "Devry Project");
		
		
	}
	
	void spawnPlayer()
	{
		Network.Instantiate(player, spawnObject.position, Quaternion.identity, 0);
		Network.Instantiate(cam, spawnObject.position, Quaternion.identity, 0);
		
		
	}
	
	
	void OnServerInitialized()
	{
		Debug.Log("server init");		
		spawnPlayer();
		
	}
	void OnConnectedToServer()
	{
		
	spawnPlayer();	
	}
	
		
	
	 void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		
		if(msEvent ==MasterServerEvent.RegistrationSucceeded)
		{
		Debug.Log("reg complete");	
		}
		
	}
	
	
	void refreshHostList()
	{
		refreshing=true;
		MasterServer.RequestHostList(gamename);
		
		MasterServer.PollHostList();
		
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	void OnGUI () {
		if(!Network.isClient && !Network.isServer)
		{
			if(GUI.Button(new Rect(btnx,btny,btnW, btnH), "Start server"))
				{
					
					StartSever();
					
				}
				if(GUI.Button(new Rect(btnx,(btny+btnH+10.0f),btnW, btnH), "Refresh"))
				{
					Debug.Log("clicked");
					refreshHostList();	
					
				}
						if(hostdatacreate)
					{
						
						for (int i = 0; i<hostdata.Length; i++)
						{
							Debug.Log(hostdatacreate.ToString());
						if(GUI.Button(new Rect(400,40+(10-i), 100,100), hostdata[i].gameName))
						{
							Network.Connect(hostdata[i]);
							
						}
						}
					}
			}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (refreshing)
			if(MasterServer.PollHostList().Length >0)
		{
			refreshing= false;
			Debug.Log(MasterServer.PollHostList().Length);
			hostdata = MasterServer.PollHostList();
			hostdatacreate=true;
			
		}
	
	}
}
