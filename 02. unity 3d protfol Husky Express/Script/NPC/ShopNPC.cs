using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : MonoBehaviour {

    public bool Change_price;           //가격 변동을 할지 안할지에 대한 불값

    int mot_Variability;                //변동폭
    public int mot_Purchase_price;      //매입가
    public int mot_Soldier;             //매각가

    //매각가의 최저는 매입가+ 변동폭 보다 같거나 커야한다

    int wood_Variability;               //변동폭
    public int wood_Purchase_price;     //매입가
    public int wood_Soldier;            //매각가

    int meat_Variability;               //변동폭
    public int meat_Purchase_price;     //매입가
    public int meat_Soldier;            //매각가

    public int m_mot_Purchase_price;    //매입가반환
    public int m_mot_Soldier;           //매각가반환
    public int m_wood_Purchase_price;   //매입가반환
    public int m_wood_Soldier;          //매각가반환
    public int m_meat_Purchase_price;   //매입가반환
    public int m_meat_Soldier;          //매각가반환

    public Text m_mot_Text_Purchase;    
    public Text m_mot_Text_Soldier;
    public Text m_wood_Text_Purchase;
    public Text m_wood_Text_Soldier;
    public Text m_meat_Text_Purchase;
    public Text m_meat_Text_Soldier;


    void Start () {
        //변동 폭은 최대갑과 최소값의 절반으로 한다 (너무낮거나 높지 않도록)
        mot_Variability = (mot_Soldier - mot_Purchase_price) / 2;
        wood_Variability = (wood_Soldier - wood_Purchase_price) / 2;
        meat_Variability = (meat_Soldier - meat_Purchase_price) / 2;
    }

	void Update () {

		if(Change_price)//변동이 활성화 되었을떄
        {   //매입 , 매각가는 변동폭 안에서 랜덤으로 결정됩니다.
            m_mot_Purchase_price = Realtime_Change(mot_Variability, mot_Purchase_price);
            m_mot_Soldier = Realtime_Change(mot_Variability, mot_Soldier);
            m_wood_Purchase_price = Realtime_Change(mot_Variability, wood_Purchase_price);
            m_wood_Soldier = Realtime_Change(wood_Variability, wood_Soldier);
            m_meat_Purchase_price = Realtime_Change(mot_Variability, meat_Purchase_price);
            m_meat_Soldier = Realtime_Change(meat_Variability, meat_Soldier);
        }
        else //변동이 비활성화 되었을때 (플레이어가 상점을 활성화 시켯을때)
        {   
            if(m_mot_Text_Purchase) m_mot_Text_Purchase.text= "매입가"+m_mot_Purchase_price.ToString();
            if(m_mot_Text_Soldier) m_mot_Text_Soldier.text= "매각가"+m_mot_Soldier.ToString();
            if(m_wood_Text_Purchase) m_wood_Text_Purchase.text= "매입가" + m_wood_Purchase_price.ToString();
            if(m_wood_Text_Soldier) m_wood_Text_Soldier.text = "매각가" + m_wood_Soldier.ToString(); ;
            if(m_meat_Text_Purchase) m_meat_Text_Purchase.text= "매입가" + m_meat_Purchase_price.ToString();
            if(m_meat_Text_Soldier) m_meat_Text_Soldier.text= "매각가" + m_meat_Soldier.ToString();
        }
	}

    int Realtime_Change(int Variability,int price)  //가격과 가격폭을 입력하면 그 사이값을 랜덤하게 반환합니다.
    {
        int m_Variability = Random.Range(-Variability, Variability);
        return price + m_Variability;
    }
}
