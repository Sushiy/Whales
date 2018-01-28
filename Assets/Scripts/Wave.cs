using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave : MonoBehaviour {

    enum Message { follow, alarm };


    float currentRadius;
    float maxRadius;
    float spreadSpeed;

    bool stillSpreading;

    public int messageType;


    Message waveMessage;
    public Vector2 spawningLocation;


    private void Awake()
    {

        currentRadius = 1.0f;
        maxRadius = 30.0f;
        spreadSpeed = 0.8f;

        stillSpreading = true;

        spawningLocation = new Vector2(transform.position.x, transform.position.y);

        setWaveMessage(messageType);

    }
    

    // Use this for initialization
    void Start () {

        StartCoroutine(SpreadWave());


    }
	
	// Update is called once per frame
	void Update () {


        if(!stillSpreading)
        {

            InputObserver.instance.setB1True();
            InputObserver.instance.setB2True();

            Destroy(this.gameObject);
        }
       
    }

    void setWaveMessage(int type)
    {
        if (type == 0)
        {
            waveMessage = Message.follow;
           
        }
        else if (type == 1)
        {
            waveMessage = Message.alarm;
           
        }


    }

    IEnumerator SpreadWave()
    {
        while (true)
        {

            if(stillSpreading)
            {
                currentRadius += spreadSpeed;
                transform.localScale = new Vector3(currentRadius, currentRadius, 0);

                if (transform.localScale.x >= maxRadius)
                {
                    stillSpreading = false;
                    
                   
                    
                }

                yield return new WaitForSeconds(spreadSpeed*0.01f);
            }
                
               yield return null;

        }
    }
}
