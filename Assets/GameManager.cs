using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    public GameObject player;
    public PlayerController playerController;
    public GameObject mainCamera;
    
    [Header("Level Change")]
    public Animator levelFadeAnimator;

    [Header("Inventory")]
    public Inventory inventory;
    public UI_Inventory UiInventory;
    public GameItem.ItemType chosenItem = GameItem.ItemType.None;
    public int chosenItemIndex = -1;
    
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

    public IEnumerator ChangeLevel(GameObject from, LevelSettings nextLevel)
    {
         Music.Instance.Stop();
        
        if (levelFadeAnimator.enabled)
        {
            levelFadeAnimator.SetBool("Start", true);
            yield return new WaitForSeconds(levelFadeAnimator.GetCurrentAnimatorStateInfo(0).length);
            levelFadeAnimator.SetBool("Start", false);
        }

        if (nextLevel.musicTheme != null)
        {
            Music.Instance.Stop();
            Music.Instance.SetAudioClip(nextLevel.musicTheme);
            Music.Instance.Play();
        }

        from.SetActive(false);
        nextLevel.thisLevel.SetActive(true);

        var position = nextLevel.spawnPoint.position;
        playerController.SetPosition(position);
        mainCamera.GetComponent<CameraFollow>().SetSpriteRenderer(nextLevel.spriteBg.GetComponent<SpriteRenderer>());
        mainCamera.transform.position = new Vector3(position.x, position.y, mainCamera.transform.position.z);
    }

    public void SetSelectedItem(int index)
    {
        chosenItem = inventory.GetItemList()[index].itemType;
        chosenItemIndex = index;
        Debug.Log("New selected Item: " + chosenItem);
    }
    public void SetSelectedItem(GameItem.ItemType type)
    {
        if (type == GameItem.ItemType.None)
        {
            chosenItemIndex = -1;
            UiInventory.CloseInventory();
        }
        
        chosenItem = type;
        Debug.Log("New selected Item: " + chosenItem);
    }

}
