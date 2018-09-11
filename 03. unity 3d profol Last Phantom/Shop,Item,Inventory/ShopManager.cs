using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public Item curItem;

    [SerializeField] private PlayerInventory playerInventory;

    public void SelltoPlayer()
    {
        playerInventory.AddInventory(curItem);
    }

    public void BuytoShop()
    {
        playerInventory.SellPlayerItem(curItem,true);
    }
}
