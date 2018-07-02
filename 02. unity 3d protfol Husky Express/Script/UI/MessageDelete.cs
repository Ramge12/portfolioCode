using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDelete : MonoBehaviour {

    //일정시간이 지나면 메시지 창을 끄는 클래스입니다

    public float timer;                 
    public float limitTime=3.0f;                  


	void Start () {
	}

	void Update () {
        timer += Time.deltaTime;            
        if(timer> limitTime)this.gameObject.SetActive(false);
    }
}
