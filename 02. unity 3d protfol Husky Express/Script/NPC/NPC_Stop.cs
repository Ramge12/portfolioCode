using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Stop : MonoBehaviour {

    //싱글모드에 유니티 캐릭터가 중간에 멈춰서 마을을 안내할떄 사용하는 클래스입니다.

    public UnityTalk m_talk;                  
    public int num;                                


	void Start () {
	}

	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Penguin")                               
        {
            if(other.GetComponent<ExpressMove>())           
            {
                if (num == 1)m_talk.check_fist_csher();             //NPC마다 가지고있는 collider에 충돌했을때 번호를 채크하여
                if (num == 2) m_talk.check_Second_csher();          //대화문을 출력합니다
                if (num == 3)m_talk.check_Three_csher();
                other.GetComponent<ExpressMove>().AI_speed = 0;     //멈춰서서 대화를 하기 때문에 speed를 0으로 둡니다
            }
        }
    }
}
