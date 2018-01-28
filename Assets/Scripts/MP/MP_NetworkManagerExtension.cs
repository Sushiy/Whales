using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_NetworkManagerExtension : NetworkManager
{ 
    public GameObject player1;

    public GameObject player2;

    public GameObject prefab1;
    public GameObject prefab2;

    int index = 1;
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        GameObject player;
        Transform startPos = GetStartPosition();

        if (index == 1)
        {
            player = Instantiate(prefab1, startPos.position, startPos.rotation) as GameObject;
            index = 2;
        }
        else
        {
            player = Instantiate(prefab2, startPos.position, startPos.rotation) as GameObject;

        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
