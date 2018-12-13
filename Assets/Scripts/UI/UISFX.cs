using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour {

    public AudioSource sfx;



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
