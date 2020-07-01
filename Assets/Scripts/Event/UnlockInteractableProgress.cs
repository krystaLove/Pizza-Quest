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
   public bool isTalkedWithKolhoznik = false;

   [Header("02_outership")] 
   public bool isVisitedByHomeless = false;
   public GameObject solaraDialogTrigger;
   public List<GameObject> toActivateSolaraTrigger;
   public List<GameObject> toDisableSolaraTrigger;
   public GameObject shipAfterVisiting;

   [Header("03_innership")] 
   public bool isTalkedWithHomeless = false;
   public bool isArrowTriggered = false;

   [Header("05_square")] 
   public bool isRockThrown;
   
   [Header("07_church")] 
   public bool isTalkedWithPastor;

   [Header("Ingredients")] 
   public bool cheese;
   public bool tomato;
   public bool flour;
   public bool salami;
   public bool water;

   public bool CheckPizzaIngredients()
   {
      return cheese && tomato && flour && salami && water;
   }

   public void SetTlkedWithHomelessTrigger()
   {
      isTalkedWithHomeless = true;
   }

   public void SetTalkedWithKolhoznik()
   {
      isTalkedWithKolhoznik = true;
   }

   public void SetIsTalkedWithPastor()
   {
      isTalkedWithPastor = true;
   }

   public void SetSlivSolari()
   {
      isVisitedByHomeless = true;
      foreach (var item in toActivateSolaraTrigger)
      {
         item.SetActive(true);
      }
      foreach (var item in toDisableSolaraTrigger)
      {
         item.SetActive(false);
      }
      //solaraDialogTrigger.SetActive(true);
      //shipAfterVisiting.SetActive(true);
   }

   public void VozvrashenieSolari()
   {
      shipAfterVisiting.SetActive(false);
   }

   public void GetCasinoCoin()
   {
      GameManager.Instance.inventory.AddItem(GameItem.ItemType.CasinoCoin);
   }
   public void GetCheese()
   {
      cheese = true;
      GameManager.Instance.inventory.AddItem(GameItem.ItemType.Cheese);
   }

   public void GetStick()
   {
      isStickGot = true;
      GameManager.Instance.inventory.AddItem(GameItem.ItemType.Stick);
   }
   public void GetSalami()
   {
      GameManager.Instance.inventory.AddItem(GameItem.ItemType.Salami);
   }
}
