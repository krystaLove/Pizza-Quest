using UnityEngine;

public class GameItem : MonoBehaviour
{
    public enum ItemType {
        None,
        Rock,
        Coin,
        Tomato,
        CasinoCoin,
        Beer,
        Stick,
        EmptyBottle,
        FullBottle
    }

    public ItemType itemType;
}
