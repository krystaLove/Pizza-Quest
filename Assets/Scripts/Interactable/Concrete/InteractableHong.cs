using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHong : InteractableObject
{

    public Transform positionToRun;
    public LevelSettings levelToRun;
    public GameObject thisLevel;
    public GameObject runDialogTrigger;
    public override Reaction GetNextReaction()
    {
        UnlockInteractableProgress unlockedProgress = UnlockInteractableProgress.Instance;
        
        if (!unlockedProgress.isRockThrown)
        {
            if (GameManager.Instance.chosenItem == GameItem.ItemType.Rock && UnlockInteractableProgress.Instance.isTalkedWithPastor)
            {
                unlockedProgress.isRockThrown = true;
                return progress[1];
            }
            
            if (GameManager.Instance.chosenItem != GameItem.ItemType.None)
            {
                return ReactionAssets.Instance.notMatchItemReaction;
            }

            return progress[0];
        }

        return progress[2];
    }

    public void TriggerDialog()
    {
        gameObject.GetComponent<Person>().StartFirstDialogue();
    }

    public void Run()
    {
        GameManager.Instance.playerController.GoTo(positionToRun.position);
    }

    public void LoadRiver()
    {
        runDialogTrigger.SetActive(true);
        GameManager.Instance.ChangeLevelWithInnerCoroutine(thisLevel, levelToRun);
    }
    
}
