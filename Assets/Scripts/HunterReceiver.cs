using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterReceiver : MonoBehaviour, IReceiver
{    
    public void receive(int message, Vector2 position)
    {
        GetComponentInParent<HunterBehaviour>().focusedTarget = position;
    }
}
