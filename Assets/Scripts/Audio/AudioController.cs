using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1) {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public static IEnumerator FadeTo(AudioSource audioSource, float FadeTime, float volume)
    {
        if (volume > audioSource.volume)
        {
            while (audioSource.volume < volume) {
                audioSource.volume += Time.deltaTime / FadeTime;
                yield return null;
            }
        }
        else
        {
            while (audioSource.volume > volume) {
                audioSource.volume -= Time.deltaTime / FadeTime;
                yield return null;
            }
        }
    }
}

