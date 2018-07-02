using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPoint : MonoBehaviour {

    public Text GoalText;                       //골일 할 때 출력할 텍스트
    public GameObject GoalMessge;               //골 메세지를 출력할 TEXT UI
    public GameObject RecordButton;             //리플레이를 기록할 리플레이 버튼
    public RecordPosition dog_record;           //dog의 리플레이 클래스
    public RecordPosition peng_record;          //pengiuin의 리플레이 클래스
    bool m_Goal;                                //골인 했는지에 대한 불값

    // Use this for initialization
    void Start () {	
	}
	// Update is called once per frame
	void Update () {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dog")                    //골라인에 Dog가 들어온경우
        {
            if (!m_Goal)                        //플레이어 승리 메세지와 플레이 시간을 표시합니다          
            {
                GoalMessge.SetActive(true);
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = "Player가 이겼습니다 \n 기록 :  " + time.ToString();
                m_Goal = true;
            }
            else                                //플레이어가 승리했다고 표시하고, 경과시간과 팽귄이 골라인까지 걸린 시간을 표시하고 레코드 버튼을 활성화 시킵니다
            {
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = GoalText.text + "\n Player기록 :" + time.ToString();
                RecordButton.SetActive(true);
                dog_record.RecordStop();        //리플레이 클래스에서는 녹화를 정지합니다
                peng_record.RecordStop();
            }
        }
        if (other.tag == "Penguin")
        {
            if (!m_Goal)
            {
                GoalMessge.SetActive(true);
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = "Player가 졌습니다 \n AI기록 : "+time.ToString();
                m_Goal = true;
            }
            else
            {
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = GoalText.text + "\n AI기록 :" + time.ToString();
                RecordButton.SetActive(true);
                dog_record.RecordStop();
                peng_record.RecordStop();
            }
        }
    }
}
