using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {  
    
    //플레이어의 점수를 기록하기위한 클래스 입니다, 시간 , 돈을 기록합니다

    public PlayerInventory p_inven;                 
    public float timer;                    
    public int player_gold;                

	void Start () {
		
	}
	
	void Update () {
        timer += Time.deltaTime;           
        player_gold = p_inven.Gold;        
    }
}
