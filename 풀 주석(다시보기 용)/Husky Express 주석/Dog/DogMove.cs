using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour {

    public PlayerControl Player;        //플레이어
    public Animator MainDog;            //중점을 잡을 개, 나머지 개는 이 개를 따라 뜁니다
    public Animator DummyDog1;          //MainDog를 따라 뛸 개들
    public Animator DummyDog2;

    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
		if(Player.slideRide)gameObject.GetComponent<DogJoyStic>().enabled=true; //플레이어가 썰매에 탑승하면 DogJoystic을 활성화
        else gameObject.GetComponent<DogJoyStic>().enabled = false;             //아닐경우 비활성ㅎ
        if (MainDog.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)     //MainDog의 애니메이션 진행도가 0.75가 넘어갈떄마다
        {
            DummyDog1.speed = Random.Range(0.5f, 1.0f);                         //DummyDog의 속도를 무작위로 결정한다
            DummyDog2.speed = Random.Range(0.5f, 1.0f)-0.1f;
        }
        if (MainDog.GetBool("Run"))                                             //MainDog가 뛸때마다 다른 개들도 뛰는 애니메이션이 되도록 한다
        {
            DummyDog1.SetBool("Run", true);
            DummyDog2.SetBool("Run", true);
        }
        else
        {
            DummyDog1.SetBool("Run", false);
            DummyDog2.SetBool("Run", false);
        }
	}
}
