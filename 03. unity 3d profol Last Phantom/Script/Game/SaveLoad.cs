
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveLoad : MonoBehaviour
{

    [SerializeField] private ItemInformation itemInformation;

    public void PlayerSaveData(CharactorStatistics playerStatistics)
    {
        var b = new BinaryFormatter();
        var m = new MemoryStream();
        b.Serialize(m, playerStatistics);
        PlayerPrefs.SetString("PlayerSatat", Convert.ToBase64String(m.GetBuffer()));
    }

    public CharactorStatistics PlayerLoadData()
    {
        CharactorStatistics loadData;
        var data = PlayerPrefs.GetString("PlayerSatat");
        if (!string.IsNullOrEmpty(data))
        {
            var b = new BinaryFormatter();
            var m = new MemoryStream(Convert.FromBase64String(data));
            loadData = (CharactorStatistics)b.Deserialize(m);
            return loadData;
        }
        else
        {
            return null;
        }
    }

    public void Save(Item[] ChractorInventory, int playerGold)
    {
        ItemSaveData[] itemSaveData = new ItemSaveData[ChractorInventory.Length];

        for (int i = 0; i < itemSaveData.Length; i++)
        {
            itemSaveData[i].ItemNum = ChractorInventory[i].ItemNum;
            itemSaveData[i].ItemName = ChractorInventory[i].ItemName;
            itemSaveData[i].ItemPrice = ChractorInventory[i].ItemPrice;
            itemSaveData[i].ItemImage = (int)ChractorInventory[i].ItemNum - 1;
            itemSaveData[i].ItemInvenNum = ChractorInventory[i].ItemInvenNum;
            itemSaveData[i].ItemCount = ChractorInventory[i].ItemCount;
            itemSaveData[i].ItemInformation = ChractorInventory[i].ItemInformation;
            itemSaveData[i].soloItem = ChractorInventory[i].oneItem;
        }

        for (int i = 0; i < itemSaveData.Length; i++)
        {
            var b = new BinaryFormatter();
            var m = new MemoryStream();
            b.Serialize(m, itemSaveData[i]);
            PlayerPrefs.SetString("Setting" + i.ToString(), Convert.ToBase64String(m.GetBuffer()));
        }

        PlayerPrefs.SetInt("PlayerGold", playerGold);
    }

    public Item[] Load(Item[] ChractorInventory)
    {
        ItemSaveData[] itemLoadData = new ItemSaveData[ChractorInventory.Length];

        for (int i = 0; i < itemLoadData.Length; i++)
        {
            var data = PlayerPrefs.GetString("Setting" + i.ToString());
            if (!string.IsNullOrEmpty(data))
            {
                var b = new BinaryFormatter();
                var m = new MemoryStream(Convert.FromBase64String(data));
                itemLoadData[i] = (ItemSaveData)b.Deserialize(m);
            }
        }

        for (int i = 0; i < itemLoadData.Length; i++)
        {
            if (itemInformation)
            {
                ChractorInventory[i].ItemNum = itemLoadData[i].ItemNum;
                ChractorInventory[i].ItemName = itemLoadData[i].ItemName;
                ChractorInventory[i].ItemPrice = itemLoadData[i].ItemPrice;
                ChractorInventory[i].ItemImage = itemInformation.SearchImage((int)itemLoadData[i].ItemNum - 1);
                ChractorInventory[i].ItemInvenNum = itemLoadData[i].ItemInvenNum;
                ChractorInventory[i].ItemCount = itemLoadData[i].ItemCount;
                ChractorInventory[i].ItemInformation = itemLoadData[i].ItemInformation;
                ChractorInventory[i].oneItem = itemLoadData[i].soloItem;
            }
        }
        return ChractorInventory;
    }

    public int LoadPlayerGold()
    {
        return PlayerPrefs.GetInt("PlayerGold");
    }

    public void SaveReset()
    {
        //HP
        CharactorStatistics playerStatisticsData = new CharactorStatistics();

        playerStatisticsData.hpMax = 100;
        playerStatisticsData.HP = playerStatisticsData.hpMax;
        playerStatisticsData.Damage = 30;
        playerStatisticsData.Def = 10;

        var b = new BinaryFormatter();
        var m = new MemoryStream();
        b.Serialize(m, playerStatisticsData);
        PlayerPrefs.SetString("PlayerSatat", Convert.ToBase64String(m.GetBuffer()));

        //inventory
        Item[] ChractorInventory = new Item[4];

        for (int i = 0; i < ChractorInventory.Length; i++)
        {
            ChractorInventory[i].ItemNum = itemNumber.Empty;
            ChractorInventory[i].ItemName = null;
            ChractorInventory[i].ItemPrice = 0;
            ChractorInventory[i].ItemImage = null;
            ChractorInventory[i].ItemInvenNum = 0;
            ChractorInventory[i].ItemCount = 0;
            ChractorInventory[i].ItemInformation = null;
            ChractorInventory[i].oneItem = false;
        }

        Save(ChractorInventory, 500);
    }
}
