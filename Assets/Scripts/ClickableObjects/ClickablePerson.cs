using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClickablePerson : ClickableObject
{
    public override void Action()
    {
        Person person = gameObject.GetComponent<Person>();
        DialogueManager.Instance.SetDialogue(person.GetNextDialog());
        StartCoroutine(DialogueManager.Instance.StartDialogue());
    }
}
