using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterReceiver : MonoBehaviour, IReceiver
{    
    public void receive(int message, Vector2 position)
    {
        Debug.Log("I got mail!");
        GetComponentInParent<HunterBehaviour>().focusedTarget = position;
    }
}
