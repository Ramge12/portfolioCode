using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject Slide;        //플레이어 썰매
    public Animator PlayerAni;      //플레이어 애니메이션
    public Rigidbody player_Rigid;  //플레이어 리지드 바디
    public bool slideRide = false;  //플레이어가 썰매에 탑승했는지
    public bool RideRange = false;  //플레이어가 탑승 범위안에 들어왔는지 체크하는 불값

    Vector3 prePositionDog;         //
    Vector3 prePositionPeng;        //플레이어가 썰매에 탑승했을 떄의 위치를 조정하기 위한 보정값
    bool RideOn = false;            //플레이어가 썰매에 탑승했는지에 대한 불값(한번만 실행)

    // Use this for initialization
    void Start(){
    }
    // Update is called once per frame
    void Update(){
    }
    public void RideSlideButton()//플레이어가 탑승버튼을 눌렀을때
    {
        if (RideRange)      //탑승 범위내에 플레이어가 있다면
        {
            if (!slideRide) //탑승하지않았다면
            {
                if(Slide)transform.parent = Slide.transform;    //플레이어가 썰매의 자식으로 들어갑니다
                slideRide = true;                               //탑승했으므로 true값을 줍니다
                if(player_Rigid)player_Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ
                    | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                                                                //탑승하면서 회전 이동하지 못하도록 Freeze를 시킵니다.
                PlayerAni.SetBool("Ride", true);                //탑승애니메이션을 실행시킵니다
                if (!RideOn)                                    //탑승시 한번만 실행되도록합니다
                {
                    RideOn = true;
                    if (Slide) transform.position = Slide.gameObject.transform.position + new Vector3(0.47f, 0.85f, 1.08f);
                                                                //탑승시 플레이어의 위치가 썰매의 뒤쪽을 향하도록 보정값을 넣어줍니다
                    prePositionDog= new Vector3(0.47f, 0.85f, 1.08f);   //썰매의 보정값을 기록해둡니다
                    prePositionPeng = new Vector3(0.47f, 0.85f, 1.08f);
                }
                else                                            //처음에 한번만 위와 같이 하고 이외에는 아래와 같이 한다
                {
                    if (transform.parent.gameObject.tag == "DogSlide")  //Dog의 경우
                    {
                        if (Slide) transform.position = Slide.gameObject.transform.position + prePositionDog;   //플레이어의 위치가 썰매 뒤쪽에 고정될 수 있도록합니다
                    }
                    if (transform.parent.gameObject.tag == "PengSlide") //Penguin의 경우
                    {
                        if (Slide) transform.position = Slide.gameObject.transform.position + prePositionPeng;  //플레이어의 위치가 썰매 뒤쪽에 고정될 수 있도록합니다
                    }
                }
                if (Slide) transform.eulerAngles = Slide.transform.eulerAngles + new Vector3(0, 90f, 0);    //플레이어의 회전값을 썰매의 회전값과 같이 하지만 썰매의 fbx파일이 기본적으로 90도 돌아가있어 보정값을 넣습니다.
                gameObject.GetComponent<JoystickMove>().enabled = false;        
                gameObject.GetComponent<CapsuleCollider>().enabled = false;//썰매쪽 조이스틱과 move가 활성화 되므로 플레이어쪽은 비활성화 시킵니다.
            }
            else                                            //썰매에서 내리는 경우
            {
                PlayerAni.SetBool("Ride", false);                           //애니메이션을 정지합니다
                gameObject.GetComponent<JoystickMove>().enabled = true;     //플레이어 조이싁을 활성화합니다
                if(player_Rigid)player_Rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                //플레이어가 원래대로 할 수 있도록 Freeze값을 풀어줍니다
                slideRide = false;  //썰매에서 내렸으므로 false값을 줍니다
                gameObject.GetComponent<CapsuleCollider>().enabled = true;  //플레이어 캡슐 콜라이더를 활성화합니다
                if(transform.parent.gameObject.tag== "DogSlide")
                {                                                   //썰매에서 내릴떄의 포지션을 지정합니다
                    if (Slide) prePositionDog = transform.position - Slide.gameObject.transform.position;
                }
                if(transform.parent.gameObject.tag== "PengSlide")
                {                                                  //썰매에서 내릴떄의 포지션을 지정합니다
                    if (Slide) prePositionPeng = transform.position - Slide.gameObject.transform.position;
                }
                transform.parent = null;                        //부모를 NULL로 두어 썰매의 자식에서 나옵니다
            }
        }
    }
}
