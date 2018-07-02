using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPoint : MonoBehaviour {//골인 지점에서 실행할 클래스

    public Text GoalText;                                 
    public GameObject GoalMessge;             
    public GameObject RecordButton;           
    public RecordPosition dog_record;         
    public RecordPosition peng_record;        
    bool m_Goal;                              


    void Start () {	
	}

	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dog")                 
        {
            if (!m_Goal)                        
            {
                GoalMessge.SetActive(true);
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = "Player가 이겼습니다 \n 기록 :  " + time.ToString();
                m_Goal = true;
            }
            else                               
            {
                float time = other.gameObject.transform.GetComponent<AnimalTimer>().m_Animal_Timer;
                GoalText.text = GoalText.text + "\n Player기록 :" + time.ToString();
                RecordButton.SetActive(true);
                dog_record.RecordStop();        
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
