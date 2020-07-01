using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePieMachine : InteractableObject
{
    public GameObject pieMachineCloseLook;
    public GameObject innerShipScene;

    public override Reaction GetNextReaction()
    {
        var item = GameManager.Instance.chosenItem;
        if (item == GameItem.ItemType.Bun)
        {
            return progress[1];
        }

        if (item != GameItem.ItemType.None)
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }

        return progress[0];
    }

    public void GetCloserLook()
    {
        pieMachineCloseLook.SetActive(true);
        innerShipScene.SetActive(false);
    }

    public void GetOutCloserLook()
    {
        innerShipScene.SetActive(true);
        pieMachineCloseLook.SetActive(false);
    }
}
