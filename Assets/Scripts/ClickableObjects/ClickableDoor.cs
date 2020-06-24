using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableDoor : ClickableObject
{
    public GameObject thisLevel;
    public GameObject nextLevel;
    public GameObject nextSpawn;
    public SpriteRenderer nextBackGround;

    public override void Action()
    {
        StartCoroutine(GameManager.Instance.ChangeLevel(thisLevel, nextLevel, nextSpawn, nextBackGround));
    }
}

