using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSlot : InteractableObject
{
    private bool _played = false;
    public override Reaction GetNextReaction()
    {
        if (_played)
        {
            return progress[1];
        }
        
        if (GameManager.Instance.chosenItem == GameItem.ItemType.CasinoCoin)
        {
            _played = true;
            return progress[2];
        }
        
        return progress[0];
    }
}
