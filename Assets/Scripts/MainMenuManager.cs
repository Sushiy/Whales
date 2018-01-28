using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject panelHow;
    public GameObject panelHow2;
    public GameObject panelStory;
    public GameObject panelCredits;


    public Button start;
    public Button howto;
    public Button story;
    public Button credits;
    public Button quit;



    // Use this for initialization
    void Start()
    {

        start.onClick.AddListener(newGame);
        howto.onClick.AddListener(onHowToButtonPressed);
        story.onClick.AddListener(onStoryButtonPressed);
        credits.onClick.AddListener(onCreditsButtonPressed);
        quit.onClick.AddListener(onQuitButtonPressed);

     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void newGame()
    {
        SceneManager.LoadScene("Level1");
            
	}

    public void onQuitButtonPressed()
    {
        Application.Quit();

    }

    public void onCreditsButtonPressed()
    {
        panelCredits.gameObject.SetActive(true);
    }

    public void onStoryButtonPressed()
    {
        panelStory.gameObject.SetActive(true);
    }



    public void onHowToButtonPressed()
    {
        panelHow.gameObject.SetActive(true);
        panelHow2.gameObject.SetActive(true);
    }


}
