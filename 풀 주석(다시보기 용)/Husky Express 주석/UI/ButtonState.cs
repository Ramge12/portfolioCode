using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour {

    public Text m_sellText;                     //현재 가게의 가격을 표시할 text UI
    public Text m_sellText_pre;                 //자신이 이전에 구매한 가격을 표시할 text UI
    public Image m_Sell_button_Image;           //버튼의 이미지
    public SellButton m_sellButton;             //판매 버튼 클래스
    public ShopManager m_shopManager;           //상점 매니저 클래스
    public PlayerInventory m_palyerInven;       //플레이어 인벤토리 클래스
    public int m_button_Num;                    //고른 아이템의 아이템번호
    public int m_button_inventory_num = 0;      //고른 아이템의 인벤토리상에서의 인덱스
    int m_pirce;                                //상점에서 표시할 가격

    // Use this for initialization
    void Start () {
    }
	// Update is called once per frame
	void Update () {
    }
    public void button_select()//인벤토리에서 버튼을 눌럿을때 사용할 함수
    {
        switch (m_button_Num)   //아이템 번호를 이용하여 가격 , 이미지를 바꿉니다
        {
            case 0:
                m_Sell_button_Image.sprite = m_shopManager.mot;         //이미지를 바꿉니다
                m_pirce = m_shopManager.m_shop.m_mot_Purchase_price;    //가격을 현재 상점의 못의 가격으로 바꿉니다
                break;
            case 1:
                m_Sell_button_Image.sprite = m_shopManager.wood;        //이미지를 바꿉니다
                m_pirce = m_shopManager.m_shop.m_wood_Purchase_price;   //가격을 현재 상점의 나무의 가격으로 바꿉니다
                break;
            case 2:
                m_Sell_button_Image.sprite = m_shopManager.meat;        //이미지를 바꿉니다
                m_pirce = m_shopManager.m_shop.m_meat_Purchase_price;   //가격을 현재 상점의 고기의 가격으로 바꿉니다
                break;
        }
        m_sellButton.button_num = m_button_inventory_num;               //버튼 인덱스를SellButton에 넘겨줍니다
        m_sellText.text = "현재 매입가:"+ m_pirce.ToString();           //매입가 text를 수정합니다
        m_sellText_pre.text = "구매했던 가격:"+m_palyerInven.ItemS[m_button_inventory_num-1].Item_Price.ToString();   //인덱스를 통해 인벤토리에서 아이템의 정보를 읽어 이전에 샀던 가격을 불러옵니다
    }
    public void Sell_Item()//아이템을 파는 함수
    {
        if (m_button_inventory_num != 0)//아이템이 없을 경우에만 inventor_num이 0이기 떄문에 0이 아닐떄만 실행된다
        {
            if (m_palyerInven.ItemS[m_button_inventory_num - 1].Item_enable)//인벤토리 그 인덱스에 아이템이 존재할경우
            {
                m_palyerInven.Gold += m_pirce;                      //가격만큼 인벤토리에 더합니다
                m_palyerInven.Sell_item(m_button_inventory_num - 1);//인벤토리에 아이템 인덱스를 sell_item에 넘긴다
            }
        }
    }
}
