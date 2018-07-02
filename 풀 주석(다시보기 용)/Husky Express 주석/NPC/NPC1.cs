using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1 : MonoBehaviour {

    public Text TextBox;            //Text UI
    public GameObject talkbox;      //말풍선 오브젝트
    public string Message;          //메세지stirng
    public string NPC_NAME;         //NPC의 이름

	// Use this for initialization
	void Start (){
	}
	// Update is called once per frame
	void Update (){
	}
    public void TalkNPC()
    {
        talkbox.SetActive(true);    //말풍선을 활성화 시키고
        talkbox.GetComponent<MessageDelete>().timer = 0;    //메시지가 출려된 시간을 카운트하는 수를 초기화
        TextBox.text = Message;     //Text UI에 string을 넣어 
        if(NPC_NAME=="NPC1")transform.GetComponent<FirstNPC>().SetSlide();//NPC1일경우 썰매와 캐릭터를 소환하는 함수를 실행합니다.
    }
}
