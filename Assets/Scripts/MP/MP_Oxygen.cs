using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class MP_Oxygen : NetworkBehaviour
{
    [SyncVar]
    public bool needOxygen = false;

    public float oxygenLevel;

    public float oxygenConsumptionRate = 3.0f;

    public float maxOxygen = 100.0f;
    
    //Give the whale 20 seconds to resurface
    public float oxygenThreshhold { get { return oxygenConsumptionRate * 40.0f; } }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isServer)
        {
            oxygenLevel -= oxygenConsumptionRate * Time.deltaTime;
            if (oxygenLevel <= oxygenThreshhold)
                needOxygen = true;
        }
	}

    public void Breathe()
    {
        if (!isServer)
            return;
        oxygenLevel = maxOxygen;
        if(needOxygen)
            needOxygen = false;
    }
}
