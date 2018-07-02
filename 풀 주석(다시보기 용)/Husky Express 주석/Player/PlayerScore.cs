using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {  //플레이어의 점수를 기록하기위한 클래스 입니다, 시간 , 돈을 기록합니다
    public PlayerInventory p_inven;         //소지 골드를 가져오기 위해 인벤토리 클래스를 가져옵니다
    public float timer;                     //전체 시간을 기록하기 위해 시간을 카운트합니다
    public int player_gold;                 //플레이어의 골드를 기록하기위한 변수
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;            //시간을 잽니다
        player_gold = p_inven.Gold;         //골드를 기록합니다
    }
}
