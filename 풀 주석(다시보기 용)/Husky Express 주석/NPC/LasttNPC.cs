using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LasttNPC : MonoBehaviour {
    //맵 마지막에 위치한 NPC 통행을 막고있다
    public GameObject talkbox;              //대화 상자
    public string Message;                  //메시지 string문
    public Text TextBox;                    //대화상자에 출력할 message UI

    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void Warning_Message()
    {
        talkbox.SetActive(true);            //대화상자 활성화
        talkbox.GetComponent<MessageDelete>().timer = 0;        //메세지 넘기기 위한 타이머를 0으로 둡니다
        TextBox.text = Message;             //대화상자에 대화문을 넣습니다
    }
}
