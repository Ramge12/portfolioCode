using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public enum shop_state { mot, wood, meat };
    public shop_state m_shop_state;                 //상점의 물건의 상태(현재고른 물건)

    public Text Information_Text_sell;             
    public Image Information_Image;                
    public Sprite mot;                                                 
    public Sprite wood;
    public Sprite meat;
    public ShopNPC m_shop;                         
    public GameObject sell_UI;                     
    public GameObject buy_UI;                      
    public PlayerInventory m_player_Inven;         
    public int price;                                           


    void Start () {
	}

	void Update () {
	}

    public void SellMessage_mot()//상점에서 아이템을 플레이어에게 팔때 이미지,텍스트를 바꾸어주는 함수
    {
        sell_UI.SetActive(true);           
        buy_UI.SetActive(false);           
        Information_Image.sprite = mot;    
        m_shop_state = shop_state.mot;
        price = m_shop.m_mot_Soldier;      
        Information_Text_sell.text = "못을" + m_shop.m_mot_Soldier.ToString() + "에 구매합니다";
    }

    public void SellMessage_wood()//판매UI에 이미지와 텍스트를 교체합니다
    {
        sell_UI.SetActive(true);
        buy_UI.SetActive(false);
        Information_Image.sprite = wood;
        m_shop_state = shop_state.wood;
        price = m_shop.m_wood_Soldier;
        Information_Text_sell.text = "나무를" + m_shop.m_wood_Soldier.ToString() + "에 구매합니다";
    }

    public void SellMessage_meat()//판매UI에 이미지와 텍스트를 교체합니다
    {
        sell_UI.SetActive(true);
        buy_UI.SetActive(false);
        Information_Image.sprite = meat;
        m_shop_state = shop_state.meat;
        price = m_shop.m_meat_Soldier;
        Information_Text_sell.text = "고기를"+ m_shop.m_meat_Soldier.ToString() + "에 구매합니다";
    }

    public void Sell_button()//판매 버튼 함수
    {
        if (price <= m_player_Inven.Gold)
        {
            m_player_Inven.Gold -= price;
            if (m_player_Inven.LegnthCount() < m_player_Inven.ItemS.Length)
                //플레이어 인벤토리칸이 여유가있을떄, 플레이어가 현재 가지고있는 아이템수가 인벤토리 전체칸보다 작을떄
            {
                switch (m_shop_state)   //아이템 종류에 따라 분류합니다
                {
                    case shop_state.mot:
                        m_player_Inven.Add_Item(0, price, mot); //아이템 종류에 따라 인벤토리에 더합니다
                        break;
                    case shop_state.wood:
                        m_player_Inven.Add_Item(1, price, wood);
                        break;
                    case shop_state.meat:
                        m_player_Inven.Add_Item(2, price, meat);
                        break;
                }
            }
            else
            {
                Debug.Log("인벤토리오버");
            }
        }
        else
        {
            Debug.Log("가격오버");
        }
    }

    public void Buy_player_icon(int num)    //플레이어에게서 아이템을 구매할떄 활성화 되는 ui
    {
        sell_UI.SetActive(false);
        buy_UI.SetActive(true);           
    }
}
