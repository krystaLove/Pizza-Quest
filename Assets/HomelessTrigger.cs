using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomelessTrigger : MonoBehaviour
{
    private bool _triggered = false;

    public Person person;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _triggered)
            return;

        _triggered = true;
        
        DialogueManager.Instance.SetDialogue(person.GetNextDialog());
        DialogueManager.Instance.SetCameraPosition(null, -1f);
        StartCoroutine(DialogueManager.Instance.StartDialogue());
    }
}
