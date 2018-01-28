using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISender {



    // Use this for initialization
    void send(IReceiver receiver, int message);
}
