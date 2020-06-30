using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    [Header("Sprites for Items")]
    [SerializeField] public Sprite rockSprite;
    [SerializeField] public Sprite coinSprite;
    [SerializeField] public Sprite beerSprite;
    [SerializeField] public Sprite stickSprite;
    [SerializeField] public Sprite emptyBottle;
    [SerializeField] public Sprite fullBottle;
    [SerializeField] public Sprite cheeseSprite;
    [SerializeField] public Sprite bunSprite;
    [SerializeField] public Sprite flourSprite;
    [SerializeField] public Sprite pieSprite;
    [SerializeField] public Sprite gasSprite;
    [SerializeField] public Sprite pizzaSprite;
    [SerializeField] public Sprite salamiSprite;
    

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetSpriteByItemType(GameItem.ItemType type)
    {
        switch (type)
        {
            case GameItem.ItemType.Rock: return rockSprite;
            case GameItem.ItemType.Coin: return coinSprite;
            case GameItem.ItemType.Beer: return beerSprite;
            case GameItem.ItemType.Stick: return stickSprite;
            case GameItem.ItemType.EmptyBottle: return emptyBottle;
            case GameItem.ItemType.FullBottle: return fullBottle;
            case GameItem.ItemType.Cheese: return cheeseSprite;
            case GameItem.ItemType.Bun: return bunSprite;
            case GameItem.ItemType.Gas: return gasSprite;
            case GameItem.ItemType.Pie: return pieSprite;
            case GameItem.ItemType.Pizza: return pizzaSprite;
            case GameItem.ItemType.Flour: return flourSprite;
            case GameItem.ItemType.Salami: return salamiSprite;
            default: return rockSprite;
        }
    }
}
