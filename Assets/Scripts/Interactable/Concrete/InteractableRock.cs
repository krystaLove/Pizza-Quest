using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRock : InteractableObject
{
    public void DisableRock()
    {
        gameObject.SetActive(false);
    }
}