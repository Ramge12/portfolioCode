using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    GameObject player;          //플레이어
    public GameObject Enemy;    //적 캐릭터 본체

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player"); //플레이어를 찾는다
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (Enemy.GetComponent<Enemy>().currState == ENEMY_STATE.ATTACK)    //적 캐릭터가 공격상태일떄
        {
            if (other.tag == "Player")                                      //플레이어와 충동한다면
            {
                player.GetComponent<PlayerCtr>().Hurt();                    //플레이어의 Hurt함수를 실행한다
            }
        }
    }
}
