using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour   //플레이어가 공격할수 있는 지점에 대한 범위
{
    //실제 지점보다 큰 구체를 배치하여 플레이어가 구체와 충돌하면 공격할 수 있는 범위에 들어왔다고 판단합니다
    GameObject Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
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
