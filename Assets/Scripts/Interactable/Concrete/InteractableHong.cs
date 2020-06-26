using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHong : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;
        
        if (!unlockedProgress.isRockThrown)
        {
            if (GameManager.Instance.chosenItem == GameItem.ItemType.Rock)
            {
                unlockedProgress.isRockThrown = true;
                return progress[1];
            }

            return progress[0];
        }

        return progress[2];
    }
}
