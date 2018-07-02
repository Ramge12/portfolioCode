using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCheck : MonoBehaviour    //플레이어가 벽에 충돌했는지 판단하는 클래스
{
    public GameObject player;   
    public bool check;             

    void Start()
    {
    }

    void Update()
    {
        check = player.GetComponent<PlayerCtr>().MovePlayerFoward;  
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag== "Wall")  
        {
            player.GetComponent<PlayerCtr>().MovePlayerFoward = false;  //플레이어가 벽에 닿으면 playerCtr에 forward값에 false를 준다(갈수없다)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            player.GetComponent<PlayerCtr>().MovePlayerFoward = true;//플레이어가 벽에서 나오면 true를 준다 (갈수있다)
        }
    }
}
