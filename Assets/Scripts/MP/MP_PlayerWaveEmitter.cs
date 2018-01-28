using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Networking;
public class MP_PlayerWaveEmitter : NetworkBehaviour
{
    public GameObject wave;
    public GameObject waveRed;

    bool b1FakeCondition;
    bool b2FakeCondition;

    float waveDelay = 2.0f;
    // Use this for initialization
    void Start ()
    {
        if (!isLocalPlayer ||!isClient)
            return;

        b1FakeCondition = true;
        b2FakeCondition = true;

        
        // A Bubble-Prefab gets created on this point 
        InputManager.instance.button1.Subscribe(val =>
        {
            //Debug.Log("Waveblue");
            if (val == true && b1FakeCondition == true)
            {
                //Debug.Log("Waveblue");
                CmdSpawnWave(false);
                StartCoroutine(DelayWaves());
                
            }
        });

        InputManager.instance.button2.Subscribe(val =>
        {
            if (val == true && b2FakeCondition == true)
            {
                //Debug.Log("Wavered");
                CmdSpawnWave(true);
                StartCoroutine(DelayWaves());
            }
        });
    }


    IEnumerator DelayWaves()
    {

        b1FakeCondition = false;
        b2FakeCondition = false;
        yield return new WaitForSeconds(waveDelay);

        b1FakeCondition = true;
        b2FakeCondition = true;
    }

    [Command]
    void CmdSpawnWave(bool red)
    {
        GameObject go;
        if(red)
            go = Instantiate(waveRed, transform.GetChild(0).position, Quaternion.identity);
        else
            go = Instantiate(wave, transform.GetChild(0).position, Quaternion.identity);

        NetworkServer.Spawn(go);
    }
}
