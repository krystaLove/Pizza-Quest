using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Person : MonoBehaviour
{
    [SerializeField] public int nextDialog;
    [SerializeField] public List<Dialogue> dialogues;
    
    public Dialogue GetNextDialog()
    {
        Dialogue diag = dialogues[nextDialog];
        nextDialog = Math.Min(nextDialog + 1, dialogues.Count - 1);

        return diag;
    }
}