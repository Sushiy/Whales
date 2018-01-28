using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HunterBehaviour : NetworkBehaviour
{
    public Transform waterHole1;
    public Transform waterHole2;
    [SyncVar]
    public Vector2 focusedTarget;

    public bool isTargetingLeftHole;

    [SyncVar]
    public int currentState; // waiting left, waiting right, attentively, aiming, shooting, reloading

    public bool isAttacking;
    public bool isLeft;
    public bool isRight;
    public bool isAlarmed;
    public bool isWalking;

    [SyncVar]
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
        if(!isClient)
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

                    if ((focusedTarget - (Vector2)waterHole1.position).magnitude >= (focusedTarget - (Vector2)waterHole2.position).magnitude)
                    {
                        //linkes loch
                        if (isLeft)
                        {
                            isTargetingLeftHole = true;
                            Debug.Log("is that a whale on the left?");

                            Debug.Log((focusedTarget - (Vector2)transform.position).magnitude);
                            // already left hole
                            if ((focusedTarget - (Vector2)transform.position).magnitude <= threshhold)
                            {
                                Debug.Log("Huya!!");

                                CmdChangeState(3);
                                getsSignal = false;
                            }
                            else
                                Debug.Log("Too far away :(");
                        }
                        else
                        {
                            // he is right and needs to move left
                            Debug.Log("need to go left!");

                            //moveLeft
                            CmdChangeState(1);
                        }
                    }
                    else
                    {
                        //rechtes loch
                        if (!isLeft)
                        {
                            Debug.Log("is that a whale on the right?");

                            //Debug.Log(new Vector2(focusedTarget.x + transform.position.x, focusedTarget.y + transform.position.y).magnitude);
                            // he is left and needs to move right
                            if (new Vector2(focusedTarget.x + transform.position.x, focusedTarget.y + transform.position.y).magnitude <= threshhold)
                            {
                                CmdChangeState(3);
                                getsSignal = false;
                            }
                        }
                        else
                        {
                            Debug.Log("need to go right!");
                            // already left hole
                            //move right
                            CmdChangeState(2);
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
                    CmdChangeState(0);
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
                    CmdChangeState(0);
                }
                //figur spiegelen?
                
                // laufanimation an

                break;

            //attacking 
            case 3:
                playAnimation("Attack");
                // Abspielen der Angriffsanimation Animation
                CmdChangeState(0);
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

    [Command]
    public void CmdSetFocus(Vector2 position)
    {
        focusedTarget = position;
        getsSignal = true;
    }
}
