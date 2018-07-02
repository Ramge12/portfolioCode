using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour {
    public Vector3 HeadOn;                      //플레이어의 머리위에 표시할 경우의 위치값

    GameObject player;                          //플레이어
    bool onPlayer=false;                        //플레이어가 이 아이템을 획득 불값
    float turnSpeed = 10.0f;                    //회전속도

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");     //플레이어를 찾는다
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, turnSpeed * Time.deltaTime,0);                      //이 오브젝트를 회전시킵니다
        if(onPlayer)transform.position = player.transform.position + HeadOn;    //플레이어가 획득한 경우 플레이어 머리 위쪽 자리잡습니다
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          player.GetComponent<PlayerCtr>().ItemGet(this.gameObject);            //플레이어와 충돌하면 이 오브젝트 정보를 플레이어의 itemget함수로 넘깁니다
          onPlayer = true;
        }
    }
}
