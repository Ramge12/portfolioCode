using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstNPC : MonoBehaviour {
    //single모드에서 첫 NPC에서 사용할 클래스
    public GameObject Slide;                //썰매와
    public GameObject NonPlaybleCharactor;  //상대 캐릭터

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void SetSlide()                  //플레이어가 탈 썰매와 상대 캐릭터를 생성하는 함수
    {
        Slide.SetActive(true);
        NonPlaybleCharactor.SetActive(true);
    }
}
