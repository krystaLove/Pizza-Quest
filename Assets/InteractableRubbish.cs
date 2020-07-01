using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRubbish : InteractableObject
{
    private bool _usedWithStick = false;
    private bool _gotMagnet = false;

    public Sprite afterStickRubbish;
    public override Reaction GetNextReaction()
    {
        var item = GameManager.Instance.chosenItem;
        if (item == GameItem.ItemType.Stick)
        {
            _usedWithStick = true;
            GameManager.Instance.ResetSelectedItem(false);
            return progress[1];
        }

        if (item != GameItem.ItemType.None)
        {
            return ReactionAssets.Instance.notMatchItemReaction;
        }

        if (_usedWithStick)
        {
            if (!_gotMagnet)
            {
                _gotMagnet = true;
                return progress[2];
            }

            return progress[3];
        }

        return progress[0];
    }

    public void ChangeRubbishSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = afterStickRubbish;
    }
}
