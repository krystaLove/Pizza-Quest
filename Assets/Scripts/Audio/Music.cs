using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;

    public float normalVolume;
    public float dialogVolume;
    public static Music Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DialogueManager.Instance.OnDialogStart += (sender, args) => FadeToVolume(dialogVolume);
        DialogueManager.Instance.OnDialogFinish += (sender, args) => FadeToVolume(normalVolume);
    }

    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.volume = normalVolume;
    }

    public void Play()
    {
        if (audioSource.clip != null)
        {
            StartCoroutine(AudioController.FadeIn(audioSource, 0.5f, normalVolume));
        }
    }

    public void FadeToVolume(float volume)
    {
        StopAllCoroutines();
        if (audioSource.clip != null)
        {
            StartCoroutine(AudioController.FadeTo(audioSource, 0.5f, volume));
        }
    }

    public void Stop()
    {
        if (audioSource != null)
        {
            StartCoroutine(AudioController.FadeOut(audioSource, 0.5f));
        }
    }
}
