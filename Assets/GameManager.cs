
using System;
using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    public PlayerController playerController;
    public GameObject mainCamera;

    [Header("Level")]
    public LevelSettings startLevel;
    
    [Header("Level Change")]
    public Animator levelFadeAnimator;

    [Header("Cursor Change")]
    public GameObject followCursorItem;

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

    public void Start()
    {
        DialogueManager.Instance.OnDialogStart += (sender, args) => followCursorItem.SetActive(false);
    }

    public void StartGame()
    {
        if (startLevel.musicTheme != null)
        {
            Music.Instance.Stop();
            Music.Instance.SetAudioClip(startLevel.musicTheme);
        }
    }

    public IEnumerator ChangeLevel(GameObject from, LevelSettings nextLevel)
    {
        
        //Music.Instance.Stop();
       // DialogueVoiceOver.Instance.Stop();
        
        AudioManager.StopMusic();
        AudioManager.instance.InstanceStopDialogueVoiceover();
        BlockMove();
        
        if (levelFadeAnimator.enabled)
        {
            levelFadeAnimator.SetBool("Start", true);
            yield return new WaitForSeconds(levelFadeAnimator.GetCurrentAnimatorStateInfo(0).length);
            levelFadeAnimator.SetBool("Start", false);
            playerController.gameObject.transform.localScale = new Vector3(nextLevel.characterSize, nextLevel.characterSize);
            mainCamera.GetComponent<Camera>().orthographicSize = nextLevel.cameraSize;
            AllowMove();
        }

        if (nextLevel.musicTheme != null)
        {
           /* Music.Instance.Stop();
            Music.Instance.SetAudioClip(nextLevel.musicTheme);
            Music.Instance.Play();*/
           AudioManager.PlayMusic(nextLevel.musicTheme);
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
        followCursorItem.SetActive(true);
        followCursorItem.GetComponent<SpriteRenderer>().sprite =
            ItemAssets.Instance.GetSpriteByItemType(inventory.GetItemList()[index].itemType);
        
        var lastItem = chosenItem;
        chosenItem = inventory.GetItemList()[index].itemType;
        if (lastItem == GameItem.ItemType.Coin && chosenItem == GameItem.ItemType.Magnet ||
            chosenItem == GameItem.ItemType.Coin && lastItem == GameItem.ItemType.Magnet)
        {
            inventory.RemoveItemByType(GameItem.ItemType.Magnet);
            inventory.RemoveItemByType(GameItem.ItemType.Coin);
            inventory.AddItem(GameItem.ItemType.MagnetAndCoin);
            chosenItem = GameItem.ItemType.None;
            chosenItemIndex = -1;
            DisableSelectedItemFollowingCursor();
            return;
        }
        chosenItemIndex = index;
        
        Debug.Log("New selected Item: " + chosenItem);
    }
    public void SetSelectedItem(GameItem.ItemType type)
    {
        chosenItem = type;
        Debug.Log("New selected Item: " + chosenItem);
    }

    public void ResetSelectedItem(bool withUi = true)
    {
        followCursorItem.SetActive(false);
        Debug.Log("Reset Selected Item");
        chosenItemIndex = -1;
        chosenItem = GameItem.ItemType.None;
        
        if (withUi)
        {
            UiInventory.CloseInventory();
        }
    }

    public void DisableSelectedItemFollowingCursor()
    {
        followCursorItem.SetActive(false);
    }
    public void HideScreen()
    {
        levelFadeAnimator.SetBool("Start", true);
    }

    public void ShowScreen()
    {
        levelFadeAnimator.SetBool("Start", false);
    }

    public void GetItem(GameItem.ItemType type)
    {
        inventory.AddItem(type);
    }
}
