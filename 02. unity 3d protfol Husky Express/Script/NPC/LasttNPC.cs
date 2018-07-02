using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LasttNPC : MonoBehaviour {

    //맵 마지막에 위치한 NPC 통행을 막고있다

    public GameObject talkbox;
    public string Message;    
    public Text TextBox;             


    void Start () {
	}


	void Update () {
	}

    public void Warning_Message()
    {
        talkbox.SetActive(true);      
        talkbox.GetComponent<MessageDelete>().timer = 0;        
        TextBox.text = Message;        
    }
}
