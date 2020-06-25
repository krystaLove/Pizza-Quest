using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePickupItem : ClickableObject
{
    public override void Action()
    {
        gameObject.SetActive(false);
        GameManager.Instance.inventory.AddItem(gameObject.GetComponent<GameItem>());
    }
}
