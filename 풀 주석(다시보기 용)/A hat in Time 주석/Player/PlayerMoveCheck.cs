using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCheck : MonoBehaviour    //플레이어가 벽에 충돌했는지 판단하는 클래스
{
    public GameObject player;   //플레이어
    public bool check;          //충돌여부

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        check = player.GetComponent<PlayerCtr>().MovePlayerFoward;  //check값은 playerCtr에서 받아온다
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag== "Wall")  //tag가 Wall인 콜라이더와 충돌할 경우
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
