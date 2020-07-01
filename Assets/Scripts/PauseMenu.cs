using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider dialogSlider;
    
    void Start()
    {
       /* float volume;
        {
            bool res = AudioManager.instance.mixer.GetFloat("MasterVolume", out volume);
            if (res)
                masterSlider.SetValueWithoutNotify(volume);
        }
        {
            bool res = AudioManager.instance.mixer.GetFloat("DialogueVolume", out volume);
            if(res)
                dialogSlider.SetValueWithoutNotify(volume);
        }
        {
            bool res = AudioManager.instance.mixer.GetFloat("MusicVolume", out volume);
            if(res)
                musicSlider.SetValueWithoutNotify(volume);
        }*/
        pauseMenu.SetActive(false);
        
        //AudioManager.instance.PlayMusic("Heroic");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        AudioManager.instance.PauseVoicever();
        CursorManager.Instance.BlockCursor();
        pauseMenu.SetActive(true);
    }

    private void Resume()
    {
        if(!DialogueManager.Instance.isDialogPlaying)
            CursorManager.Instance.UnlockCursor();
        Time.timeScale = 1f;
        gamePaused = false;
        AudioManager.instance.UnpauseVoiceover();
        pauseMenu.SetActive(false);
    }
}
