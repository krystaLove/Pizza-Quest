using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockInteractableProgress : MonoBehaviour
{
   public static UnlockInteractableProgress Instance { get; private set; }

   private void Awake()
   {
      Instance = this;
   }

   [Header("Whole Game")] public bool isPizzaGot = false;
   public bool isShipLoaded = false;

   [Header("00_plantation")] 
   public bool isStickGot = false;

   [Header("02_outership")] 
   public bool isVisitedByHomeless = false;
   public GameObject solaraDialogTrigger;
   public GameObject shipAfterVisiting;

   [Header("03_innership")] 
   public bool isTalkedWithHomeless = false;
   public bool isLeverTriggered = false;

   [Header("05_square")] 
   public bool isRockThrown;

   [Header("Ingredients")] 
   public bool cheese;
   public bool tomato;
   public bool flour;

   public void checkPizzaIngredients()
   {
      isPizzaGot = cheese && tomato && flour;
   }

   public void SetTlkedWithHomelessTrigger()
   {
      isTalkedWithHomeless = true;
   }

   public void SetSlivSolari()
   {
      isVisitedByHomeless = true;
      solaraDialogTrigger.SetActive(true);
      shipAfterVisiting.SetActive(true);
   }

}
