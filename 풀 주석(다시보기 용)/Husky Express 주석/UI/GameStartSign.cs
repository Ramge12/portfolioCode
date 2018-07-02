using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSign : MonoBehaviour {

    //AI 레이싱에서 게임이시작된 후 일정 시간이 지난후 출발할 수 있도록 하는 클래스입니다.

    public Text TimeLine;           //시작시간을 표시할 Text UI
    public GameObject Timer_UI;     //시작시간을 표시할 UI
    public AnimalTimer dog_time;    //개의 레코드 시간을 컨트롤하기 위한 클래스
    public AnimalTimer peng_time;   //펭귄의 레코드 시간을 컨틀ㄹ하기 위한 클래스
    public DogJoyStic m_player;     //플레이어의 속도를 조절하기 위한 클래스
    public ExpressMove m_peng;      //AI의 속도를 조절하기 위한 클래스
    public float LimitTime;         //제한시간
    float timer;                    //시간을 잴 카운터
    bool m_start;                   //시작불값

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        if (!m_start)               //시작하지 않았다면
        {
            timer += Time.deltaTime;//시간을 재어
            TimeLine.text = ((int)(LimitTime - timer)).ToString();  //남은시간을 표시합니다
            if (LimitTime - timer < 0)      //경과 시간이 제한시간을 넘으면
            {
                dog_time.Start_time();      //dog의 레코드 타임을 시작합니다
                peng_time.Start_time();     //Penguin의 레코드 타임을 시작합니다
                Timer_UI.SetActive(false);  //UI도 끕니다
                m_player.MoveSpeed = 5.0f;  //이동속도를 주어 움직일 수 있도록합니다
                m_peng.AI_speed = 4.5f;     //AI도 이동속도를 주어 움직일 수 있도록합니다
                m_start = true;             //제한시간이 다 돼면 이 함수를 더 이상 실행하지않도록합니다
            }
        }
    }
}
