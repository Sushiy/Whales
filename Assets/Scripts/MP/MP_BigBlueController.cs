using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public enum WhaleState
{
    FOLLOWING,
    SIGNALLED,
    OUTOFOXYGEN,
    ALONE,
    BREATHING
}

public class MP_BigBlueController : NetworkBehaviour
{
    public static MP_BigBlueController instance;
    List<MP_PlayerMovement> guides;

    public WhaleState state = WhaleState.FOLLOWING;

    [HideInInspector]
    public ReactiveProperty<Vector2> guidanceDirection;
    [HideInInspector]
    public ReactiveProperty<float> guidanceSpeed;
    
    public Vector2 GuidanceDirection;
    public float GuidanceSpeed;

    public float guidanceRange = 3.0f;

    MP_Oxygen oxygen;
    MP_Health health;

    void Awake()
    {
        instance = this;
        oxygen = GetComponent<MP_Oxygen>();
        health = GetComponent<MP_Health>();
        guides = new List<MP_PlayerMovement>();

        guidanceDirection = new ReactiveProperty<Vector2>();
        guidanceSpeed = new ReactiveProperty<float>();

        guidanceDirection.Subscribe(val => GuidanceDirection = val);
        guidanceSpeed.Subscribe(val => GuidanceSpeed = val);

    }

    // Update is called once per frame
    void Update()
    {
        if(state == WhaleState.FOLLOWING)
        {
            if ((transform.position.y >= 11.0f && oxygen.oxygenLevel <= oxygen.maxOxygen - 30.0f * oxygen.oxygenConsumptionRate) || oxygen.needOxygen)
            {
                state = WhaleState.OUTOFOXYGEN;
            }
            else if (guides.Count > 0)
            {
                Vector2[] positions = new Vector2[guides.Count];
                bool shouldCalculate = true;
                for (int i = 0; i < guides.Count; i++)
                {
                    positions[i] = guides[i].transform.position;
                    Debug.DrawLine(transform.position, guides[i].transform.position, Color.cyan);
                    //Check if you are in range
                    if ((positions[i] - (Vector2)transform.position).sqrMagnitude > guidanceRange * guidanceRange)
                    {
                        shouldCalculate = false;
                    }
                }

                if(shouldCalculate)
                    CalculateGuidanceDirection(positions);
                else
                {
                    state = WhaleState.ALONE;
                }
            }
        }
        else if(state == WhaleState.OUTOFOXYGEN)
        {
                guidanceDirection.Value = (new Vector3(transform.position.x + 3.0f, 14.0f, 0.0f) - transform.position).normalized;
            if(transform.position.y >= 12.0f)
            {
                oxygen.Breathe();
                state = WhaleState.FOLLOWING;
            }
        }
        else if(state == WhaleState.ALONE)
        {
            if ((transform.position.y >= 11.0f && oxygen.oxygenLevel <= oxygen.maxOxygen - 30.0f * oxygen.oxygenConsumptionRate) || oxygen.needOxygen)
            {
                state = WhaleState.OUTOFOXYGEN;
            }
            guidanceDirection.Value = Vector2.right;

            Vector2[] positions = new Vector2[guides.Count];
            bool guidesInRange = true;
            for (int i = 0; i < guides.Count; i++)
            {
                positions[i] = guides[i].transform.position;
                Debug.DrawLine(transform.position, guides[i].transform.position, Color.cyan);
                //Check if you are in range
                if ((positions[i] - (Vector2)transform.position).sqrMagnitude > guidanceRange * guidanceRange)
                {
                    guidesInRange = false;
                }
            }

            if(guidesInRange)
            {
                state = WhaleState.FOLLOWING;
            }
        }
        else if (state == WhaleState.SIGNALLED)
        {
            if ((transform.position.y >= 11.0f && oxygen.oxygenLevel <= oxygen.maxOxygen - 30.0f * oxygen.oxygenConsumptionRate) || oxygen.needOxygen)
            {
                state = WhaleState.OUTOFOXYGEN;
            }

            Vector2[] positions = new Vector2[guides.Count];
            bool guidesInRange = true;
            for (int i = 0; i < guides.Count; i++)
            {
                positions[i] = guides[i].transform.position;
                Debug.DrawLine(transform.position, guides[i].transform.position, Color.cyan);
                //Check if you are in range
                if ((positions[i] - (Vector2)transform.position).sqrMagnitude > guidanceRange * guidanceRange)
                {
                    guidesInRange = false;
                }
            }

            if (guidesInRange)
            {
                state = WhaleState.FOLLOWING;
            }
        }
    }


    //Recalculate your Directionvector
    public void CalculateGuidanceDirection(Vector2[] positions)
    {
        //Initialize Vectors
        Vector2 direction = Vector2.zero;
        Vector2 ownPosition = transform.position;
        foreach (Vector2 pos in positions)
        {
            direction += (pos - ownPosition);
        }
        
        //Average out GuidanceSpeed
        guidanceSpeed.Value = ((direction.magnitude / positions.Length) + guidanceRange) / 2.0f;
        //Normalize Direction
        direction.Normalize();
        //Update reactiveProperty
        guidanceDirection.Value = direction / 2.0f;

    }

    public void AddGuide(MP_PlayerMovement guide)
    {
        Debug.Log("Guide added");
        guides.Add(guide);
    }

    public void GetSignal(Vector2 position)
    {
        if(state == WhaleState.ALONE)
        {
            state = WhaleState.SIGNALLED;
            guidanceDirection.Value = (position - (Vector2)transform.position).normalized;
        }
    }
}
