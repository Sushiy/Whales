using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HunterBehaviour : NetworkBehaviour
{
    public Transform waterHole1;
    public Transform waterHole2;
    public GameObject focusedTarget;

    public bool isTargetingLeftHole;

    [SyncVar]
    public int currentState; // waiting left, waiting right, attentively, aiming, shooting, reloading

    public bool isAttacking;
    public bool isLeft;
    public bool isRight;
    public bool isAlarmed;
    public bool isWalking;

    public bool getsSignal;


    float velocity;
    public float threshhold;

    

    void Awake()
    {
        //waterEntrance = new Vector2(waterHole1.transform.position.x, waterHole1.transform.position.y); 
        if(isServer)
            currentState = 1;

        velocity = 0.05f;      

        isAlarmed = false;
        isWalking = false;
        isLeft = true;
        isRight = false;
        isAttacking = false;
        getsSignal = false;

        threshhold = 5.0f;
       

    }

    // Use this for initialization
    void Start () {

        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            isTargetingLeftHole = true;
        }
        else
        {
            isTargetingLeftHole = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isServer)
            return;
        switch (currentState)
        {
            //alarmed
            case 0:
                // signal comes in which hole is nearer?
                isWalking = false;
                playAnimation("focused");

                if (getsSignal)
                {

                    if (new Vector2(focusedTarget.transform.position.x + waterHole1.position.x, focusedTarget.transform.position.y + waterHole1.position.y).magnitude <= new Vector2(focusedTarget.transform.position.x + waterHole2.position.x, focusedTarget.transform.position.y + waterHole2.position.y).magnitude)
                    {
                        //linkes loch
                        if (isLeft)
                        {
                            isTargetingLeftHole = true;

                            Debug.Log(new Vector2(focusedTarget.transform.position.x + transform.position.x, focusedTarget.transform.position.y + transform.position.y).magnitude);
                            // already left hole
                            if (new Vector2(focusedTarget.transform.position.x + transform.position.x, focusedTarget.transform.position.y + transform.position.y).magnitude <= threshhold)
                            {
                                currentState = 3;
                                getsSignal = false;
                            }
                        }
                        else
                        {
                            // he is right and needs to move left

                            //moveLeft
                            currentState = 1;
                        }
                    }
                    else
                    {
                        //linkes loch
                        if (isLeft)
                        {
                            Debug.Log(new Vector2(focusedTarget.transform.position.x + transform.position.x, focusedTarget.transform.position.y + transform.position.y).magnitude);
                            // he is left and needs to move right
                            if (new Vector2(focusedTarget.transform.position.x + transform.position.x, focusedTarget.transform.position.y + transform.position.y).magnitude <= threshhold)
                            {
                                currentState = 3;
                                getsSignal = false;
                            }
                        }
                        else
                        {

                            // already right hole
                            //moveLeft
                            currentState = 2;
                        }

                        //rechtes loch
                    }

                }




                break;
            // walk to the left
            case 1:
                isWalking = true;

                if (!isTargetingLeftHole)
                {
                    isTargetingLeftHole = true;
                    GetComponent<SpriteRenderer>().flipX = true;
                }

                if (transform.position.x > waterHole1.position.x)
                {
                    playAnimation("walking");
                    transform.Translate(-velocity, 0.0f, 0.0f);
                }
                else
                {
                    playAnimation("focused");
                    currentState = 0;
                }
                // spielgele hunter
                // starte lauf animation

                //wenn da stoppe die laufanimation

                break;
            
            //walk to the right 
            case 2:
                isWalking = true;
                
                if (isTargetingLeftHole)
                {
                    isTargetingLeftHole = false;
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                playAnimation("focused");
                if (transform.position.x < waterHole2.position.x)
                {
                    playAnimation("walking");
                    transform.Translate(velocity, 0.0f, 0.0f);
                }
                else
                {
                    playAnimation("focused");
                    currentState = 0;
                }
                //figur spiegelen?
                
                // laufanimation an

                break;

            //attacking 
            case 3:
                playAnimation("Attack");
                // Abspielen der Angriffsanimation Animation
                currentState = 0;
                break;

            default: break;

        }


	}

   // float getDistance()
   // {
   //     return new Vector2(waterEntrance.x + transform.position.x, waterEntrance.y + transform.position.y).magnitude;
   // }

    void playAnimation(string anima)
    {

        GetComponent<Animator>().SetTrigger(anima);

    }

    void enableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }


    void disableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    [Command]
    public void CmdChangeState(int newState)
    {
        currentState = newState;
    }
}
