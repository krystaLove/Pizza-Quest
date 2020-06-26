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
            default: return rockSprite;
        }
    }
}
