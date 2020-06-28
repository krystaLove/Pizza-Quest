using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchInTrigger : MonoBehaviour
{
    [SerializeField] public GameObject churchMan;
    private bool triggered = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || triggered)
            return;

        triggered = true;
        
        DialogueManager.Instance.SetDialogue(churchMan.GetComponent<Person>().GetNextDialog());
        DialogueManager.Instance.SetCameraPosition(churchMan.GetComponent<Person>().cameraPosition, churchMan.GetComponent<Person>().camSize);
        DialogueManager.Instance.StartDialogueWithInnerCoroutine();
        
        gameObject.SetActive(false);
    }
}
