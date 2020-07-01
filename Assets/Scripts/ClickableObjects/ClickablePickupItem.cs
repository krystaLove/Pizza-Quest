using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePickupItem : ClickableObject
{

    public Reaction react;
    public override void Action()
    {
        if(react != null)
            react.Do();
        gameObject.SetActive(false);
        GameManager.Instance.inventory.AddItem(gameObject.GetComponent<GameItem>());
    }
}
