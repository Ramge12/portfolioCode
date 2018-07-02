using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDelete : MonoBehaviour {

    //일정시간이 지나면 메시지 창을 끄는 클래스입니다

    public float timer;                         //시간 카운터
    public float limitTime=3.0f;                //지울시간

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;                //시간이 경고되면 비활성화 시킨다
        if(timer> limitTime)this.gameObject.SetActive(false);
    }
}
