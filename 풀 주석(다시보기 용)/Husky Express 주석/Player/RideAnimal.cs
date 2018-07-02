using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideAnimal : MonoBehaviour {
    //플레이어가 썰매를 탈때 dog인지 penguin인지 구분하여 타기위한 함수입니다.
    public DogMove dog;                 
    public DogJoyStic dogjoystick;      //dog를 탓을 경우 필요한 클래스
    public PenguinMove peng;
    public PengJoyStic pengjoystick;    //penguin을 탓을 경우 필요한 클래스
    GameObject slide;                   //썰매

    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
        if(transform.GetComponent<PlayerControl>().slideRide)   //플레이어가 썰매에 탑승 했을 떄
        {
            if (transform.parent.gameObject.tag == "DogSlide")  //개썰매일 경우
            {
                dog.enabled = true;
                dogjoystick.enabled = true;
                if(peng)peng.enabled = false;
                if(pengjoystick)pengjoystick.enabled = false;   //개썰매 클래스는 활성화 시키고 팽귄클래스는 비활성화 시킵니다.
            }
            if (transform.parent.gameObject.tag == "PengSlide") //펭귄썰매일 경우
            {
                dog.enabled = false;
                dogjoystick.enabled = false;
                if (peng) peng.enabled = true;
                if (pengjoystick) pengjoystick.enabled = true;  //팽귄썰매 클래스는 활성화 시키고 개클래스는 비활성화 시킵니다.
            }
        }

    }
}
