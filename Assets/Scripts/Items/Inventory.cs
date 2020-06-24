using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    public GameObject gameObjShow;
}

[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
}
