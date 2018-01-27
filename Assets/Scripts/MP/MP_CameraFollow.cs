using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float geschwindigkeit = 0.5f;
    [SerializeField]
    private float yOffset = 1f;

    private float LerpSpeed = 3f;

    [HideInInspector]
    public bool Follow = true;

    MP_PlayerMovement target;

    // Use this for initialization
    void Start()
    {
        target = MP_PlayerMovement.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Follow || target == null)
            return;

        //Kamera verfolgt die Position des Spielers

        Vector2 middlePos;
        Vector2 playerPos = target.transform.position;
        middlePos = playerPos;
        //Aktuelle Spielergeschwindigkeit wird eingerechnet
        Vector3 playerV = target.GetComponent<Rigidbody2D>().velocity;
        playerV *= geschwindigkeit;

        Vector3 desiredPos = new Vector3(middlePos.x, middlePos.y + yOffset, -10.0f) + playerV;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);
    }

    public void SetTarget(MP_PlayerMovement newTarget)
    {
        target = newTarget;
    }
}
