using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public struct Item_Info//아이템 정보 구조체
    {
        public Sprite Item_image;                   //아이템 이미지
        public int      Item_Price;                 //아이템 가격
        public int      Item_num;                   //아이템 넘버
        public int      Item_inven_Num;             //아이템의 인벤토리에서의 넘버
        public bool     Item_enable;                //아이템이 존재하는지에 대한 불값
        public string Item_Name;                    //아이템 이름
    };

    public Button[] buttonS = new Button[8];        
    public Item_Info[] ItemS = new Item_Info[8];    
    public Image[] inventory_image = new Image[8];  
    public int Gold;                                
    public Text GoldText;                                   
    
    void Start()
    {
        Gold = 2000;//시작소지골드
        for (int i = 0; i < ItemS.Length; i++)
        {
            ItemS[i].Item_enable = false;                
        }
    }

	void Update ()
    {
        if(GoldText)GoldText.text = Gold.ToString();
        Shop_UI_player_inventory();                      
    }

    public void Add_Item(int num,int money,Sprite image)    
    {
        switch (num)                                        
        {
            case 0:                                         
                Item_Info add_tem;
                add_tem.Item_Name = "못";
                add_tem.Item_Price = money;
                add_tem.Item_image = image;
                add_tem.Item_enable = true;
                add_tem.Item_num = 1;                       
                for (int i=0; i< ItemS.Length;i++)
                {
                    if(ItemS[i].Item_enable == false)       
                    {
                        add_tem.Item_inven_Num = i;
                        ItemS[i] = add_tem;
                        break;
                    }
                }
                break;
            case 1:                                                       
                Item_Info add_tem2;
                add_tem2.Item_Name = "나무";
                add_tem2.Item_Price = money;
                add_tem2.Item_image = image;
                add_tem2.Item_enable = true;
                add_tem2.Item_num = 2;
                for (int i = 0; i < ItemS.Length; i++)           
                {
                    if (ItemS[i].Item_enable == false)
                    {
                        add_tem2.Item_inven_Num = i;       
                        ItemS[i] = add_tem2;
                        break;
                    }
                }
                break;
            case 2:                                        
                Item_Info add_tem3;
                add_tem3.Item_Name = "고기";
                add_tem3.Item_Price = money;
                add_tem3.Item_image = image;
                add_tem3.Item_enable = true;               
                add_tem3.Item_num =3;
                for (int i = 0; i < ItemS.Length; i++)     
                {
                    if (ItemS[i].Item_enable == false)
                    {
                        add_tem3.Item_inven_Num = i;                                                   
                        ItemS[i] = add_tem3;
                        break;
                    }
                }
                break;
        }
    }

    public void Shop_UI_player_inventory()
    {
        for (int i = 0; i < ItemS.Length; i++) 
        {
            if (ItemS[i].Item_enable)          
            {
                switch (ItemS[i].Item_num)            
                {
                    case 1:
                        buttonS[i].image.sprite = ItemS[i].Item_image;
                        buttonS[i].GetComponent<ButtonState>().m_button_Num = 0;
                        buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = i + 1;
                        break;
                    case 2:
                        buttonS[i].image.sprite = ItemS[i].Item_image;
                        buttonS[i].GetComponent<ButtonState>().m_button_Num = 1;
                        buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = i + 1;
                        break;
                    case 3:
                        buttonS[i].image.sprite = ItemS[i].Item_image;
                        buttonS[i].GetComponent<ButtonState>().m_button_Num = 2;
                        buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = i + 1;
                        break;
                    default:
                        buttonS[i].image.sprite = null;
                        buttonS[i].GetComponent<ButtonState>().m_button_Num = 10;
                        buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = 0;
                        break;
                }
            }
            else
            {
                if (buttonS[i])
                {
                    buttonS[i].image.sprite = null;
                    buttonS[i].GetComponent<ButtonState>().m_button_Num = 10;
                    buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = 0;
                }
            }
        }
    }

    public void Sell_item(int itemNum)//아이템 번호를 통한 Sell_item
    {
        for (int i = 0; i < ItemS.Length; i++)      //아이템 리스트 중에서
        {
            if(ItemS[i].Item_inven_Num == itemNum)  //일치하는 번호를 찾아서
            {
                ItemS[i].Item_enable = false;       //인벤토리 칸의 정보를 초기화 시켜줍니다
                ItemS[i].Item_image = null;         //(팔렸으므로 초기화)
                ItemS[i].Item_inven_Num = 0;
                ItemS[i].Item_Name = "";
                ItemS[i].Item_num = 0;
                ItemS[i].Item_Price = 0;
            }
        }
    }

    public void Inventroy_UI_player_inventory()//플레이어가 인벤토리를 켯을 때의 UI
    {
        for (int i = 0; i < ItemS.Length; i++)  //인벤토리 리스트 에서
        {
            if (ItemS[i].Item_enable)           //인벤토리 칸에 아이템이 있다면
            {
                switch (ItemS[i].Item_num)      //아이템 번호에 따라
                {
                    case 1:                     //이미지를 넣어 준다
                        inventory_image[i].sprite = ItemS[i].Item_image;
                        break;
                    case 2:
                        inventory_image[i].sprite = ItemS[i].Item_image;
                        break;
                    case 3:
                        inventory_image[i].sprite = ItemS[i].Item_image;
                        break;
                }
            }
        }
    }

    public int LegnthCount()//인벤토리 정보 카운트 함수
    {
        int m_LengthCount = 0 ;
        for (int i = 0; i < ItemS.Length; i++)
        {
            if(ItemS[i].Item_enable)m_LengthCount++;    //인벤토리 칸에 아이템이 있는 경우에만 카운트를 한다
        }
       return m_LengthCount;
    }

}
