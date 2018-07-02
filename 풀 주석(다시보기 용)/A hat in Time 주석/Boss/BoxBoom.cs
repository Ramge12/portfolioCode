using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoom : MonoBehaviour        //마지막 보스 패턴중 하나인 폭탄 떨구기 관련 클래스입니다
{
    public ParticleSystem explosion;        //폭발 파티클
    public GameObject Player;               //플레이어
    public GameObject Boss;                 //마지막 보스
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        Boss= GameObject.Find("BossCharactor");
        explosion.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <0f)Destroy(this.gameObject);   //위치가 0아래로 내려가면 제거합니다
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rand")                                    //Rand와 충돌하면
        {
            playSound("BoxBoomSound");                              //사운드가 재생되며
            explosion.Play();                                       //폭발 이펙트가 재생됩니다
            Boss.GetComponent<MainBoss>().Drop = true;              //메인 보스 스크립트의 Drop에도 값을 줍니다
        }
        if (other.tag == "Player")                                  //Player와 충돌하면
        {
            explosion.Play();
            playSound("BoxBoomSound");
            Player.GetComponent<PlayerCtr>().CharactorFallDown();   //PlayerCtr의 CharactorFallDown함수를 실행합니다
            Boss.GetComponent<MainBoss>().Drop = true;
        }
    }
    void playSound(string snd)
    {
      GameObject.Find(snd).GetComponent<AudioSource>().Play();      //폭발음을 한번 재생합니다
    }
}
