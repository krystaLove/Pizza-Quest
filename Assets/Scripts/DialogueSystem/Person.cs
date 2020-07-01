using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

[Serializable]
public class Person : MonoBehaviour
{
    [SerializeField] public bool faceRight = true;
    [SerializeField] public int nextDialog;
    [SerializeField] public List<Dialogue> dialogues;
    [SerializeField] public bool isNeedItem;
    [SerializeField] public bool restartDialogBeforeItem = false;
    [SerializeField] public GetItemDialog itemDialog;
    [SerializeField] public Dialogue dialogueAfterGettingItem;

    public bool gotItem = false;

    [SerializeField] public Reaction afterGettingItemReaction;
    
    [Header("Camera")]
    public Transform cameraPosition;
    public float camSize;

    private bool _isLastDialogPlayed = false;

    private void Start()
    {
        itemDialog.dialogue.OnDialogEndEvent.AddListener(_unlockItem);
    }

    public virtual void Action()
    {
        GameManager.Instance.playerController.Flip(faceRight);
        if (itemDialog.neededItem != GameManager.Instance.chosenItem && GameManager.Instance.chosenItem != GameItem.ItemType.None)
        {
            GameManager.Instance.ResetSelectedItem(false);
            ReactionAssets.Instance.GetNotMatchItemReaction().Do();
            return;
        }
        
        if (IsNoNeedToDialog())
        {
            afterGettingItemReaction.Do();
            return;
        }

        DialogueManager.Instance.SetDialogue(GetNextDialog());
        DialogueManager.Instance.SetCameraPosition(cameraPosition, camSize);
        StartCoroutine(DialogueManager.Instance.StartDialogue());
    }

    public Dialogue GetNextDialog()
    {
        Dialogue diag;
        if (!gotItem && GameManager.Instance.chosenItem == itemDialog.neededItem && isNeedItem)
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
                if (nextDialog + 1 >= dialogues.Count)
                    _isLastDialogPlayed = true;
                nextDialog = Math.Min(nextDialog + 1, dialogues.Count - 1);
            }
            
        }
        
        return diag;
    }

    public bool IsNoNeedToDialog()
    {
        return (_isLastDialogPlayed && !isNeedItem) || gotItem;
        
        //return _isLastDialogPlayed && !restartDialogBeforeItem && !isNeedItem || gotItem;
        //return _isLastDialogPlayed && !restartDialogBeforeItem || gotItem;
    }

    private void _unlockItem()
    {
        GameManager.Instance.inventory.RemoveItem(GameManager.Instance.chosenItemIndex);
        foreach (var item in itemDialog.givingItem)
        {
            GameManager.Instance.inventory.AddItem(new GameItem{itemType = item});
        }
        
    }

    public void GoToPositionToSpeak()
    {
        GameManager.Instance.playerController.GoTo(gameObject.GetComponent<ClickablePerson>().positionToStep.position);
    }

    public void StartFirstDialogue()
    {
        DialogueManager.Instance.SetDialogue(dialogues[0]);
        DialogueManager.Instance.SetCameraPosition(cameraPosition, camSize);
        StartCoroutine(DialogueManager.Instance.StartDialogue());
    }
}