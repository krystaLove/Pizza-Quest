using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClickablePerson : ClickableObject
{
    public override void Action()
    {
        Person person = gameObject.GetComponent<Person>();
        if (person.itemDialog.neededItem != GameManager.Instance.chosenItem && GameManager.Instance.chosenItem != GameItem.ItemType.None)
        {
            GameManager.Instance.ResetSelectedItem(false);
            ReactionAssets.Instance.GetNotMatchItemReaction().Do();
            return;
        }
        
        if (person.gotItem)
        {
            person.afterGettingItemReaction.Do();
            return;
        }

        DialogueManager.Instance.SetDialogue(person.GetNextDialog());
        StartCoroutine(DialogueManager.Instance.StartDialogue());
    }
}
