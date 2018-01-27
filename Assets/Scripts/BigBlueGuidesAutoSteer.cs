using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueGuidesAutoSteer : MonoBehaviour
{
    Vector2 desiredDirection;

    BigBlueControllerView bigBlue;

    public float paddlingInterval = 2.0f;
    public float paddlingForce = 10.0f;

    public float turningForce = 5.0f;

    Rigidbody2D rigid;

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        bigBlue = BigBlueController.instance.GetComponent<BigBlueControllerView>();
        paddlingInterval = bigBlue.paddlingInterval/2.0f;
        paddlingForce = bigBlue.paddlingForce/2.0f;

        StartCoroutine(Paddling());

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        desiredDirection = bigBlue.desiredDirection;
        Vector2 separatingVector = desiredDirection - (Vector2)transform.right;
        Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
        rigid.AddForceAtPosition(projectedSepVector * turningForce * Time.fixedDeltaTime, (Vector3)rigid.position + transform.right * 1.8f, ForceMode2D.Force);
    }

    IEnumerator Paddling()
    {
        while (true)
        {
            yield return new WaitForSeconds(paddlingInterval);
            rigid.AddForce(transform.right * paddlingForce, ForceMode2D.Impulse);
        }
    }
}
