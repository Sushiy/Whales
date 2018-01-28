using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_BigBlueReceiver : NetworkBehaviour, IReceiver
{
    public void receive(int message, Vector2 position)
    {
        if (message == (int)Wave.Message.follow)
            GetComponentInParent<MP_BigBlueController>().GetSignal(position);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
