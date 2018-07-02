using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour {

    public GameObject InventoryUI;       


	void Start () {
	}

	void Update () {
	}

    public void InventoryButtonClick()//인벤토리 버튼을 눌렀을때
    {
        if(InventoryUI.activeSelf)          
        {
            InventoryUI.SetActive(false);
        } 
        else                              
        {
            InventoryUI.SetActive(true);
        }
    }
}
