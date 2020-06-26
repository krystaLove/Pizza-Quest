using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[Serializable]
public class DialogueVoiceOver : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isPlaying = false;
    public static DialogueVoiceOver Instance { get; private set; }
    

    public void Awake()
    {
        Instance = this;
    }

    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    private void Update()
    {
        
    }

    public void Play()
    {
        Music.Instance.FadeToVolume(Music.Instance.dialogVolume);
        if (audioSource.clip != null)
        {
            isPlaying = true;
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
}
