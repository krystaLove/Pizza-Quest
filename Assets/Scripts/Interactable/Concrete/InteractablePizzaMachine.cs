using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePizzaMachine : InteractableObject
{
    private bool _isPizzaPrepared = false;

    public Sprite machineDefault;
    public Sprite pizzaPrepared;
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;
        if (unlockedProgress.CheckPizzaIngredients() && !unlockedProgress.isPizzaGot)
        {
            if (!_isPizzaPrepared)
            {
                _isPizzaPrepared = true;
                return progress[1];
            }

            return progress[2];

        }

        return progress[0];
    }

    public void RemovePizzaIngredients()
    {
        GameManager.Instance.inventory.RemoveItemByType(GameItem.ItemType.Salami);
        GameManager.Instance.inventory.RemoveItemByType(GameItem.ItemType.Cheese);
        GameManager.Instance.inventory.RemoveItemByType(GameItem.ItemType.FullBottle);
        GameManager.Instance.inventory.RemoveItemByType(GameItem.ItemType.Tomato);
        GameManager.Instance.inventory.RemoveItemByType(GameItem.ItemType.Flour);
    }

    public void ChangeSpriteToPizza()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = pizzaPrepared;
    }
    public void ChangeSpriteToDefault()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = machineDefault;
    }
    
}
