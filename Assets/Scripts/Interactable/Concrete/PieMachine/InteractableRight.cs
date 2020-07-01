using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRight : InteractableObject
{
    public GameObject flourObject;
    public override Reaction GetNextReaction()
    {
        if (UnlockInteractableProgress.Instance.isArrowTriggered && GameManager.Instance.chosenItem == GameItem.ItemType.Bun)
        {
            return progress[1];
        }

        if (GameManager.Instance.chosenItem == GameItem.ItemType.None)
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }

        return progress[0];
    }

    public void ActivateFlourObject()
    {
        flourObject.SetActive(true);
    }
}
