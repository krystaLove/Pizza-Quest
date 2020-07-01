using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableArrow : InteractableObject
{
    public Sprite arrowLeft;
    public override Reaction GetNextReaction()
    {
        if(!UnlockInteractableProgress.Instance.isArrowTriggered)
            return progress[0];
        return ReactionAssets.Instance.notMatchItemReaction;
    }

    public void SetArrowToLeft()
    {
        UnlockInteractableProgress.Instance.isArrowTriggered = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = arrowLeft;
    }
    
}
