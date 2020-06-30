using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRocket : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        if (GameManager.Instance.chosenItem == GameItem.ItemType.Gas)
        {
            return progress[1];
        }
        
        if (GameManager.Instance.chosenItem == GameItem.ItemType.None)
        {
            return progress[0];
        }
        else
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }
    }
}
