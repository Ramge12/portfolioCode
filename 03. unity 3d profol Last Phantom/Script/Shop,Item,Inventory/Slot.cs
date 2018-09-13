using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

    [SerializeField] private ShopManager shopManager;
    [SerializeField] private PlayerInventory playerInventory;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject parentObject;
        parentObject = DragAndDrop.itemBeingDragged.transform.parent.gameObject;

        int inventoryNum1 = parentObject.transform.GetComponent<ItemInformation>().itemInventoryNum;
        int inventoryNum2 = transform.GetComponent<ItemInformation>().itemInventoryNum;

        DragAndDrop thisSlot;
        thisSlot = transform.GetComponentInChildren<DragAndDrop>();

        if (transform.CompareTag(parentObject.tag) && transform.CompareTag("Inventory"))
        {
            thisSlot.transform.SetParent(parentObject.transform);
            DragAndDrop.itemBeingDragged.transform.SetParent(transform);
            playerInventory.SwapInventory(inventoryNum1, inventoryNum2);
        }
        else if(transform.CompareTag("Shop"))
        {
            int iteminft = DragAndDrop.itemBeingDragged.transform.parent.GetComponent<ItemInformation>().itemInventoryNum;
            shopManager.curItem = playerInventory.chractorInventory[iteminft-1];
            shopManager.BuytoShop();
        }
        else if(transform.CompareTag("Inventory"))
        {
            ItemInformation iteminft = DragAndDrop.itemBeingDragged.transform.parent.GetComponent<ItemInformation>();
            shopManager.curItem = DragAndDrop.itemBeingDragged.transform.parent.GetComponent<ItemInformation>().itemValue;
            shopManager.SelltoPlayer();
        }
    }

    public void Sell_Item()
    {
        int curItemInvenNum = transform.GetComponent<ItemInformation>().itemInventoryNum;
        shopManager.curItem = playerInventory.chractorInventory[curItemInvenNum - 1];
    }
}
