using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class InputObserver : MonoBehaviour {

    public static InputObserver instance;

    //Velocity Counter: Pressing the Velocity Button 
    

    // Use this for initialization
    void Start () {
        InputManager.instance.direction.Subscribe(dir => ShowDirection(dir));

        //InputManager.instance.button1.Where(var => var == true).TakeLast(1).Subscribe(val => Debug.Log("Button 1 changed to " + val));

        InputManager.instance.button1.Subscribe(val => Debug.Log("Button 1 changed to " + val));

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
}
