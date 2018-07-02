using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SellButton : MonoBehaviour {

    public int button_num;                              //누른 버튼의 번호
    public ButtonState[] button = new ButtonState[8];   //검색할 버튼

    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {	
	}
    public void Sell_button_Click()//
    {
       if(button_num!=0) button[button_num-1].Sell_Item();  //번호로 인덱스를 찾아 아이템을 팝니다
    }
}
