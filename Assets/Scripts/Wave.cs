using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wave : NetworkBehaviour
{

    public enum Message { follow, alarm };


    float currentRadius = 1.0f;
    float maxRadius = 30.0f;
    float spreadSpeed = 0.8f;

    bool stillSpreading;

    public Message waveMessage;
    public Vector2 spawningLocation;

    private void Awake()
    {
        stillSpreading = true;

        spawningLocation = new Vector2(transform.position.x, transform.position.y);

    }
    

    // Use this for initialization
    void Start ()
    {

        StartCoroutine(SpreadWave());

    }
	
	// Update is called once per frame
	void Update () {


        if(!stillSpreading)
        {
            Destroy(this.gameObject);
        }
       
    }

    IEnumerator SpreadWave()
    {
        while (true)
        {

            if(stillSpreading)
            {
                currentRadius += spreadSpeed;
                transform.GetChild(0).localScale = new Vector3(currentRadius, currentRadius, 0);

                if (transform.GetChild(0).localScale.x >= maxRadius)
                {
                    stillSpreading = false;

                }

                yield return new WaitForSeconds(spreadSpeed*0.01f);
            }
                
               yield return null;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isClient)
            return;
        Debug.Log("I hit " + other.name);
        IReceiver receiver = other.GetComponent<IReceiver>();
        if(receiver != null)
        {
            receiver.receive((int)waveMessage, spawningLocation);
        }
    }
}
