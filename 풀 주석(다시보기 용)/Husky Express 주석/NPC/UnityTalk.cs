using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityTalk : MonoBehaviour {

    public Text textBox;            // Text UI
    public GameObject talkBox;      // 대화창
    public ExpressMove m_move;      // 유니티 캐릭터가 이동할때 필요로한 클래스

    bool first;                     //가장 처음 하는 대화
    bool second;                    //두번째로 하는 대화
    bool first_time;                //첫번째 대화가 끝나고 출발하는 것에 대한 불값
    bool Second_time;               //두번째 대화가 끝나고 출발하는 것에 대한 불값
    bool three_time;                //세번째 대화가 끝나고 출발하는 것에 대한 불값
    float m_timer;                  //시간을 체크하는 카운터
    float first_timer;              //첫번쨰 대화가 끝나고 시간을 재는 카운터
    float Second_timer;             //두번째 대화가 끝나고 시간을 재는 카운터
    float three_timer;              //세번째 대화가 끝나고 시간을 재는 카운터

    void Start()
    {
        first = false;              //불값을 미리 초기화 시킵니다
        second = false;
    }
	void Update ()
    {
        m_timer += Time.deltaTime;
        if(m_timer>5.0f && first==false)    //가장 처음에 생성되고 5초후 이동하도록 합니다
        {
            textBox.text = "안녕, 겨울 마을에 어서와!\n 너도 무역을 목적으로 왔다고 들었어.";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            first = true;
            m_move.AI_speed = 5.0f;         //속도값을 주어 움직일수 있도록합니다
        }
        else if (m_timer > 8.5f&& second==false)    //8.5초 뒤에 다음 대화를 진행하도록 합니다.
        {
            textBox.text = "이 마을에는 3개의 무역소가있는데.\n 안내해 줄태니 날 따라와";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            second = true;
        }
        if (first_time) go_first();     //3번의 충돌이 있는데, 각 충돌에 맞춰서 함수를 실행합니다.
        if (Second_time) go_second();
        if (three_time) go_three();
    }
    public void check_fist_csher()//NPC마다 가지고있는 영역에 충돌하면 멈춰서서 NPC에 대한 설명을 합니다.
    {
        textBox.text = "저쪽에 보이는게 스티븐네 무역소야.\n 못을 아주 싸게 판다구";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }
    public  void check_Second_csher()//NPC마다 가지고있는 영역에 충돌하면 멈춰서서 NPC에 대한 설명을 합니다.
    {
        textBox.text = "저쪽에 보이는게 할아버지네 무역소야.\n 나무가 저렴하다고 들었어";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }
    public void check_Three_csher()//NPC마다 가지고있는 영역에 충돌하면 멈춰서서 NPC에 대한 설명을 합니다.
    {
        textBox.text = "저쪽에 보이는게 IF네 무역소야.\n 이 마을의 마지막 무역소야";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }
    public void check_first()   //각 NPC마다 상점을 끄면 불값을 바꾸어줍니다
    {
        first_time = true;
    }
    public void check_Second()  //각 NPC마다 상점을 끄면 불값을 바꾸어줍니다
    {
        Second_time = true;
    }
    public void check_three()   //각 NPC마다 상점을 끄면 불값을 바꾸어줍니다
    {
        three_time = true;
    }
    public void go_first()//함수 실행후 시간을 체크하여 대화상자를 출력합니다
    {
        first_timer += Time.deltaTime;
        if(first_timer>5.0f)//5초후 대화상자를 출력합니다
        {
            textBox.text = "다 둘러본거야? 이제간다?";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            first_time = false;
        }
    }
    public void go_second()//함수 실행후 시간을 체크하여 대화상자를 출력합니다
    {
        Second_timer += Time.deltaTime;
        if (Second_timer > 5.0f)//5초후 대화상자를 출력합니다
        {
            textBox.text = "다음이 마지막이야. 어서 가자";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            Second_time = false;
        }
    }
    public void go_three()//함수 실행후 시간을 체크하여 대화상자를 출력합니다
    {
        three_timer += Time.deltaTime;
        if (three_timer > 5.0f)// 5초후 대화상자를 출력합니다
        {
            textBox.text = "이제 끝났어, 마을 외각쪽으로 가볼까?";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            three_time = false;
        }
    }
}
