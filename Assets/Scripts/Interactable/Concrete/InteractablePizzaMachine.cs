using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePizzaMachine : InteractableObject
{
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;
        if (unlockedProgress.CheckPizzaIngredients())
        {
            return progress[1];
        }

        return progress[0];
    }
}
