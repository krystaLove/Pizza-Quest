using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAssets : MonoBehaviour
{
    public Reaction notMatchItemReaction;
    public static ReactionAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Reaction GetNotMatchItemReaction()
    {
        return notMatchItemReaction;
    }
}
