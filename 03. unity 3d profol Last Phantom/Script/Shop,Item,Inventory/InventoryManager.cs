using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField] private Text sellButtonText;
    [SerializeField] private Text[] countText = new Text[4];
    [SerializeField] private ShopManager shopManager;

    [SerializeField] private Transform sellButton;
    [SerializeField] private Transform[] inventorySlot = new Transform[4];

    public void SetImage(Item[] playerInventory)
    {
        for (int i=0; i< inventorySlot.Length; i++)
        {
            int inventoryNum = playerInventory[i].ItemInvenNum - 1;
            if (inventoryNum != -1)
            {
                inventorySlot[inventoryNum].GetChild(0).transform.GetComponent<Image>().sprite = playerInventory[inventoryNum].ItemImage;
            }
        }

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if (playerInventory[i].ItemInvenNum == 0)
            {
                inventorySlot[i].GetChild(0).transform.GetComponent<Image>().sprite = null;
            }
        }

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            countText[i].text = playerInventory[i].ItemCount.ToString();
        }
    }

    public void SellButtonPosition()
    {
        sellButton.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        sellButton.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        sellButtonText.text = "판매가: " + (shopManager.curItem.ItemPrice * 0.5).ToString("N0");
        yield return new WaitForSeconds(2f);
        sellButton.gameObject.SetActive(false);
        yield return null;
    }
}
