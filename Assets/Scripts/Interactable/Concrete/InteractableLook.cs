using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLook : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        if (GameManager.Instance.chosenItem == GameItem.ItemType.None)
        {
            return progress[0];
        }
        else
        {
            return commonReaction;
        }
    }
}
