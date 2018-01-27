using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public ReactiveProperty<Vector2> direction; // analog stick
    public ReactiveProperty<bool> button1;
    public ReactiveProperty<bool> button2;
    public ReactiveProperty<bool> button3;
    public ReactiveProperty<bool> button4;



    // Use this for initialization
    void Awake () {

        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        direction = new ReactiveProperty<Vector2>();

        button1 = new ReactiveProperty<bool>();
        button2 = new ReactiveProperty<bool>();
        button3 = new ReactiveProperty<bool>();
        button4 = new ReactiveProperty<bool>();


        direction.Value = new Vector2(0.0f, 0.0f);
        button1.Value = false;
        button2.Value = false;
        button3.Value = false;
        button4.Value = false;
    }

    void Start()
    {
       

    }

    // Update is called once per frame
    void Update () {

        button1.Value = false;
        button2.Value = false;
        button3.Value = false;
        button4.Value = false;

        if (Input.GetAxis("1_Button1") >=1.0f)
        {
            button1.Value = true;
        }

        if (Input.GetAxis("1_Button2") >= 1.0f)
        {
            button2.Value = true;
        }

        if (Input.GetAxis("1_Button3") >= 1.0f)
        {
            button3.Value = true;
        }

        if (Input.GetAxis("1_Button4") >= 1.0f)
        {
            button4.Value = true;
        }

        direction.Value = new Vector2(Input.GetAxis("1_Horizontal")  ,-Input.GetAxis("1_Vertical"));
    }

   
}
