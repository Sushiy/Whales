using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver{

    // Use this for initialization
    void receive(ISender sender, int message);
}
