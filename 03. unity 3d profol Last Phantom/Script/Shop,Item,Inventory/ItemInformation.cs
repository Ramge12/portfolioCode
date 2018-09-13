using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Item
{
    public int ItemPrice;
    public int ItemCount;
    public int ItemInvenNum;
    public bool oneItem;
    public string ItemName;
    public string ItemInformation;
    public Sprite ItemImage;
    public itemNumber ItemNum;
};

public enum itemNumber
{
    Empty,
    Potion,
    CarSkill,
    BikeSpawn
}

public class ItemInformation : MonoBehaviour {

    [Header("Inventory Object")]
    [SerializeField] private GameObject[] itemImages = new GameObject[3];

    [Header("Inventory Scripts")]
    [SerializeField] private ShopManager shopManager;

    [Header("Inventory UI")]
    [SerializeField] private Text showItemInfo;
    [SerializeField] private Text showItemName;
    [SerializeField] private Image showItemImage;

    [Header("Inventory Number")]
    public int itemInventoryNum;

    [Header("Inventory Value")]
    public Item itemValue;

    [Header("Inventory Information")]
    [SerializeField] private int itemPrice;
    [SerializeField] private int itemCount;
    [SerializeField] private int itemInvenNum;
    [SerializeField] private bool itemSoloItem;
    [SerializeField] private string itemName;
    [SerializeField] private string itemInformation;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private itemNumber itemNum;

    private void Awake()
    {
        itemValue.ItemPrice         = itemPrice;
        itemValue.ItemCount         = itemCount;
        itemValue.ItemInvenNum      = itemInvenNum;
        itemValue.oneItem           = itemSoloItem;
        itemValue.ItemName          = itemName;
        itemValue.ItemInformation   = itemInformation;
        itemValue.ItemImage         = itemImage;
        itemValue.ItemNum           = itemNum;
    }

    public void ShowItemInformation()
    {
        showItemName.text = itemName;
        shopManager.curItem = itemValue;
        showItemImage.sprite = itemImage;
        showItemInfo.text = "가격:" + itemPrice.ToString() + "\n" + itemInformation;
    }

    public Sprite SearchImage(int number)
    {
        if (number != -1)
        {
            return itemImages[number].GetComponent<Image>().sprite;
        }
        else
        {
            return null;
        }
    }
}
