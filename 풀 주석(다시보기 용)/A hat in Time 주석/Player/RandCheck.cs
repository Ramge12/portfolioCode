using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCheck : MonoBehaviour {    //플레이어의 다리부분에서 다리에 닿는게 뭔지 판단하는 클래스

    public GameObject Boss;             //보스캐릭터
    public GameObject Player;           //플레이어
    public bool onRand = false;         //땅에 닿았는지 에 대한 불값
    bool DJ_Jump = false;               //더블점프 했는지 불값
    bool PlayerAttack = false;          //플레이어가 공격했는지에 대한 불값 여러번 충돌되는것을 방지하기 위한 불값

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (Player)
        {
            if (Player.GetComponent<PlayerCtr>().ps_State == PlayerState.IDLE)
            {
                PlayerAttack = false;       //플레이어가 IDle상태일때는 attack을 취소한다
            }
        }

    }
    public void Dj_Jump_Check()//바깥쪽 클래스에서 호출하는 함수
    {
        DJ_Jump = true;                 //더블점프를 하면 dj_jump를 true로 바꾼다
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rand")onRand = true;          //발이 닿는 부분 태그가 Rand일 경우 onRand가 트루
        if (other.tag == "HitPoint")DJ_Jump = true;     //발이 닿는 부분 태그가 HitPoint일 경우 더블점프가 트루
        if (other.tag == "Boss")
        {
            if (Player.GetComponent<PlayerCtr>().ps_State == PlayerState.Jump_Attack && PlayerAttack==false)
            {
                PlayerAttack = true;                    //플레이어가 점프 어택상태이고 Playerattack이 false일때
                Boss.GetComponent<MainBoss>().Hurt();   //보스의 hurt함수를 실행한다.
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rand")onRand = false;         //각각 태그의 오브젝트에서 벗어날경우 초기화를 시켜준다
        if (other.tag == "HitPoint") DJ_Jump = false;
    }
}
