using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    public GameObject main;
    public GameObject play;
    public GameObject options;

    public Text tx, ty;

    public SOgameInfo gi;

    public AudioMixer mixerMusic;
    public Slider musicSlider, effectsSlider;
    public AudioMixer mixerEffects;

    public AudioSource fxAudio;

    private void Start()
    {
        musicSlider.value = gi.musicVol;
        effectsSlider.value = gi.effectsVol;
        mixerMusic.SetFloat("MasterVol", Mathf.Log10(gi.musicVol) * 20);
        mixerEffects.SetFloat("EffectsVol", Mathf.Log10(gi.effectsVol) * 20);
    }

    public void Play()
    {
        fxAudio.Play();
        main.SetActive(false);
        play.SetActive(true);
    }

    public void StartPlay()
    {
        fxAudio.Play();

        int x = 16, y = 16;
        if (tx.text != "")
        {
            x = int.Parse(tx.text);
            if (x < 4) x = 4;
            if (x > 32) x = 32;
        }
        if (ty.text != "")
        {
            y = int.Parse(ty.text);
            if (y < 4) y = 4;
            if (y > 32) y = 32;
        }

        gi.gridX = x;
        gi.gridY = y;
        gi.multiplayer = true;

        SceneManager.LoadScene("GameScene");
    }

    public void Back()
    {
        fxAudio.Play();
        main.SetActive(true);
        play.SetActive(false);
        options.SetActive(false);
    }

    public void Options()
    {
        fxAudio.Play();
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Quit()
    {
        fxAudio.Play();
        Application.Quit();
    }

    public void SetVolume(float value)
    {
        mixerMusic.SetFloat("MasterVol", Mathf.Log10(value)*20);
        gi.musicVol = value;
    }

    public void SetVolumeEffects(float value)
    {
        mixerEffects.SetFloat("EffectsVol", Mathf.Log10(value) * 20);
        gi.effectsVol = value;
    }
}
