﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public PlayerController playerController;
    public GameObject mainCamera;

    public Animator levelFadeAnimator;

    public Inventory inventory;
    public UI_Inventory UiInventory;

    public GameItem.ItemType chosenItem = GameItem.ItemType.None;
    private void Awake()
    {
        Instance = this;
        inventory = new Inventory();
        UiInventory.SetInventory(inventory);
    }

    public void BlockMove()
    {
        ClickManager.Instance.canMove = false;
    }

    public void AllowMove()
    {
        ClickManager.Instance.canMove = true;
    }

    public IEnumerator ChangeLevel(GameObject from, GameObject to, GameObject spawnPoint, SpriteRenderer nextBg)
    {
        if (levelFadeAnimator.enabled)
        {
            levelFadeAnimator.SetBool("Start", true);
            yield return new WaitForSeconds(levelFadeAnimator.GetCurrentAnimatorStateInfo(0).length);
            levelFadeAnimator.SetBool("Start", false);
        }

        from.SetActive(false);
        to.SetActive(true);
        
        var position = spawnPoint.transform.position;
        playerController.SetPosition(position);
        mainCamera.GetComponent<CameraFollow>().SetSpriteRenderer(nextBg);
        mainCamera.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, mainCamera.transform.position.z);
    }

    public void SetSelectedItem(int index)
    {
        chosenItem = inventory.GetItemList()[index].itemType;
        Debug.Log("New selected Item: " + chosenItem);
    }
    public void SetSelectedItem(GameItem.ItemType type)
    {
        if (type == GameItem.ItemType.None)
        {
            UiInventory.CloseInventory();
        }
        
        chosenItem = type;
        Debug.Log("New selected Item: " + chosenItem);
    }

}
