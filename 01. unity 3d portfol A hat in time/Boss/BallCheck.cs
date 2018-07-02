using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour {    //마지막 보스가 타고다니는 공에 대한 클래스입니다

    GameObject Player;
    GameObject Boss;


    void Start () {
        Player = GameObject.Find("Player");
        Boss = GameObject.Find("BossCharactor");
    }

	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftWall")Boss.GetComponent<MainBoss>().m_Left = true;
        if (other.tag == "RightWall")Boss.GetComponent<MainBoss>().m_Left = false;//보스가 타고다니는 볼은 벽에 충돌하면 반대방향으로 움직입니다
        if (other.tag == "Player")
        {
            if (Player.GetComponent<PlayerCtr>().ps_State != PlayerState.Jump_Attack && 
                Boss.GetComponent<MainBoss>().BossSt!=MainBoss.Boss_State.Patton2)
            {   //플레이어는 점프 공격중이 아니거나 보스가 패턴2 상태일 경우에만 데미지를 입습니다
                Player.GetComponent<PlayerCtr>().Hurt();    
            }
            if (Boss.GetComponent<MainBoss>().m_Left==true)Boss.GetComponent<MainBoss>().m_Left = false;
            else Boss.GetComponent<MainBoss>().m_Left = true;   //볼은 플레이어와 충돌한 경우에도 반대로 움직힙니다.
        }
    }
}
