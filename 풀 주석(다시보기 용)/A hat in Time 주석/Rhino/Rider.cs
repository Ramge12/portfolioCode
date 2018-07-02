using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour  //플레이어가 근처에 있는지 체크하는 클래스
{
    //실제 코뿔소 보다 큰 구체를 두어 플레이어가 이 구체에 충돌하면 코뿔소 근처에 있다는걸 알수 있도록합니다
    public GameObject Player;       //플레이어
    public GameObject Message;      //메시지 박스
    public GameObject apple;        //사과(키아이템)

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = transform.parent.localEulerAngles; //이 오브젝트는 부모의 회전값을 같이 사용합니다
        Player.GetComponent<PlayerCtr>().Ride_Rhino(this.gameObject);   //플레이어의 ride함수 실행
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")      //플레이어와 충돌한다면
        {
            Player.GetComponent<PlayerCtr>().Rhino_here = true; //플레이어 함수에 코뿔소가 있다는 정보를 보내줍니다
           if(apple.activeSelf) Message.SetActive(true);        //메시지박스 출력
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")      //플레이어가 벗어난다면
        {
            Player.GetComponent<PlayerCtr>().Rhino_here = false;
            if (apple.activeSelf) Message.SetActive(false);
        }
    }


   
}
