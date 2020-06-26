using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDashboard : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;

        if (unlockedProgress.isPizzaGot && unlockedProgress.isShipLoaded)
        {
            return progress[2];
        }

        if (unlockedProgress.isVisitedByHomeless)
        {
            return progress[1];
        }
        else
        {
            return progress[0];
        }
    }
    

}
