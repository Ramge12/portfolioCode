using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour {

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject inventoryUI;

    public void OpenShopUI()
    {
        inventoryUI.SetActive(true);
        shopUI.SetActive(true);
    }

    public void CloseShopUI()
    {
        shopUI.SetActive(false);
    }
}
