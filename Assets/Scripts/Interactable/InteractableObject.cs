using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : ClickableObject
{
    public Reaction commonReaction;
    [SerializeField] public List<Reaction> progress;
    public int currentProgress;
    public override void Action()
    {
        GetNextReaction().Do();
    }

    public virtual Reaction GetNextReaction()
    {
        Reaction toReact;
        if (currentProgress < progress.Count)
        {
            toReact = progress[currentProgress];
            currentProgress++;
        }
        else
        {
            toReact = commonReaction;
        }

        return toReact;
    }
}
