using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScipts : MonoBehaviour {

    GameObject Player;              //플레이어
    public GameObject Message;      //메세지 박스

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player"); //플레이어를 찾는다
    }
	// Update is called once per frame
	void Update () {
        Vector3 targetDir = Player.transform.position - transform.position;             //자신과 대상의 방향을 파악합니다
        targetDir.y = 0;                                                                //높이값을 0으로 둡니다
        float dist = Vector3.Distance(Player.transform.position, transform.position);   //dist는 자신과 대상의 거리를 나타냅니다
        if (dist < 15f)Message.SetActive(true);                                         //거리가 15이내 일때마 메시지를 출력합니다
        else Message.SetActive(false);
    }
}
