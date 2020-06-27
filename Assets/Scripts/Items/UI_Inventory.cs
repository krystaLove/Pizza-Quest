using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public List<GameObject> UiItemSlots;
    private Inventory _inventory;
    public Animator uiInventoryAnimator;

    public bool isInventoryOpen = false;

    public GameObject inventoryButton;

    private void Start()
    {
        _inventory.OnItemAdded += Inventory_ItemAdded;
        _inventory.OnItemRemoved += _refreshInventoryItems;

        DialogueManager.Instance.OnDialogStart += (e, o) => _triggerInventoryButton();
        DialogueManager.Instance.OnDialogFinish += (e, o) => _triggerInventoryButton();
    }

    private void _refreshInventoryItems(object sender, EventArgs e)
    {
        _refreshInventoryItems();
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        _refreshInventoryItems();
    }

    public void TriggerInventoryButton()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            ShowInventory();
        }
    }

    public void _triggerInventoryButton()
    {
        inventoryButton.SetActive(!inventoryButton.activeSelf);
    }

    public void ShowInventory()
    {
        uiInventoryAnimator.SetBool("Start", true);
        isInventoryOpen = true;
    }

    public void CloseInventory()
    {
        uiInventoryAnimator.SetBool("Start", false);
        isInventoryOpen = false;
    }

    private void Inventory_ItemAdded(object sender, GameItem item)
    {
        _refreshInventoryItems();
        
        StopAllCoroutines();
        StartCoroutine(_popUpandDownInventory());
    }

    void _refreshInventoryItems()
    {
        int index = 0;
        foreach (GameItem item in _inventory.GetItemList())
        {
            UiItemSlots[index].SetActive(true);
            UiItemSlots[index].GetComponent<Image>().sprite = ItemAssets.Instance.GetSpriteByItemType(item.itemType);
            ++index;
        }
    }

    private IEnumerator _popUpandDownInventory()
    {
        uiInventoryAnimator.SetBool("Start", true);
        yield return new WaitForSeconds(uiInventoryAnimator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        uiInventoryAnimator.SetBool("Start", false);
    }


}
