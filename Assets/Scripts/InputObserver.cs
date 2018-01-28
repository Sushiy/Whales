using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class InputObserver : MonoBehaviour {

    public static InputObserver instance;
    public GameObject wave;
    public GameObject waveRed;

    bool b1FakeCondition;
    bool b2FakeCondition;
    

    //Velocity Counter: Pressing the Velocity Button 


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    // Use this for initialization
    void Start () {

        b1FakeCondition = true;
        b2FakeCondition = true;


        InputManager.instance.direction.Subscribe(dir => ShowDirection(dir));

        //InputManager.instance.button1.Where(var => var == true).TakeLast(1).Subscribe(val => Debug.Log("Button 1 changed to " + val));

        // A Bubble-Prefab gets created on this point 
        InputManager.instance.button1.Subscribe(val =>
        {

            if (val == true && b1FakeCondition == true)
            {
                Instantiate(wave, PlayerMovement.instance.transform.GetChild(0).transform.position, Quaternion.identity);
                b1FakeCondition = false;
                b2FakeCondition = false;
            }
        });
        
        InputManager.instance.button2.Subscribe(val =>

        { if (val == true && b2FakeCondition == true)
            {
                Instantiate(waveRed, PlayerMovement.instance.transform.GetChild(0).transform.position, Quaternion.identity);
                b1FakeCondition = false;
                b2FakeCondition = false;
            }
        });

        InputManager.instance.button3.Subscribe(val => {

            if (val)
                PlayerButtonMovement.instance.paddle();

        });

       // InputManager.instance.button4.Subscribe(val => Debug.Log("Button 4 changed to " + val));
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    void ShowDirection(Vector2 direction)
    {
        Debug.DrawRay(Vector3.zero, direction);
    }

    public void setB1True()
    {
        b1FakeCondition = true;
    }

    public void setB2True()
    {
        b2FakeCondition = true;
    }
    public void setB3True()
    {
       // b3FakeCondition = true;
    }
    public void setB4True()
    {
        //b4FakeCondition = true;
    }


}
