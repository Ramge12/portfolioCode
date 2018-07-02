using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideRange : MonoBehaviour {
    //썰매의 자식으로 있는 범위에서 사용하는 클래스 입니다
    public GameObject Player;                   //플레이어
    public GameObject Slide;                    //슬라이드
  
	// Use this for initialization
	void Start () {
        Slide = transform.parent.gameObject;    //썰매는 자식의 부모 오브젝트입니다.
    }
	// Update is called once per frame
	void Update () {
	}
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")                 //플레이어가 충돌할 경우
        {
            Player.GetComponent<PlayerControl>().RideRange = true;                              //플레이어 컨트롤러에 불값을 true로준다
            Player.GetComponent<PlayerControl>().Slide = transform.parent.gameObject;           //플레이어 slide의 이 클래스의 slide를 넣습니다
            transform.parent.parent.gameObject.GetComponent<SlideControl>().Slide_Move = true;  //slideControl에 불값도 true로 바꾸어줍니다
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")              //플레이어가 범위 바깥으로 나가는 경우
        {
            Player.GetComponent<PlayerControl>().RideRange = false;                             //플레이어 컨트롤러에 불값을 true로준다
            Player.GetComponent<PlayerControl>().Slide = null;                                  //플레이어 slide의 이 클래스의 slide를 넣습니다
            transform.parent.parent.gameObject.GetComponent<SlideControl>().Slide_Move = false; //slideControl에 불값도 true로 바꾸어줍니다
        }
    }
}
