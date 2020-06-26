using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFridge : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;

        if (unlockedProgress.isTalkedWithHomeless && currentProgress + 1< progress.Count)
        {
            currentProgress++;
            return progress[currentProgress];
        }
        else
        {
            return progress[0];
        }
    }
}
