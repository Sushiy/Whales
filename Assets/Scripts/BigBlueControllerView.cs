using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BigBlueControllerView : MonoBehaviour
{
    Vector2 desiredDirection;
    float speed;

    public float paddlingInterval = 2.0f;
    public float paddlingForce = 10.0f;

    public float turningForce = 5.0f;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        BigBlueController.instance.guidanceDirection.Subscribe(dir => { desiredDirection = dir; });
        BigBlueController.instance.guidanceSpeed.Subscribe(spd => { speed = spd; Debug.Log("Speed:" + speed); });
        StartCoroutine(Paddling());
    }
    void Update()
    {
        Debug.DrawRay(transform.position, desiredDirection * speed, Color.green);

        //Slowly align to the desired directionvector
        Vector2 separatingVector = (Vector2)transform.right - desiredDirection;
        Vector2 projectedSepVector = Vector3.Project(separatingVector, transform.up);
        rigid.AddForceAtPosition(projectedSepVector * turningForce * Time.deltaTime, new Vector2(1.8f, 0.0f), ForceMode2D.Force);

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
        if (BigBlueController.instance != null)
            Gizmos.DrawWireSphere(transform.position, BigBlueController.instance.guidanceRange);
    }

}