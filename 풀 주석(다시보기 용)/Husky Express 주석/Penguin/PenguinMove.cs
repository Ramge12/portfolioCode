using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMove : MonoBehaviour {
    //플레이어가 펭귄을 타고다닐경우
    public PlayerControl Player;    //플레이어 컨트롤러

    // Use this for initialization
    void Start(){
    }
    // Update is called once per frame
    void Update()
    {
        //플레이어가 펭귄의 썰매에 탈경우 PengJoystic를 활성화시킵니다
        if (Player.slideRide)gameObject.GetComponent<PengJoyStic>().enabled = true;
        else gameObject.GetComponent<PengJoyStic>().enabled = false;
    }
}
