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
        FullBottle,
        Flour,
        Cheese,
        Bun,
        Magnet,
        Pie,
        Gas,
        Salami,
        Pizza,
        MagnetAndCoin
    }

    public ItemType itemType;
}
