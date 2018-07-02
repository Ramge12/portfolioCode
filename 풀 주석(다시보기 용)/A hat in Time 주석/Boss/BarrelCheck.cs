using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCheck : MonoBehaviour {

    public GameObject Barrel;           //나무통  
    public GameObject Player;           //플레이어
    public GameObject parents;          //부모 오브젝트
    public ParticleSystem explosion;    //폭발 효과
    float JumpPower = 5.0f;             //튀어오를때 줄 힘의값

	// Use this for initialization
	void Start () {
        explosion.Stop();
        Player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 Dir = new Vector3(0, 5, 0);                                                 //나무통이 위로 튈수있도록 방향을 줍니다
        if (other.tag == "Rand")                                                            //나무통이 땅 tag를 가진 오브젝트와 충돌할 경우
        {
            playSound("BounsSound");                                                        //튕기는 소리가 나며
            Barrel.GetComponent<Rigidbody>().AddForce(Dir * JumpPower, ForceMode.Impulse);  //위쪽으로 튀어오른다
            parents.GetComponent<BarrelMove>().enabled = true;                              //통을 움직이는 함수는 true로 둡니다
        }
        if (other.tag == "Item" )                                                           //나무통끼리 충돌할경우
        {
            explosion.Play();                                                               //폭발이펙트를 실행하며
            playSound("BoxBoomSound");                                                      //폭발하는 소리를 내며
            transform.parent.GetComponent<MeshRenderer>().enabled = false;                  
            transform.parent.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;                      //충돌에 관한 모든 오브젝트 들을 끕니다
        }
        if ( other.tag == "Player")                                                         //나무통이 플레이어와 충돌할 때
        {
            explosion.Play();
            playSound("BoxBoomSound");
            Player.GetComponent<PlayerCtr>().CharactorFallDown();                           //플레이어의 fallDown함수를 호출합니다
            transform.parent.GetComponent<MeshRenderer>().enabled = false;
            transform.parent.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    void playSound(string snd)  //사운드를 1번만 재생하는 함수
    {
        GameObject.Find(snd).GetComponent<AudioSource>().Play();
    }
}
