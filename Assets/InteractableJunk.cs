using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJunk : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        if (!UnlockInteractableProgress.Instance.isStickGot && UnlockInteractableProgress.Instance.isTalkedWithKolhoznik)
        {
            return progress[1];
        }

        return progress[0];
    }
}
