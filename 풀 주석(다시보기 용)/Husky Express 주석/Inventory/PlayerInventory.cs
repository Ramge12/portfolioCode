using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public struct Item_Info                         //아이템 정보를 담을 구조체
    {
        public Sprite Item_image;                   //아이템 이미지
        public int      Item_Price;                 //아이템 가격
        public int      Item_num;                   //아이템 넘버
        public int      Item_inven_Num;             //아이템의 인벤토리에서의 넘버
        public bool     Item_enable;                //아이템이 존재하는지에 대한 불값
        public string Item_Name;                    //아이템 이름
    };
    public Button[] buttonS = new Button[8];        //인벤토리 칸 8칸의 대한 버튼
    public Item_Info[] ItemS = new Item_Info[8];    //인벤토리 8칸에 대한 아이템
    public Image[] inventory_image = new Image[8];  //인벤토리 8칸에 대한 이미지
    public int Gold;                                //플레이어 소지골드
    public Text GoldText;                           //플레이어가 소지한 골드를 표시할 텍스트
    
    void Start()
    {
        Gold = 2000;                                //시작소지골드
        for (int i = 0; i < ItemS.Length; i++)
        {
            ItemS[i].Item_enable = false;           //인벤토리에 아이템이 없으므로 모든 존재값에 false를 준다
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if(GoldText)GoldText.text = Gold.ToString();//소지 Gold를 텍스트로 출력한다
        Shop_UI_player_inventory();                 //플레이어 인벤톨 UI에서 실행할 함수
    }
    public void Add_Item(int num,int money,Sprite image)    //아이템 번호, 가격, 이미지를 가져온다
    {
        switch (num)                                        //아이템 번호로 구분한다
        {
            case 0:                                         //아이템 번호 0번 못일 경우
                Item_Info add_tem;
                add_tem.Item_Name = "못";
                add_tem.Item_Price = money;
                add_tem.Item_image = image;
                add_tem.Item_enable = true;
                add_tem.Item_num = 1;                       //아이템 정보를 모두 기록한다
                for (int i=0; i< ItemS.Length;i++)
                {
                    if(ItemS[i].Item_enable == false)       //인벤토리중 빈칸에 아이템을 추가합니다
                    {
                        add_tem.Item_inven_Num = i;
                        ItemS[i] = add_tem;
                        break;
                    }
                }
                break;
            case 1:                                         //아이템 번호 1번 나무일 경우
                Item_Info add_tem2;
                add_tem2.Item_Name = "나무";
                add_tem2.Item_Price = money;
                add_tem2.Item_image = image;
                add_tem2.Item_enable = true;
                add_tem2.Item_num = 2;
                for (int i = 0; i < ItemS.Length; i++)      //아이템 정보를 모두 기록하여
                {
                    if (ItemS[i].Item_enable == false)
                    {
                        add_tem2.Item_inven_Num = i;        //마찬가지로 인벤토리중 빈칸에 아이템을 추가합니다
                        ItemS[i] = add_tem2;
                        break;
                    }
                }
                break;
            case 2:                                         //아이템 번호 2번 고기일 경우
                Item_Info add_tem3;
                add_tem3.Item_Name = "고기";
                add_tem3.Item_Price = money;
                add_tem3.Item_image = image;
                add_tem3.Item_enable = true;                //아이템정보를 기록한후
                add_tem3.Item_num =3;
                for (int i = 0; i < ItemS.Length; i++)      //인벤토리중 빈칸을 찾아
                {
                    if (ItemS[i].Item_enable == false)
                    {
                        add_tem3.Item_inven_Num = i;        //추가한다
                        ItemS[i] = add_tem3;
                        break;
                    }
                }
                break;
        }
    }
    public void Shop_UI_player_inventory()//상점창에서 보여주는 플레이어 인벤토리의 경우
    {
        for (int i = 0; i < ItemS.Length; i++)  //인벤토리창 전체에서
        {
            if (ItemS[i].Item_enable)           //아이템이 있다면
            {
                switch (ItemS[i].Item_num)      //아이템 넘버를 통해서
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
                        //각각 아이템 번호를 따라 버튼정보를 교체해여 ButtonState버튼 정보값을 교체해서 버튼을 만들어줍니다
                }
            }
            else
            {
                if (buttonS[i])
                {
                    buttonS[i].image.sprite = null;
                    buttonS[i].GetComponent<ButtonState>().m_button_Num = 10;
                    buttonS[i].GetComponent<ButtonState>().m_button_inventory_num = 0;
                    //없을 경우 아무것도 출력하지않도록 합니다
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
