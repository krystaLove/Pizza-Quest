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
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(gameManager.ChangeLevel(thisLevel, nextLevel, nextSpawn, nextBackGround));
    }
}
