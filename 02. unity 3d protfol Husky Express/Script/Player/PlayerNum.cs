using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNum : MonoBehaviour {
    
    //멀티 플레이에서 플레이어를 구분하기 위한 클래스

    public int player_select_Num;       //플레이어 넘버를 지정합니다


    void Start () {
        player_select_Num = 0;          //지정되지않을 경우 초기값을 0으로 줍니다
    }
	

	void Update () {
		
	}
}
