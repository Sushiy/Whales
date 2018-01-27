using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class InputObserver : MonoBehaviour {

    public static InputObserver instance;
    public GameObject wave;

    float b1Cooldown;
    public bool b1FakeCondition;

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

        InputManager.instance.direction.Subscribe(dir => ShowDirection(dir));

        //InputManager.instance.button1.Where(var => var == true).TakeLast(1).Subscribe(val => Debug.Log("Button 1 changed to " + val));

        // A Bubble-Prefab gets created on this point 
        InputManager.instance.button1.Subscribe(val =>
        {

            if (val == true && b1FakeCondition == true)
            {
                Instantiate(wave, PlayerMovement.instance.transform.GetChild(0).transform.position, Quaternion.identity);
                b1FakeCondition = false;
            }
        });
        
        InputManager.instance.button2.Subscribe(val => Debug.Log("Button 2 changed to " + val));

        InputManager.instance.button3.Subscribe(val => Debug.Log("Button 3 changed to " + val));

        InputManager.instance.button4.Subscribe(val => Debug.Log("Button 4 changed to " + val));
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
}
