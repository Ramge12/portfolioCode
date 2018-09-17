using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour {

    private int carCount = 0;

    [SerializeField] private Transform playerBike;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerInventory playerInventory;

    public void UseItem(int number)
    {
        if (playerInventory.chractorInventory[number].ItemNum == itemNumber.Potion)
        {
            playerTransform.GetComponent<PlayerStats>().AddPlayerHealthPoint(30,1);
            playerInventory.SellPlayerItem(playerInventory.chractorInventory[number], false);
        }
        else if (playerInventory.chractorInventory[number].ItemNum == itemNumber.CarSkill)
        {
            playerInventory.SellPlayerItem(playerInventory.chractorInventory[number], false);
            CarDropSkill();
        }
        else if (playerInventory.chractorInventory[number].ItemNum == itemNumber.BikeSpawn)
        {
            SpawnBike();
            playerInventory.SellPlayerItem(playerInventory.chractorInventory[number], false);
        }
    }

     void CarDropSkill()
     {
         carCount++;
         if (carCount >=3) CarCrushSkill.call().Reload(carCount % 3);
     
         Transform carDrop;
         carDrop = CarCrushSkill.call().GetObject("CarCrush").transform;
         carDrop.gameObject.SetActive(true);
         carDrop.position = playerTransform.position + playerTransform.forward * 3 + playerTransform.up * 5;
     }

     void SpawnBike()
     {
         playerBike.position = playerTransform.position + playerTransform.forward*2;
         playerBike.gameObject.SetActive(true);
     }
}
