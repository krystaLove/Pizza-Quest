using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public AudioMixerGroup mixerGroup;

    private AudioSource source;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.playOnAwake = false;
        source.outputAudioMixerGroup = mixerGroup;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        if(source.isPlaying)
            source.Stop();
    }
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public Sound[] music;
    public Sound[] sounds;

    public AudioMixer masterMixer;
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;

    private int playingIndex = -1;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        CreateAudioSources(music);
        CreateAudioSources(sounds);
    }

    private void CreateAudioSources(Sound[] sounds)
    {
        foreach (Sound sound in sounds)
            sound.SetSource(gameObject.AddComponent<AudioSource>());
    }

    public void PlaySound(string nameSound)
    {
        Sound s_tmp = Array.Find(sounds, sound => sound.name == nameSound);
        if (s_tmp == null)
        {
            Debug.Log("Sound" + nameSound + " not found");
            return;
        }
        s_tmp.Play();
    }

    public void PlayMusic(string nameSound)
    {
        int index = Array.FindIndex(music, sound => sound.name == nameSound);
        if (index == -1)
        {
            Debug.Log("Music" + nameSound + " not found");
            return;
        }
        if (playingIndex != -1)
            music[playingIndex].Stop();
        playingIndex = index;
        music[index].Play();
    }

    public void StopMusic()
    {
        if (playingIndex != -1)
        {
            music[playingIndex].Stop();
            playingIndex = -1;
        }
    }

    public void ChangeVolume(float volume)
    {
        masterMixer.SetFloat("masterVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        masterMixer.SetFloat("musicVolume", volume);
    }

    public void ChangeSoundVolume(float volume)
    {
        masterMixer.SetFloat("soundVolume", volume);
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
            paused.TransitionTo(.01f);
        else
            unpaused.TransitionTo(.01f);
    }
    public void UnPause()
    {
        Pause();
    }
}

