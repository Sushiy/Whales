using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProperties : MonoBehaviour {

    float oxygen;
    float health;

    bool isAlive;


    private void Awake()
    {
        oxygen = 100.0f;
        health = 100.0f;

        isAlive = true;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOxygen(float newOxygen)
    {
        oxygen = newOxygen;
    }

    public void setHealth(float newHP)
    {
        health = newHP;
    }

    public void setCondition(bool alive)
    {
        isAlive = alive;
    }
}
