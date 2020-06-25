using System;
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

   public void RemoveItem(int index)
   {
      _itemList.RemoveAt(index);
      OnItemRemoved?.Invoke(this, null);
   }
   
   public List<GameItem> GetItemList()
   {
      return _itemList;
   }
}

