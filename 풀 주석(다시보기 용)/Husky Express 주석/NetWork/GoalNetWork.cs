using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalNetWork : MonoBehaviour {

    public Text             m_goal_text;        //골라인 텍스트
    public DogJoyStic       m_player1;          //플레이어1의 스틱값
    public PengJoyStic      m_player2;          //플레이어2의 스틱값
    public MulityPlay       m_multyManager;     //멀티 플레이 클래스매니져
    public GameObject       m_goal_info;        //골정보를 출력할 UI
    public GameObject       Record_button;      //리플레이 기록버튼을 출력할 UI
    public RecordPosition   p1_rec;             //플레이어1의 레코드 정보
    public RecordPosition   p2_rec;             //플레이어2의 레코드 정보
    bool Goal_in;                               //플레이어가 골인했는지에 대한 여부(플레이어1,2무관)
    bool Goal_p1;                               //플레이어1이 골인했는지에 대한 불값
    bool Goal_p2;                               //플레이어2가 골인했는지에 대한 불값

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")                 //Dog가 Player1이므로 Player1이 골에 충돌하면
        {
            if (!Goal_in)                       //아직 한명도 통과 하지 못했다면
            {
                p1_rec.RecordStop();            //Player1의 기록을 정지합니다
                m_goal_info.SetActive(true);    //골인 UI를 출력합니다
                m_multyManager.goal = true;     //멀티매니저에 goal을 true로 둡니다
                m_player1.MoveSpeed = 0;        //플레이어가 움직이지 못하게 Movespeed를 0으로 둡니다
                Goal_in = true;                 //플레이어1이 들어왔으므로 true를 줍니다
                if (!Goal_p1)                   //Goal_P1이 false이므로 true로 바꾸며 경과시간을 text로 출력합니다
                {
                    Goal_p1 = true;
                    m_goal_text.text = "Player 1이 이겼습니다. 기록:" + p1_rec.replayTime.ToString();
                }
            }
            if (Goal_in)                        //이미 골라인을 통과한 사람이 있는 상태에서 Player1이 들어오면
            {
                p1_rec.RecordStop();            //플레이어1의 기록을 멈춘다
                m_player1.MoveSpeed = 0;        //플레이어1의 속도를 0으로 만든다
                if (!Goal_p1)                   //Goal_p1이 false이므로
                {
                    Record_button.SetActive(true);  
                    Goal_p1 = true;             //기록저장 버튼을 출력하며 Goal_p1이 값을 true로 바꾸고 시간을 출력합니다
                    m_goal_text.text = m_goal_text.text + "\n Player1의 기록" + p1_rec.replayTime.ToString();
                }
            }
        }
        if (other.tag == "Penguin")//펭귄은 AI역할이므로 아래에서 적용하는 부분은 player의 반대되는 역할을 합니다
        {
            if (!Goal_in)
            {
                p2_rec.RecordStop();
                m_goal_info.SetActive(true);
                m_multyManager.goal = true;
                m_player2.MoveSpeed = 0;
                Goal_in = true;
                if (!Goal_p2)
                {
                    Goal_p2 = true; //플레이어2(AI)가 이겼다고 출력합니다
                    m_goal_text.text = "Player 2이 이겼습니다. 기록:" + p2_rec.replayTime.ToString();
                }
            }
            if (Goal_in)
            {
                p2_rec.RecordStop();
                m_player2.MoveSpeed = 0;
                if (!Goal_p2)
                {
                    Record_button.SetActive(true);
                    Goal_p2 = true;
                    m_goal_text.text = m_goal_text.text + "\n Player2의 기록" + p2_rec.replayTime.ToString();
                }
            }
        }
    }
}
