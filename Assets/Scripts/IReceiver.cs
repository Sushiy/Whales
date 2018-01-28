using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver{

    // Use this for initialization
    void receive(int message, Vector2 position);
}
