using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

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
}

