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
            default: return rockSprite;
        }
    }
}
