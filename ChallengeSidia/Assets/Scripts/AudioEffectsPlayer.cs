using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectsPlayer : MonoBehaviour
{
    AudioSource audioSrc;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayAudio(int index)
    {
        audioSrc.clip = clips[index];
        audioSrc.Play();
    }
}
