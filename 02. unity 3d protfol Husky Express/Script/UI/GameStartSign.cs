using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSign : MonoBehaviour {

    //AI 레이싱에서 게임이시작된 후 일정 시간이 지난후 출발할 수 있도록 하는 클래스입니다.

    public Text TimeLine;           
    public GameObject Timer_UI;     
    public AnimalTimer dog_time;    
    public AnimalTimer peng_time;   
    public DogJoyStic m_player;     
    public ExpressMove m_peng;      
    public float LimitTime;         
    float timer;                    
    bool m_start;                                                       


	void Start () {
	}

	void Update () {
        if (!m_start)                       
        {
            timer += Time.deltaTime;        
            TimeLine.text = ((int)(LimitTime - timer)).ToString();
            if (LimitTime - timer < 0)                                      
            {
                dog_time.Start_time();      
                peng_time.Start_time();     
                Timer_UI.SetActive(false);  
                m_player.MoveSpeed = 5.0f;  
                m_peng.AI_speed = 4.5f;     
                m_start = true;             
            }
        }
    }
}
