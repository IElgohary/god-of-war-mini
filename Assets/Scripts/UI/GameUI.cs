using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameUI : MonoBehaviour {

    public GameObject HUD;
    public GameObject Pause;
    public GameObject Upgrade;
    public GameObject GameOver;

    public AudioSource gameoverMusic;
    public AudioSource pauseMusic;

    public AudioSource normalMusic;
    public AudioSource bossMusic;

    AudioSource currMusic;

    public bool game_Paused;

    bool UI_Exists;
    public bool bossLevel;

    private void Awake()
    {
        if (!UI_Exists)
        {
            
            UI_Exists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            bossLevel = true;
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        currMusic = normalMusic;
        game_Paused = false;
        Resume();
	}
	
	// Update is called once per frame
	void Update () {
        Check_Escape();
    }

    public void switch_music() {
        currMusic.Stop();
        currMusic = bossMusic;
        currMusic.Play();
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

    public void To_GameOver() {
        currMusic.Stop();
        gameoverMusic.Play();
        Hide_Everything();
        GameOver.SetActive(true);
        game_Paused = true;
        
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
        pauseMusic.Stop();
        currMusic.Play();
    }


    public void To_Pause() {
        currMusic.Stop();
        if(!game_Paused)
            pauseMusic.Play();
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
