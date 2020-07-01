using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableRoulette : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        var item = GameManager.Instance.chosenItem;
        if (item == GameItem.ItemType.MagnetAndCoin)
        {
            return progress[1];
        }
        if (item == GameItem.ItemType.Coin)
        {
            return progress[2];
        }

        if (item != GameItem.ItemType.None)
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }

        return progress[0];
    }

    public void StartDialogRoulette()
    {
        gameObject.GetComponent<Person>().StartFirstDialogue();
    }
}
