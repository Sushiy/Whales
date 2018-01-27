using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {


    public Button start;

    // Use this for initialization
    void Start()
    {

        start.onClick.AddListener(newGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void newGame()
    {
        Debug.Log("button1 pressed");
        SceneManager.LoadScene("Level1");
            
	}
    

}
