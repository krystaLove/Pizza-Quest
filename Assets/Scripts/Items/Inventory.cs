using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Inventory
{
   private List<GameItem> _itemList;
   public event EventHandler<GameItem> OnItemAdded;

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
   
   public List<GameItem> GetItemList()
   {
      return _itemList;
   }
}

