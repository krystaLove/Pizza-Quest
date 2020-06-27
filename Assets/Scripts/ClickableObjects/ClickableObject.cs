using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClickableObject : MonoBehaviour
{
    public Transform positionToStep;

    public virtual void Action()
    {
        GameManager.Instance.ResetSelectedItem(false);
    }
}
