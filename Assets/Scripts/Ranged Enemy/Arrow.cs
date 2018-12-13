using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    // Use this for initialization
    void Start () {
        StartCoroutine(destroy());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
