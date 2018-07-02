using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour {

    public GameObject shopUI;           
    public GameObject IdleUI;           
    public ShopManager m_shopManager;   
    public ShopNPC shop;                            


	void Start () {
	}

	void Update () {
	}

    public void UIclick()   //NPC를 클릭하여 shop UI를 출력하는 함수
    {
        m_shopManager.m_shop = shop;   
        shop.Change_price=(false);     
        shopUI.SetActive(true);        
        IdleUI.SetActive(false);       
    }                                                                                                                                                  

    public void Idleclick() //shop UI를 끄고 원래대로 돌아올때 사용하는 함수
    {
        m_shopManager.m_shop = null;    
        shop.Change_price = (true);     
        shopUI.SetActive(false);        
        IdleUI.SetActive(true);                            
    }
}
