using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalNetWork : MonoBehaviour {

    public Text             m_goal_text;                                      
    public DogJoyStic       m_player1;        
    public PengJoyStic      m_player2;        
    public MulityPlay       m_multyManager;   
    public GameObject       m_goal_info;      
    public GameObject       Record_button;    
    public RecordPosition   p1_rec;           
    public RecordPosition   p2_rec;           
    bool Goal_in;                           
    bool Goal_p1;                             
    bool Goal_p2;                             


	void Start () {
	}


	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")             
        {
            if (!Goal_in)                     
            {
                p1_rec.RecordStop();           
                m_goal_info.SetActive(true);   
                m_multyManager.goal = true;    
                m_player1.MoveSpeed = 0;       
                Goal_in = true;                
                if (!Goal_p1)                    
                {
                    Goal_p1 = true;
                    m_goal_text.text = "Player 1이 이겼습니다. 기록:" + p1_rec.replayTime.ToString();
                }
            }
            if (Goal_in)                       
            {
                p1_rec.RecordStop();           
                m_player1.MoveSpeed = 0;       
                if (!Goal_p1)                                                         
                {
                    Record_button.SetActive(true);  
                    Goal_p1 = true;           
                    m_goal_text.text = m_goal_text.text + "\n Player1의 기록" + p1_rec.replayTime.ToString();
                }
            }
        }
        if (other.tag == "Penguin")
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
                    Goal_p2 = true; 
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
