using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_DetroyOnHit : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider.gameObject.layer == 8)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
