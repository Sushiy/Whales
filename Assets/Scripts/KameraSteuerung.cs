using UnityEngine;
using System.Collections;

public class KameraSteuerung : MonoBehaviour
{
    private GameObject spieler;
    [SerializeField]
    private float kameraEntfernung = 20f;
    [SerializeField]
    private float geschwindigkeit = 0.5f;
    [SerializeField]
    private float yOffset = 1f;

    private float LerpSpeed = 3f;

    [HideInInspector]
    public bool Follow = true;

    // Use this for initialization
    void Start()
    {
        spieler = GameObject.FindGameObjectWithTag("Spieler");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Follow)
            return;

        //Kamera verfolgt die Position des Spielers

        Vector2 middlePos;
        Vector2 playerPos = spieler.transform.position;
        middlePos = playerPos;
        //Aktuelle Spielergeschwindigkeit wird eingerechnet
        Vector3 playerV = spieler.GetComponent<Rigidbody2D>().velocity;
        playerV *= geschwindigkeit;
        
        Vector3 desiredPos = new Vector3(middlePos.x, middlePos.y + yOffset, -kameraEntfernung) + playerV;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime);
    }
}