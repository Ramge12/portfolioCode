using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour   
{
    //실제 지점보다 큰 구체를 배치하여 플레이어가 구체와 충돌하면 공격할 수 있는 범위에 들어왔다고 판단합니다
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.gameObject.GetComponent<PlayerCtr>().DubleJumpAttack=true;
        }
    }
}
