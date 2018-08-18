using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {
    public GameObject PlayerUnitPrefab;
    public World localWorld;
    
	// Use this for initialization
	void Start () {
        localWorld = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        if (!isLocalPlayer)
        {
            //this obj belongs to other player
            return;
        }
        //only spawns for local player
        //Instantiate(PlayerUnitPrefab);
        CmdSpawnUnit();//gets sent to the server
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            //this obj belongs to other player
            return;
        }
        
        //update runs on everyone's computer
    }

    //commands runs only on the server

    [Command]
    void CmdSpawnUnit()
    {
        //guaranteed to be on the server
        GameObject go = Instantiate(PlayerUnitPrefab);
        //now thats in the server, propogate it to all clients and do network identity
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
        localWorld.playerPos = go.transform;
    }
}
