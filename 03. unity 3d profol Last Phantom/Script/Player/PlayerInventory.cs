using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public struct ItemSaveData
{
    public int ItemPrice;
    public int ItemImage;
    public int ItemCount;
    public int ItemInvenNum;
    public bool soloItem;
    public string ItemName;
    public string ItemInformation;
    public itemNumber ItemNum;
};

public class PlayerInventory : MonoBehaviour {
    
    [SerializeField] private float playerGold;
    [SerializeField] private Text goldText;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private InventoryManager inventoryManager;

    private Item emptyItem;
    public Item[] chractorInventory = new Item[4];

    void Start()
    {
        SetGold(0);
        Load();
    }

    public void AddInventory(Item addItem)
    {
        if (!addItem.oneItem)
        {
            bool sameItem = false;
            for (int i = 0; i < chractorInventory.Length; i++)
            {
                if (chractorInventory[i].ItemNum == addItem.ItemNum)
                {
                    if (playerGold >= addItem.ItemPrice)
                    {
                        playerGold -= addItem.ItemPrice;
                        sameItem = true;
                        chractorInventory[i].ItemCount++;
                        break;
                    }
                }
            }
            if (!sameItem)
            {
                for (int i = 0; i < chractorInventory.Length; i++)
                {
                    if (chractorInventory[i].ItemInvenNum == 0)
                    {
                        if (playerGold >= addItem.ItemPrice)
                        {
                            playerGold -= addItem.ItemPrice;
                            chractorInventory[i] = addItem;
                            chractorInventory[i].ItemCount++;
                            chractorInventory[i].ItemInvenNum = i + 1;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            bool sameItem=false;

            for (int i = 0; i < chractorInventory.Length; i++)
            {
                if (chractorInventory[i].ItemNum == addItem.ItemNum)
                {
                    sameItem = true;
                    break;
                }
            }

            if(!sameItem)
            {
                for (int i = 0; i < chractorInventory.Length; i++)
                {
                    if (chractorInventory[i].ItemInvenNum == 0)
                    {
                        if (playerGold >= addItem.ItemPrice)
                        {
                            playerGold -= addItem.ItemPrice;
                            chractorInventory[i] = addItem;
                            chractorInventory[i].ItemCount++;
                            chractorInventory[i].ItemInvenNum = i + 1;
                            break;
                        }
                    }
                }
            }
        }
        goldText.text = "Gold:" + playerGold.ToString("N0");
        inventoryManager.SetImage(chractorInventory);
    }

    public void SwapInventory(int invenNum1,int invenNum2)
    {
        Item dummyItemInfo = chractorInventory[invenNum1 - 1];
        chractorInventory[invenNum1 - 1] = chractorInventory[invenNum2 - 1];
        chractorInventory[invenNum2 - 1] = dummyItemInfo;
        inventoryManager.SetImage(chractorInventory);
    }

    public void SellPlayerItem(Item sell_Item,bool sell)
    {
        if (sell_Item.oneItem)
        {
            for (int i = 0; i < chractorInventory.Length; i++)
            {
                if(chractorInventory[i].ItemNum == sell_Item.ItemNum)
                {
                    if(sell)playerGold += (sell_Item.ItemPrice * 0.5f);
                    chractorInventory[i] = emptyItem;
                }
            }
        }
        else
        {
            for (int i = 0; i < chractorInventory.Length; i++)
            {
                if (chractorInventory[i].ItemNum == sell_Item.ItemNum)
                {
                    if(chractorInventory[i].ItemCount>1)
                    {
                        if (sell) playerGold += (sell_Item.ItemPrice * 0.5f);
                        chractorInventory[i].ItemCount--;
                    }
                    else
                    {
                        if (sell) playerGold += (sell_Item.ItemPrice * 0.5f);
                        chractorInventory[i] = emptyItem;
                    }
                }
            }
        }
        goldText.text = "Gold:" + playerGold.ToString("N0");
        inventoryManager.SetImage(chractorInventory);
    }

    public void Save()
    {
        saveLoad.Save(chractorInventory,(int)playerGold);
    }

    public void Load()
    {
        chractorInventory = saveLoad.Load(chractorInventory);
        playerGold = saveLoad.LoadPlayerGold();

        goldText.text = "Gold:" + playerGold.ToString("N0");
        if (inventoryManager)inventoryManager.SetImage(chractorInventory);
    }

    public void SaveReset()
    {
        saveLoad.SaveReset();
    }

    public void SetGold(int addGold)
    {
        playerGold += addGold;
        goldText.text = "Gold:" + playerGold.ToString("N0");
    }
}
