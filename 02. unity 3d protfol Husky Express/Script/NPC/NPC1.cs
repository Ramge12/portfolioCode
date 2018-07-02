using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1 : MonoBehaviour {

    public Text TextBox;                         
    public GameObject talkbox;      
    public string Message;          
    public string NPC_NAME;         


	void Start (){
	}

	void Update (){
	}

    public void TalkNPC()
    {
        talkbox.SetActive(true);   
        talkbox.GetComponent<MessageDelete>().timer = 0;
        TextBox.text = Message;
        if(NPC_NAME=="NPC1")transform.GetComponent<FirstNPC>().SetSlide();
    }
}
