using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueVoiceOver : MonoBehaviour
{
    public AudioSource audioSource;

    public static DialogueVoiceOver Instance { get; private set; }
    

    public void Awake()
    {
        Instance = this;
    }

    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void Play()
    {
        if(audioSource.clip != null) audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource != null) audioSource.Stop();
    }
}
