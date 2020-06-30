using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWater : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        GameItem.ItemType item = GameManager.Instance.chosenItem;
        if (item == GameItem.ItemType.EmptyBottle)
        {
            return progress[1];
        } 
        else if (item != GameItem.ItemType.None)
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }

        return progress[0];
    }
}
