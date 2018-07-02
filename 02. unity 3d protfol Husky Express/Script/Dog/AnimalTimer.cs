using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTimer : MonoBehaviour {

    //동물들이 몇초간 달렸는지 재는 카운터입니다

    bool Start_timer;               
    public float m_Animal_Timer;     


	void Start () {
	}

	void Update () {
        if (Start_timer) m_Animal_Timer += Time.deltaTime;  
    }

    public void Start_time()
    {
        Start_timer = true;             
    }
}
