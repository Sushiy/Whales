using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class MP_BigBlueController : NetworkBehaviour
{
    public static MP_BigBlueController instance;
    List<MP_PlayerMovement> guides;

    [HideInInspector]
    public ReactiveProperty<Vector2> guidanceDirection;
    [HideInInspector]
    public ReactiveProperty<float> guidanceSpeed;
    
    public Vector2 GuidanceDirection;
    public float GuidanceSpeed;

    public float guidanceRange = 3.0f;

    void Awake()
    {
        instance = this;

        guides = new List<MP_PlayerMovement>();

        guidanceDirection = new ReactiveProperty<Vector2>();
        guidanceSpeed = new ReactiveProperty<float>();

        guidanceDirection.Subscribe(val => GuidanceDirection = val);
        guidanceSpeed.Subscribe(val => GuidanceSpeed = val);

    }

    // Update is called once per frame
    void Update()
    {
        if (guides.Count > 0)
        {
            Vector2[] positions = new Vector2[guides.Count];
            for(int i=0; i<guides.Count;i++)
            {
               positions[i] = guides[i].transform.position;
                Debug.DrawLine(transform.position, guides[i].transform.position, Color.cyan);
            }
             
            CalculateGuidanceDirection(positions);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(2, 0, 0), Color.magenta);
        }
    }


    //Recalculate your Directionvector
    public void CalculateGuidanceDirection(Vector2[] positions)
    {
        //Initialize Vectors
        Vector2 direction = Vector2.zero;
        Vector2 ownPosition = transform.position;
        bool shouldCalculate = true;
        foreach (Vector2 pos in positions)
        {
            Vector2 localPosition = (pos - ownPosition);
            //if the whale is in range, keep calculating
            if (localPosition.sqrMagnitude <= guidanceRange * guidanceRange)
            {
                //Add each guide position
                direction += (pos - ownPosition);
            }
            //if it isn't stop the calculation and keep the old direction
            else
            {
                shouldCalculate = false;
                direction = guidanceDirection.Value;
            }
        }

        if (shouldCalculate)
        {
            //Average out GuidanceSpeed
            guidanceSpeed.Value = ((direction.magnitude / positions.Length) + guidanceRange) / 2.0f;
            //Normalize Direction
            direction.Normalize();
            //Update reactiveProperty
            guidanceDirection.Value = direction / 2.0f;
        }

    }

    public void AddGuide(MP_PlayerMovement guide)
    {
        Debug.Log("Guide added");
        guides.Add(guide);
    }
}
