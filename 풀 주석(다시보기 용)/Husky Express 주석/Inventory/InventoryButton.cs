using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour {

    public GameObject InventoryUI;          //인벤토리 UI

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void InventoryButtonClick()      //인벤토리 버튼을 눌렀을때
    {
        if(InventoryUI.activeSelf)          
        {
            InventoryUI.SetActive(false);
        } 
        else                                //비활성화가 되어있다면 활성화시킨다
        {
            InventoryUI.SetActive(true);
        }
    }
}
