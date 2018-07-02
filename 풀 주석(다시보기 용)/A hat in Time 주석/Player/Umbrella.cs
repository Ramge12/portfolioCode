using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour {

    GameObject Enemy;           //적캐릭터
    GameObject Player;          //플레이어
    bool attack=false;          //공격여부(연속적으로 충돌하는것을 방지하기 위한 불값입니다)
    bool Player_attack = false; //플레이어의 공격모션

    // Use this for initialization
    void Start () {
        Enemy = GameObject.Find("Enemy");   //적캐릭터를 찾습니다
        Player = GameObject.Find("Player"); //플레이어를 찾습니다
    }
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(Player.GetComponent<PlayerCtr>().ps_State == PlayerState.Attack)
        {
            Player_attack = true;   //플레이어가 공격 상태이면 Player_attak에 true를 줍니다
        }
        else
        {
            attack = false;
            Player_attack = false;  //아닐경우 모두 초기화 합니다
        }
        if (other.tag == "Enemy")   //적과 충돌했을 경우
        {
            if (Player_attack==true && attack ==false)  //플레이어는 공격상태이고 attack이 false일떄 
            {
                other.GetComponent<Enemy>().currState = ENEMY_STATE.Hit;    //적캐릭터 상태는 Hit상태로
                attack = true;                          //연속적인 충돌을 막기위해 true로 바꾸어줍니다
            }
        }
    }
}
