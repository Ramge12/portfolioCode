using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoom : MonoBehaviour        //마지막 보스 패턴중 하나인 폭탄 떨구기 관련 클래스입니다
{
    public ParticleSystem explosion;       
    public GameObject Player;              
    public GameObject Boss;                  

    void Start()
    {
        Player = GameObject.Find("Player");
        Boss= GameObject.Find("BossCharactor");
        explosion.Stop();
    }

    void Update()
    {
        if(transform.position.y <0f)Destroy(this.gameObject);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rand")                                    
        {
            playSound("BoxBoomSound");                              
            explosion.Play();                                       
            Boss.GetComponent<MainBoss>().Drop = true;              
        }
        if (other.tag == "Player")                                  
        {
            explosion.Play();
            playSound("BoxBoomSound");
            Player.GetComponent<PlayerCtr>().CharactorFallDown();   
            Boss.GetComponent<MainBoss>().Drop = true;
        }
    }

    void playSound(string snd)
    {
      GameObject.Find(snd).GetComponent<AudioSource>().Play();      //폭발음을 한번 재생합니다
    }
}
