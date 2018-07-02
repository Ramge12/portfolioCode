using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTimer : MonoBehaviour {

    //동물들이 몇초간 달렸는지 재는 카운터입니다

    bool Start_timer;                   //시간 카운트의 시작 여부
    public float m_Animal_Timer;        //동물 카운터를 기록할 카운터

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        if (Start_timer) m_Animal_Timer += Time.deltaTime;  //카운팅이 시작되면 deltaTime을 이용해 카운팅을 시작합니다
    }
    public void Start_time()
    {
        Start_timer = true;             //카운팅을 시작할 수 있도록 불값을 바꾸어 줍니다
    }
}
