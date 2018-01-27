using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public class MP_BigBlueView : NetworkBehaviour
{
    public Vector2 desiredDirection;
    float speed;

    public float maxVelocity = 10.0f;

    public float paddlingInterval = 2.0f;
    public float paddlingForce = 10.0f;

    public float turningForce = 5.0f;

    Rigidbody2D rigid;

    MP_BigBlueController model;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        model = GetComponent<MP_BigBlueController>();
        model.guidanceDirection.Subscribe(dir => { desiredDirection = dir; });
        model.guidanceSpeed.Subscribe(spd => { speed = spd; Debug.Log("Speed:" + speed); });
        StartCoroutine(Paddling());
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, desiredDirection * speed, Color.green);

        if (Vector2.Angle(transform.right, Vector3.right) > 45)
        {
            //Slowly align to the desired directionvector
            desiredDirection = Vector2.right;
        }
        Vector2 separatingVector = desiredDirection - (Vector2)transform.right;
        Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
        rigid.AddForceAtPosition(projectedSepVector * turningForce * Time.fixedDeltaTime, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Force);

        rigid.angularVelocity = Mathf.Clamp(rigid.angularVelocity, -20.0f, 20.0f);
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, 10.0f);
    }

    IEnumerator Paddling()
    {
        while (true)
        {
            yield return new WaitForSeconds(paddlingInterval);
            rigid.AddForce(transform.right * paddlingForce, ForceMode2D.Impulse);
        }
    }


    private void OnDrawGizmos()
    {
        if (model != null)
            Gizmos.DrawWireSphere(transform.position, model.guidanceRange);
    }
}
