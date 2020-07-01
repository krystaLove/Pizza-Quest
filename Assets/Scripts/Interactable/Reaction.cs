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
            AudioClip audioclip;
            if (loop)
            {

                audioclip = voiceReactions[loopProgress];
                loopProgress = (loopProgress + 1) % voiceReactions.Count;
            }
            else
            {
                audioclip = voiceReactions[0];
            }
            AudioManager.PlayReactionVoiceover(audioclip);
        }
        
        OnReactionEvent?.Invoke();
        
        if(neededItemType != GameItem.ItemType.None)
            GameManager.Instance.inventory.RemoveItemByType(neededItemType);
        
        if (giveItem != GameItem.ItemType.None)
        {
            GameManager.Instance.inventory.AddItem(giveItem);
        }
            
    }

    public bool CheckCondition()
    {
        return GameManager.Instance.chosenItem == neededItemType;
    }
}
