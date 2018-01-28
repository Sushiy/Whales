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
    public float OxygenThreshhold
    {
        get
        {
            return oxygenConsumptionRate * 40.0f;
        }
    }
    public float drowningInterval = 3.0f;
    public bool drowning = false;
	// Update is called once per frame
	void Update ()
    {
        if(isServer)
        {
            oxygenLevel -= oxygenConsumptionRate * Time.deltaTime;
            if (oxygenLevel <= OxygenThreshhold)
                needOxygen = true;
            if(oxygenLevel<=0 && !drowning)
            {
                StartCoroutine(Drown());
                drowning = true;
            }
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

    IEnumerator Drown()
    {
        while(oxygenLevel <= 0)
        {
            GetComponent<MP_Health>().TakeDamage();
            yield return new WaitForSeconds(drowningInterval);
        }
    }
}
