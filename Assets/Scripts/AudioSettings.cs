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

    public TMPro.TextMeshProUGUI musicVal;
    public TMPro.TextMeshProUGUI speechVal;
    public TMPro.TextMeshProUGUI fxVal;

    // Use this for initialization
    void Start () {
        music.value = -5;
        speech.value = -5;
        fx.value = -5;
	}
	

    public void changeMusicVol() {
        mixer.SetFloat("musicVol", music.value);
        PlayerPrefs.SetFloat("musicVol", music.value);
        musicVal.text = ((int)music.value).ToString() + " dB";
    }

    public void changeSpeechVol() {
        mixer.SetFloat("speechVol", speech.value);
        PlayerPrefs.SetFloat("speechVol", speech.value);
        speechVal.text = ((int)speech.value).ToString() + " dB";
    }

    public void changeFXVol()
    {
        mixer.SetFloat("fxVol", fx.value);
        PlayerPrefs.SetFloat("fxVol", fx.value);
        fxVal.text = ((int)fx.value).ToString() + " dB";
    }
}
