using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour  //플레이어가 근처에 있는지 체크하는 클래스
{
    //실제 코뿔소 보다 큰 구체를 두어 플레이어가 이 구체에 충돌하면 코뿔소 근처에 있다는걸 알수 있도록합니다
    public GameObject Player;      
    public GameObject Message;     
    public GameObject apple;          


    void Start()
    {

    }

    void Update()
    {
        transform.localEulerAngles = transform.parent.localEulerAngles; 
        Player.GetComponent<PlayerCtr>().Ride_Rhino(this.gameObject);                       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Player.GetComponent<PlayerCtr>().Rhino_here = true; 
           if(apple.activeSelf) Message.SetActive(true);                                           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")     
        {
            Player.GetComponent<PlayerCtr>().Rhino_here = false;
            if (apple.activeSelf) Message.SetActive(false);
        }
    }


   
}
