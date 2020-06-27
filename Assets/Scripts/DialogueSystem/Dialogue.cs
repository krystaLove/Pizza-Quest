using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Dialogue
{
    [Serializable]
    public class DialogueObject
    {
        public string sentence;
        public AudioClip voiceOver;
    }
    
    [SerializeField] public List<DialogueObject> dialogueObjects;
    [SerializeField] public UnityEvent OnDialogEndEvent;
}

[Serializable]
public class GetItemDialog
{
    public GameItem.ItemType neededItem;
    public Dialogue dialogue;
    public GameItem.ItemType givingItem;


}

