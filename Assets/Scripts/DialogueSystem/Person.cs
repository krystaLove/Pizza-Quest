﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

[Serializable]
public class Person : MonoBehaviour
{
    [SerializeField] public int nextDialog;
    [SerializeField] public List<Dialogue> dialogues;
    [SerializeField] public GetItemDialog itemDialog;
    [SerializeField] public Dialogue dialogueAfterGettingItem;
    public bool gotItem = false;

    [SerializeField] public Reaction afterGettingItemReaction;

    private void Start()
    {
        itemDialog.dialogue.OnDialogEndEvent.AddListener(_unlockItem);
    }

    public Dialogue GetNextDialog()
    {
        Dialogue diag;
        if (!gotItem && GameManager.Instance.chosenItem == itemDialog.neededItem)
        {
            gotItem = true;
            diag = itemDialog.dialogue;
        }
        else
        {
            if (gotItem)
            {
                diag = dialogueAfterGettingItem;
            }
            else
            {
                diag = dialogues[nextDialog];
                nextDialog = Math.Min(nextDialog + 1, dialogues.Count - 1);
            }
            
        }
        
        return diag;
    }

    private void _unlockItem()
    {
        GameManager.Instance.inventory.RemoveItem(GameManager.Instance.chosenItemIndex);
        GameManager.Instance.inventory.AddItem(new GameItem{itemType = itemDialog.givingItem});
    }
}