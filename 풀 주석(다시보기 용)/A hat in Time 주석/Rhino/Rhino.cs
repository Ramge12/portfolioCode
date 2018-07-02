using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour      //코뿔소 컨트롤 클래스
{
    public Animator RhinoAni;           //코뿔소의 애니메이션
    public GameObject Player;           //플레이어
    public GameObject Message;          //메시지박스
    public ParticleSystem footstep;     //파티클 효과

    Vector3 lookDir;                    //바라보는 방향
    float moveSpeed = 5.0f;             //움직이는 속도
    float turnSpeed = 50f;              //회전 속도 
    bool Rhino_move = false;            //움직임을 확인 하는 불값

    // Use this for initialization
    void Start()
    {
        Rhino_move = false;             //움직임을 false로 둔다
        footstep.Stop();                //파티클을 정지시켜둡니다
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerCtr>().Riding)            //플레이어가 라이딩 상태일때
        {
            MoveCharactor();                                    //캐릭터를 움직입니다
            if (Message.activeSelf) Message.SetActive(false);   //메세지 상자가 켜져있다면 끕니다
        }
    }
    void playSounding(string snd)       //사운드를 반복재생합니다
    {
        if (!GameObject.Find(snd).GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find(snd).GetComponent<AudioSource>().Play();    //play가 false가 될떄마다 다시 함수를 실행
        }
    }
    void MoveCharactor()                //캐릭터를 움직입니다
    {   //기본적으로 Player의 MoveCharactor함수와 같습니다
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;
        if (Input.GetKey(KeyCode.W))
        {
            if (!footstep.isPlaying) footstep.Play();   //걸을 때 마다 파티클을 생성합니다
            playSounding("RhinoSound");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            RhinoAni.SetBool("Run", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playSounding("RhinoSound");
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            RhinoAni.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))RhinoAni.SetBool("Run", false);
        if (Input.GetKey(KeyCode.A))transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f);
        if (Input.GetKey(KeyCode.D))transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f);
    }
}
