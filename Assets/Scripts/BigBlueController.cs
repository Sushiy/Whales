using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueController : MonoBehaviour
{
    public static BigBlueController instance;
    List<BigBlueGuides> guides;

    public ReactiveProperty<Vector2> guidanceDirection;
    public ReactiveProperty<float> guidanceSpeed;

    public float guidanceRange = 3.0f;

    void Awake()
    {
        instance = this;
        guidanceDirection = new ReactiveProperty<Vector2>();
        guidanceSpeed = new ReactiveProperty<float>();
    }

    // Use this for initialization
    void Start()
    {
        guides = BigBlueGuides.instances;
        List<IObservable<Vector2>> guides_Position = new List<IObservable<Vector2>>();
        //Fill a List with all available Guides
        foreach (BigBlueGuides guide in guides)
        {
            guides_Position.Add(guide.worldPosition);
        }

        //Make a new Observable List with the Combined Guides, that fires at the end of the frame
        Observable.CombineLatest(guides_Position).ThrottleFrame(1).Subscribe(guidePositions => CalculateGuidanceDirection(guidePositions));
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Recalculate your Directionvector
    public void CalculateGuidanceDirection(IList<Vector2> guidePositions)
    {
        //Initialize Vectors
        Vector2 direction = Vector2.zero;
        Vector2 ownPosition = transform.position;
        bool shouldCalculate = true;
        foreach (Vector2 pos in guidePositions)
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
            guidanceSpeed.Value = ((direction.magnitude / guidePositions.Count) + guidanceRange) / 2.0f;
            //Normalize Direction
            direction.Normalize();
            //Update reactiveProperty
            guidanceDirection.Value = direction / 2.0f;
        }

    }
}