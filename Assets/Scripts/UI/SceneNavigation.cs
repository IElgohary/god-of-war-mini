using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string SceneName) {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Game UI"));
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        // Destroy Game Manager
        SceneManager.LoadScene(SceneName);
    }
    

}
