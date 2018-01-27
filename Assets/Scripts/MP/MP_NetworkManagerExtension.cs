using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_NetworkManagerExtension : NetworkManager
{ 
    public GameObject player1;

    public GameObject player2;

     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("Adding Player" + playerControllerId);
        base.OnServerAddPlayer(conn, playerControllerId);
        if (conn.playerControllers.Count > 0)
        {
            GameObject player = conn.playerControllers[0].gameObject;
            if(player1 == null)
            {
                player1 = player;
            }
            else if (player2 == null)
            {
                player2 = player;
            }
            else
                Debug.LogWarning("Too many players");
        }
    }
}
