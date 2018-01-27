using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    public Vector2 currentPosition;
    public Vector2 currentDirection;

    public float velocity;
    public float paddleInterval = 2.0f;
    public bool hasPaddled = false;

    public float flipperInterval = 1.0f;
    public bool hasFlippered = false;


    public Vector2 curDirection;
    public Vector2 movement;

    public float turningForce = 0.5f;
    public float paddleForce = 2.0f;

    public Rigidbody2D rigid;

    public Vector3 currentAngle;
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

        StartCoroutine(PaddleDelay());
        StartCoroutine(FlipperDelay());

    }

    // Use this for initialization
    void Start () {



        currentAngle = transform.eulerAngles;

        // not looked from the observer, instead from manager
        InputManager.instance.direction.Subscribe(dir =>
        {

        //the velocity should depend on the length of the directions vector and the amplitude of swim buttons pressed.
        // swim button are kind of impulses coming to move the fins.


            movement = new Vector3(dir.x, dir.y).normalized;
            //rigid.AddForce(transform.right * dir.magnitude * paddleForce, ForceMode2D.Impulse);


        });

	}
    private void Update()
    {
        if (movement.magnitude > 0.0f && !hasPaddled)
        {
            rigid.AddForce(transform.right * movement.magnitude * paddleForce, ForceMode2D.Impulse);
            hasPaddled = true;
        }
        //Debug.DrawRay(rigid.transform.position, transform.right, Color.red);
        Debug.DrawRay(rigid.transform.position, movement, Color.yellow);
    }
    // Update is called once per frame
    void FixedUpdate ()
    {

        //Slowly align to the desired directionvector
        Vector2 separatingVector =  movement - (Vector2)transform.right;
        Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
        rigid.AddForceAtPosition(projectedSepVector.normalized * turningForce * Time.fixedDeltaTime, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Force);
        //rigid.angularVelocity *= 0.98f;
        //
        Debug.DrawRay(rigid.transform.position, projectedSepVector * turningForce, Color.red);
        Debug.DrawRay(rigid.transform.position, separatingVector, Color.blue);
        Debug.DrawRay(rigid.transform.position, movement, Color.yellow);
        /*if (!hasFlippered)
        {
            rigid.AddForceAtPosition(-projectedSepVector * turningForce, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Impulse);
            hasFlippered = true;
        }*/

        rigid.angularVelocity = Mathf.Clamp(rigid.angularVelocity, -20.0f, 20.0f);
    }

    IEnumerator PaddleDelay()
    {
        while (true)
        {

            if (hasPaddled)
            {
                yield return new WaitForSeconds(paddleInterval);
                hasPaddled = false;
            }

            yield return null;
            
        }
    }

    IEnumerator FlipperDelay()
    {
        while (true)
        {

            if (hasFlippered)
            {
                yield return new WaitForSeconds(flipperInterval);
                hasFlippered = false;
            }

            yield return null;

        }
    }

    void ChangeDirection()
    {

    }
}
