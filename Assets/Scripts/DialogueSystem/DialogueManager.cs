using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject continueButton;
    public TextMeshProUGUI textDisplay;
    public Animator dialogBoxAnimator;

    public bool isDialogPlaying;

    public event EventHandler OnDialogStart;
    public event EventHandler OnDialogFinish;
    
    public float typingSpeed;

    private int _index;

    private Dialogue _currentDialogue = null;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator StartDialogue()
    {
        isDialogPlaying = true;
        OnDialogStart?.Invoke(this, null);
        
        GameManager.Instance.BlockMove();
        
        dialogBoxAnimator.SetBool("Start", true);
        yield return new WaitForSeconds(dialogBoxAnimator.GetCurrentAnimatorStateInfo(0).length);

        textDisplay.text = "";
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (_currentDialogue != null && textDisplay.text == _currentDialogue.dialogueObjects[_index].sentence)
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        var dialogueObject = _currentDialogue.dialogueObjects[_index];
        
        if (dialogueObject.voiceOver != null)
        {
            DialogueVoiceOver.Instance.SetAudioClip(dialogueObject.voiceOver);
            DialogueVoiceOver.Instance.Play();
        }
        
        foreach (char letter in dialogueObject.sentence)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        DialogueVoiceOver.Instance.Stop();
        continueButton.SetActive(false);
        if (_index < _currentDialogue.dialogueObjects.Count - 1)
        {
            _index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            StartCoroutine(EndDialog());
            textDisplay.text = "";
            continueButton.SetActive(false);
            
            GameManager.Instance.AllowMove();
        }
    }

    public void SetDialogue(Dialogue d)
    {
        _currentDialogue = d;
        _index = 0;
    }

    IEnumerator EndDialog()
    {
        dialogBoxAnimator.SetBool("Start", false);
        yield return new WaitForSeconds(dialogBoxAnimator.GetCurrentAnimatorStateInfo(0).length);

       _currentDialogue.OnDialogEndEvent.Invoke();
       OnDialogFinish?.Invoke(this, null);
       isDialogPlaying = false;
    }
}
