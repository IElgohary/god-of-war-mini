using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {


    public GameObject HUD;
    public GameObject Pause;
    public GameObject Upgrade;

    bool game_Paused;

	// Use this for initialization
	void Start () {
        game_Paused = false;
        Resume();
	}
	
	// Update is called once per frame
	void Update () {
        Check_Escape();
    }


    void Check_Escape() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (game_Paused)
                Resume(); 
            else
                To_Pause(); 

        }
    }


    void Hide_Everything() {
        HUD.SetActive(false);
        Pause.SetActive(false);
        Upgrade.SetActive(false);
    }


    public void Resume() {
        Hide_Everything();
        HUD.SetActive(true);
        Time.timeScale = 1.0f;
        game_Paused = false;
    }


    public void To_Pause() {
        Hide_Everything();
        Pause.SetActive(true);
        Time.timeScale = 0.0f;
        game_Paused = true;
    }


    public void To_Upgrade() {
        Hide_Everything();
        Upgrade.SetActive(true);
        Time.timeScale = 0.0f;
        game_Paused = true;
    }

}
