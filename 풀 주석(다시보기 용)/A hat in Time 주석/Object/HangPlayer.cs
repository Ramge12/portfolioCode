using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangPlayer : MonoBehaviour
{
    GameObject player;      //플레이어
    bool hang = false;      //매달린지에 대한 불값

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player"); //플레이어를 찾습니다
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hang")        //충돌한 오브젝트의 태그가 Hang인 경우
        {
            player.GetComponent<PlayerCtr>().HangPlayer(transform.position.y);  //높이값을 플레이어의 HangPlayer에 전해줍니다
            hang = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hang")
        {
            player.GetComponent<PlayerCtr>().HangOutPlayer();   //충돌한 오브젝트에서 벗어난 경우 Player의 HangOutPlayer함수를 실행합니다
            hang = false;
        }
    }
}
