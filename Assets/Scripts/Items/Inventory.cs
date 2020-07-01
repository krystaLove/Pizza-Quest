using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Inventory
{
   private List<GameItem> _itemList;
   public event EventHandler<GameItem> OnItemAdded;
   public event EventHandler OnItemRemoved;

   public Inventory()
   {
      _itemList = new List<GameItem>();
   }

   public void AddItem(GameItem item)
   {
      _itemList.Add(item);
      OnItemAdded?.Invoke(this, item);

      Debug.Log("Inventory Size:" + _itemList.Count);
   }
   
   public void AddItem(GameItem.ItemType type)
   {
      GameItem item = new GameItem();
      item.itemType = type;
      AddItem(item);
   }

   public void RemoveItem(int index)
   {
      Debug.Log("Remove Item: " + _itemList[index].itemType);
      _itemList.RemoveAt(index);
      OnItemRemoved?.Invoke(this, null);
   }

   public void RemoveItemByType(GameItem.ItemType type)
   {
      for (int i = 0; i < _itemList.Count; i++)
      {
         if (_itemList[i].itemType == type)
         {
            RemoveItem(i);
            break;
         }
      }
   }
   
   public List<GameItem> GetItemList()
   {
      return _itemList;
   }
}

