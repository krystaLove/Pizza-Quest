using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Public Fields

    public enum AudioChannel { Master, Dialogue, Reaction, Music, VFX }
    public enum SourceType { Dialogue, Reaction, Music, VFX }

    public static AudioManager instance;

    public AudioMixer mixer;

    public AudioMixerGroup masterGroup;
    public AudioMixerGroup dialogueGroup;
    public AudioMixerGroup reactionGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup VFXGroup;

    public AudioClip[] VFXClips;

    #endregion Public Fields

    #region Private Fields

    private AudioSource dialogueSource;
    private AudioSource reactionSource;
    private AudioSource musicSource;
    private AudioSource VFXSource;

    #endregion Private Fields

    #region Settings Fields

    private const float kDefaultDialogueVolume = 1f;
    private const float kDefaultReactionVolume = 1f;
    private const float kDefaultMusicVolume = 0.5f;
    private const float kDialogMusicVolume = 0.2f;
    private const float kDefaultVFXVolume = 1f;
    private const float kVolumeToMuteMusic = 0f;
    private const float kFadeDuration = 0.5f; // It's fading time.

    #endregion Settings Fields

    #region Initialization Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DialogueManager.Instance.OnDialogStart += (sender, args) => StartCoroutine(Fade(kDialogMusicVolume, 0.5f));
        DialogueManager.Instance.OnDialogFinish += (sender, args) => StartCoroutine(Fade(kDefaultMusicVolume, 0.5f));
        CreateAudioSources();
        SettingSourceFieldValues();
        AssignChannelsToSources();
        SetChannelVolume();
    }

    private void CreateAudioSources()
    {
        dialogueSource = gameObject.AddComponent<AudioSource>();
        reactionSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        VFXSource = gameObject.AddComponent<AudioSource>();
    }

    private void SettingSourceFieldValues()
    {
        dialogueSource.volume = kDefaultDialogueVolume;
        reactionSource.volume = kDefaultReactionVolume;
        musicSource.volume = kDefaultMusicVolume;
        VFXSource.volume = kDefaultVFXVolume;

        musicSource.loop = true;
    }

    private void AssignChannelsToSources()
    {
        dialogueSource.outputAudioMixerGroup = dialogueGroup;
        reactionSource.outputAudioMixerGroup = dialogueGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        VFXSource.outputAudioMixerGroup = VFXGroup;
    }

    private void SetChannelVolume()
    {
        InstanceSetChannelVolume(AudioChannel.Master, PlayerPrefs.GetFloat("MasterVolume", 0));
        InstanceSetChannelVolume(AudioChannel.Dialogue, PlayerPrefs.GetFloat("DialogueVolume", 0));
        InstanceSetChannelVolume(AudioChannel.Reaction, PlayerPrefs.GetFloat("ReactionVolume", 0));
        InstanceSetChannelVolume(AudioChannel.Music, PlayerPrefs.GetFloat("MusicVolume", 0));
        InstanceSetChannelVolume(AudioChannel.VFX, PlayerPrefs.GetFloat("VFXVolume", 0));
    }

    #endregion Initialization Methods

    #region Private Methods

    public IEnumerator InstancePlayReactionVoiceover(AudioClip clip)
    {
        if (clip == null)
            yield break;
        reactionSource.clip = clip;
        if (musicSource.clip != null)
        {
            StartCoroutine(Fade(0.15f, 0.5f));
            //musicSource.Stop();
            //StartCoroutine(InstanceStopMusic()); // Or the music fades out.
        }
        reactionSource.Play();
        yield return new WaitForSeconds(reactionSource.clip.length);
        StartCoroutine(Fade(kDefaultMusicVolume, 0.5f));
    }

    public void InstancePlayDialogueVoiceover(AudioClip clip)
    {
        if (clip == null)
            return;
        dialogueSource.clip = clip;
        dialogueSource.Play();
    }

    public void InstanceStopDialogueVoiceover()
    {
        if(dialogueSource.clip != null)
            dialogueSource.Stop();
    }

    private void InstancePlayMusic(AudioClip clip, bool fadeIn = true)
    {
        if (clip == null)
            return;
        musicSource.clip = clip;
        if (fadeIn)
        {
            musicSource.volume = 0f;
            musicSource.Play();
            StartCoroutine(Fade(kDefaultMusicVolume, 0.5f));
        }
        else
        {
            musicSource.volume = kDefaultMusicVolume;
            musicSource.Play();
        }
    }
    

    private IEnumerator InstanceStopMusic(bool fadeOut = true)
    {
        if (fadeOut)
            yield return StartCoroutine(Fade(kVolumeToMuteMusic, 0.5f));
        musicSource.Stop();
    }

    private void InstancePlayEffect(string clipName)
    {
        foreach (AudioClip clip in VFXClips)
            if (clip.name == clipName)
            {
                VFXSource.clip = clip;
                VFXSource.Play();
                break;
            }
    }

    public void InstanceSetClip(AudioClip clip, SourceType channel)
    {
        switch (channel)
        {
            case SourceType.Dialogue:
                instance.dialogueSource.clip = clip;
                break;
            case SourceType.Reaction:
                instance.reactionSource.clip = clip;
                break;
            case SourceType.Music:
                instance.musicSource.clip = clip;
                break;
            case SourceType.VFX:
                instance.VFXSource.clip = clip;
                break;
        }
    }

    private void InstanceSetChannelVolume(AudioChannel channel, float volume)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                mixer.SetFloat("MasterVolume", Mathf.Log(volume) * 20);
                break;
            case AudioChannel.Dialogue:
                mixer.SetFloat("DialogueVolume", Mathf.Log(volume) * 20);
                break;
            case AudioChannel.Reaction:
                mixer.SetFloat("ReactionVolume", Mathf.Log(volume) * 20);
                break;
            case AudioChannel.Music:
                mixer.SetFloat("MusicVolume", Mathf.Log(volume) * 20);
                break;
            case AudioChannel.VFX:
                mixer.SetFloat("VFXVolume", Mathf.Log(volume) * 20);
                break;
        }
    }

    private IEnumerator Fade(float targetVolume, float duration = kFadeDuration)
    {
        float currentTime = 0;
        float startVolume = musicSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
    }

    #endregion Private Methods

    #region Public Methods

    public static void PlayReactionVoiceover(AudioClip clip)
    {
        instance.StartCoroutine(instance.InstancePlayReactionVoiceover(clip));
    }

    public static void PlayDialogueVoiceover(AudioClip clip)
    {
        instance.InstancePlayDialogueVoiceover(clip);
    }

    public void PauseVoicever()
    {
        dialogueSource.Pause();
        reactionSource.Pause();
    }

    public void UnpauseVoiceover()
    {
        dialogueSource.UnPause();
        reactionSource.UnPause();
    }
    

    public static void PlayMusic(bool fadeIn = true)
    {
        PlayMusic(instance.musicSource.clip, fadeIn);
    }

    public static void PlayMusic(AudioClip clip, bool fadeIn = true)
    {
        instance.InstancePlayMusic(clip, fadeIn);
    }

    public static void StopMusic(bool fadeOut = true)
    {
        instance.StartCoroutine(instance.InstanceStopMusic(fadeOut));
    }

    public static void PlayEffect(string clipName)
    {
        instance.InstancePlayEffect(clipName);
    }

    public static void SetClip(AudioClip clip, SourceType channel)
    {
        instance.InstanceSetClip(clip, channel);
    }

    // To mute, the volume must be set to 0.001.
    public static void SetChannelVolume(AudioChannel channel, float volume)
    {
        instance.InstanceSetChannelVolume(channel, volume);
    }

    #endregion Public Methods

    #region Slider Methods

    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetReactionVolume(float volume)
    {
        mixer.SetFloat("Reaction", volume);
        PlayerPrefs.SetFloat("Reaction", volume);
    }

    public void SetDialogueVolume(float volume)
    {
        mixer.SetFloat("DialogueVolume", volume);
        PlayerPrefs.SetFloat("DialogueVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectVolume(float volume)
    {
        mixer.SetFloat("VFXVolume", volume);
        PlayerPrefs.SetFloat("VFXVolume", volume);
    }

    #endregion Slider Methods
}
