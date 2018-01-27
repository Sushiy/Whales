using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueControllerView : MonoBehaviour
{
    public Vector2 desiredDirection;
    float speed;

    public float maxVelocity = 10.0f;

    public float paddlingInterval = 2.0f;
    public float paddlingForce = 10.0f;

    public float turningForce = 5.0f;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start ()
    {
        BigBlueController.instance.guidanceDirection.Subscribe(dir => { desiredDirection = dir;});
        BigBlueController.instance.guidanceSpeed.Subscribe(spd => { speed = spd;Debug.Log("Speed:" + speed); });
        StartCoroutine(Paddling());
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, desiredDirection*speed, Color.green);

        if(Vector2.Angle(transform.right, Vector3.right) < 45)
        {
            //Slowly align to the desired directionvector
            Vector2 separatingVector = desiredDirection - (Vector2)transform.right;
            Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
            rigid.AddForceAtPosition(projectedSepVector * turningForce * Time.fixedDeltaTime, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Force);
        }

        rigid.angularVelocity = Mathf.Clamp(rigid.angularVelocity, -20.0f, 20.0f);
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, 10.0f);
    }

    IEnumerator Paddling()
    {
        while(true)
        {
            yield return new WaitForSeconds(paddlingInterval);
            rigid.AddForce(transform.right * paddlingForce, ForceMode2D.Impulse);
        }
    }

    
    private void OnDrawGizmos()
    {
        if(BigBlueController.instance != null)
            Gizmos.DrawWireSphere(transform.position, BigBlueController.instance.guidanceRange);
    }

}
