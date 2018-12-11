using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {

    public AudioMixer mixer;
    public Slider music;
    public Slider speech;
    public Slider fx;

	// Use this for initialization
	void Start () {
        music.value = -5;
        speech.value = -5;
        fx.value = -5;
	}
	
	// Update is called once per frame
	void Update () {
		mixer.SetFloat("musicVol", music.value);
        PlayerPrefs.SetFloat("musicVol", music.value);

        mixer.SetFloat("speechVol", speech.value);
        PlayerPrefs.SetFloat("speechVol", speech.value);

        mixer.SetFloat("fxVol", fx.value);
        PlayerPrefs.SetFloat("fxVol", fx.value);
    }
}
