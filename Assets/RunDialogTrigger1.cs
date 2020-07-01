using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDialogTrigger1 : MonoBehaviour
{
   public GameObject nextDialogTrigger;
   public GameObject billy;
   public void UnlockNextDialogRunTrigger()
   {
      billy.SetActive(false);
      nextDialogTrigger.SetActive(true);
   }
}
