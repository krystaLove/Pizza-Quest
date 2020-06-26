using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableDoor : ClickableObject
{
    public GameObject thisLevel;
    public LevelSettings nextLevelSettings;

    private void Start()
    {
        if (positionToStep == null)
        {
            positionToStep = gameObject.transform;
        }
    }

    public override void Action()
    {
        StartCoroutine(GameManager.Instance.ChangeLevel(thisLevel, nextLevelSettings));
    }
}

