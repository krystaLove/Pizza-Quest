using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Reaction
{
    public string reactionName;
    public bool itemNeeded;
    public bool isVoicing;

    [SerializeField] public List<AudioClip> voiceReactions;
    public bool loop;
    public int loopProgress;
    public GameItem.ItemType neededItemType;
    public GameItem.ItemType giveItem;
    public UnityEvent OnReactionEvent;
    
    public void Do()
    {
        if (isVoicing)
        {
            if (loop)
            {
                DialogueVoiceOver.Instance.SetAudioClip(voiceReactions[loopProgress]);
                loopProgress = (loopProgress + 1) % voiceReactions.Count;
            }
            else
            {
                DialogueVoiceOver.Instance.SetAudioClip(voiceReactions[0]);
            }
        }
        DialogueVoiceOver.Instance.Play();
        OnReactionEvent?.Invoke();

        if (giveItem != GameItem.ItemType.None)
        {
            if(neededItemType != GameItem.ItemType.None)
                GameManager.Instance.inventory.RemoveItemByType(neededItemType);
            
            GameManager.Instance.inventory.AddItem(giveItem);
        }
            
    }

    public bool CheckCondition()
    {
        return GameManager.Instance.chosenItem == neededItemType;
    }
}
