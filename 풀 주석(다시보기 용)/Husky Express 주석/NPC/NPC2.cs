using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour {

    public GameObject shopUI;           //상점 UI
    public GameObject IdleUI;           //원래 UI(버튼, 조이스틱)
    public ShopManager m_shopManager;   //상점 매니저 클래스
    public ShopNPC shop;                //상점 클래스

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void UIclick()   //NPC를 클릭하여 shop UI를 출력하는 함수
    {
        m_shopManager.m_shop = shop;    //상점 매니저 클래스에 상점 클래스를 넣습니다
        shop.Change_price=(false);      //상점에서 판매하는 물품은 실시간으로 가격이 변화되기 떄문에 상점이 켜저있는 동안은 변하지않도록 false를줍니다
        shopUI.SetActive(true);         //상점 UI를 활성화시킵니다
        IdleUI.SetActive(false);        //원래 UI를 비활성화시킵니다
    }
    public void Idleclick() //shop UI를 끄고 원래대로 돌아올때 사용하는 함수
    {
        m_shopManager.m_shop = null;    //상점 매니저 클래스에서 상점 부분을 제거합니다
        shop.Change_price = (true);     //상점 가격변동을 할수 있도록 true를 줍니다
        shopUI.SetActive(false);        //상점 UI를 비활성화 시키고
        IdleUI.SetActive(true);         //원래 UI를 활성화 시킵니다
    }
}
