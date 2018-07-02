using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SellButton : MonoBehaviour {

    public int button_num;                              
    public ButtonState[] button = new ButtonState[8];            


    void Start () {
	}

	void Update () {	
	}

    public void Sell_button_Click()
    {
       if(button_num!=0) button[button_num-1].Sell_Item();
    }
}
