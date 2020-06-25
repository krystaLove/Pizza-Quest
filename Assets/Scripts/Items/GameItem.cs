using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public enum ItemType {
        None,
        Rock,
        Coin,
        Tomato,
        CasinoCoin,
        Beer
    }

    public ItemType itemType;
}
