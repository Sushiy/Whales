using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueGuidesAutoSteer : MonoBehaviour
{

    BigBlueController bigBlue;

    // Use this for initialization
    void Start ()
    {
        bigBlue = BigBlueController.instance;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}
}
