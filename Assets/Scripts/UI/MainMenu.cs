using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject MainMenu_Object;
    public GameObject OptionsMenu_Object;
    public GameObject AudioSettings_Object;
    public GameObject HowToPlay_Object;
    public GameObject Credits_Object;

    // Use this for initialization
    void Start () {
        Hide_All();
        MainMenu_Object.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Hide_All() {
        MainMenu_Object.SetActive(false);
        OptionsMenu_Object.SetActive(false);
        AudioSettings_Object.SetActive(false);
        HowToPlay_Object.SetActive(false);
        Credits_Object.SetActive(false);
    }

    public void ToHowToPlay() {
        Hide_All();
        HowToPlay_Object.SetActive(true);
    }

    public void ToAudioSettings() {
        Hide_All();
        AudioSettings_Object.SetActive(true);    
    }

    public void ToCredits() {
        // Disable_Everything() AND:
        Debug.Log("Load Credits Scene");
    }

    public void ToMainMenu() {
        Hide_All();
        MainMenu_Object.SetActive(true);   
    }

    public void ToOptions() {
        Hide_All();
        OptionsMenu_Object.SetActive(true);
    }

    public void OnQuit() {
        Application.Quit();
    }
}
