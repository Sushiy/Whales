﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class PlayerButtonMovement : MonoBehaviour
{

    public static PlayerButtonMovement instance;

    Vector2 currentPosition;
    Vector2 currentDirection;

    float velocity;
    public float paddleInterval = 2.0f;
    public bool hasPaddled = false;

    Vector2 curDirection;
    Vector2 movement;

    public float turningForce = 0.5f;
    public float paddleForce = 2.0f;

    Rigidbody2D rigid;

    public float maxDegreePerSecond = 40.0f;
    Vector3 currentAngle;
    Vector3 targetAngle;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }


        instance = this;


        rigid = GetComponent<Rigidbody2D>();

        velocity = 1.0f;

        //StartCoroutine(PaddleDelay());

    }

    // Use this for initialization
    void Start()
    {



        currentAngle = transform.eulerAngles;

        // not looked from the observer, instead from manager
        InputManager.instance.direction.Subscribe(dir =>
        {

            //the velocity should depend on the length of the directions vector and the amplitude of swim buttons pressed.
            // swim button are kind of impulses coming to move the fins.


            movement = new Vector3(dir.x, dir.y).normalized;


        });

    }
    private void Update()
    {
        Debug.DrawRay(rigid.transform.position, movement, Color.yellow);
    }

    public void paddle()
    {
        // if (movement.magnitude > 0.0f && !hasPaddled && InputObserver.instance.b3FakeCondition == false)
        if (movement.magnitude > 0.0f)
        {
            rigid.AddForce(transform.right * movement.magnitude * paddleForce, ForceMode2D.Impulse);
            hasPaddled = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        

            //Slowly align to the desired directionvector
            Vector2 separatingVector = movement - (Vector2)transform.right;
        Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
        rigid.AddForceAtPosition(projectedSepVector.normalized * turningForce * Time.fixedDeltaTime, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Force);

        Debug.DrawRay(rigid.transform.position, projectedSepVector * turningForce, Color.red);
        Debug.DrawRay(rigid.transform.position, separatingVector, Color.blue);
        Debug.DrawRay(rigid.transform.position, movement, Color.yellow);

        rigid.angularVelocity = Mathf.Clamp(rigid.angularVelocity, -maxDegreePerSecond, maxDegreePerSecond);
    }

    IEnumerator PaddleDelay()
    {
        while (true)
        {

            if (hasPaddled)
            {
                yield return new WaitForSeconds(paddleInterval);
                hasPaddled = false;
                //InputObserver.instance.b3FakeCondition = true;
            }

            yield return null;

        }
    }

    void ChangeDirection()
    {

    }
}
