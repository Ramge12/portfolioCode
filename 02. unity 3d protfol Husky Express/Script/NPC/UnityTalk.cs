using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityTalk : MonoBehaviour {

    public Text textBox;           
    public GameObject talkBox;         
    public ExpressMove m_move;                

    bool first;                     
    bool second;                    
    bool first_time;                
    bool Second_time;               
    bool three_time;                
    float m_timer;                  
    float first_timer;              
    float Second_timer;             
    float three_timer;              

    void Start()
    {
        first = false;       
        second = false;
    }

	void Update ()
    {
        m_timer += Time.deltaTime;
        if(m_timer>5.0f && first==false)    
        { 
            textBox.text = "안녕, 겨울 마을에 어서와!\n 너도 무역을 목적으로 왔다고 들었어.";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            first = true;
            m_move.AI_speed = 5.0f;        
        }
        else if (m_timer > 8.5f&& second==false)   
        {
            textBox.text = "이 마을에는 3개의 무역소가있는데.\n 안내해 줄태니 날 따라와";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            second = true;
        }
        if (first_time) go_first();   
        if (Second_time) go_second();
        if (three_time) go_three();
    }

    public void check_fist_csher()
    {
        textBox.text = "저쪽에 보이는게 스티븐네 무역소야.\n 못을 아주 싸게 판다구";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }

    public  void check_Second_csher()
    {
        textBox.text = "저쪽에 보이는게 할아버지네 무역소야.\n 나무가 저렴하다고 들었어";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }

    public void check_Three_csher()
    {
        textBox.text = "저쪽에 보이는게 IF네 무역소야.\n 이 마을의 마지막 무역소야";
        talkBox.SetActive(true);
        talkBox.GetComponent<MessageDelete>().timer = 0;
    }

    public void check_first() 
    {
        first_time = true;
    }

    public void check_Second()  
    {
        Second_time = true;
    }

    public void check_three()   
    {
        three_time = true;
    }

    public void go_first()
    {
        first_timer += Time.deltaTime;
        if(first_timer>5.0f)
        {
            textBox.text = "다 둘러본거야? 이제간다?";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            first_time = false;
        }
    }

    public void go_second()
    {
        Second_timer += Time.deltaTime;
        if (Second_timer > 5.0f)
        {
            textBox.text = "다음이 마지막이야. 어서 가자";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            Second_time = false;
        }
    }

    public void go_three()
    {
        three_timer += Time.deltaTime;
        if (three_timer > 5.0f)
        {
            textBox.text = "이제 끝났어, 마을 외각쪽으로 가볼까?";
            talkBox.SetActive(true);
            talkBox.GetComponent<MessageDelete>().timer = 0;
            three_time = false;
        }
    }
}
