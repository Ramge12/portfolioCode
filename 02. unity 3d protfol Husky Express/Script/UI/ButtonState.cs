using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour {

    public Text m_sellText;                    
    public Text m_sellText_pre;                
    public Image m_Sell_button_Image;          
    public SellButton m_sellButton;            
    public ShopManager m_shopManager;          
    public PlayerInventory m_palyerInven;      
    public int m_button_Num;                   
    public int m_button_inventory_num = 0;     
    int m_pirce;                                                                 


    void Start () {
    }

	void Update () {
    }

    public void button_select()//상점 UI에서 인벤토리 버튼을 눌렀을떄
    {
        switch (m_button_Num)   //아이템 번호를 이용하여 가격 , 이미지를 바꿉니다
        {
            case 0:
                m_Sell_button_Image.sprite = m_shopManager.mot;         
                m_pirce = m_shopManager.m_shop.m_mot_Purchase_price;    
                break;
            case 1:
                m_Sell_button_Image.sprite = m_shopManager.wood;        
                m_pirce = m_shopManager.m_shop.m_wood_Purchase_price;   
                break;
            case 2:
                m_Sell_button_Image.sprite = m_shopManager.meat;        
                m_pirce = m_shopManager.m_shop.m_meat_Purchase_price;     
                break;
        }
        m_sellButton.button_num = m_button_inventory_num;         
        m_sellText.text = "현재 매입가:"+ m_pirce.ToString();                  
        m_sellText_pre.text = "구매했던 가격:"+m_palyerInven.ItemS[m_button_inventory_num-1].Item_Price.ToString();   //인덱스를 통해 인벤토리에서 아이템의 정보를 읽어 이전에 샀던 가격을 불러옵니다
    }

    public void Sell_Item()//아이템을 파는 함수
    {
        if (m_button_inventory_num != 0)
        {
            if (m_palyerInven.ItemS[m_button_inventory_num - 1].Item_enable)    
            {
                m_palyerInven.Gold += m_pirce;                      
                m_palyerInven.Sell_item(m_button_inventory_num - 1);
            }
        }
    }
}
