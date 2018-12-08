using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour {

    public AudioSource sfx;

    // Use this for initialization
    void Start () {
        sfx.volume = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HoverSFX(AudioClip hoverSFX)
    {
        
        sfx.PlayOneShot(hoverSFX);
    }

    public void ClickSFX(AudioClip clickSFX)
    {
        sfx.PlayOneShot(clickSFX);
    }

    public void SlideSFX(AudioClip slideSFX)
    {
        sfx.PlayOneShot(slideSFX);

    }

}
