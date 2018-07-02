using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject Slide;        
    public Animator PlayerAni;      
    public Rigidbody player_Rigid;  
    public bool slideRide = false;  
    public bool RideRange = false;  

    Vector3 prePositionDog;         
    Vector3 prePositionPeng;        
    bool RideOn = false;                     


    void Start(){
    }

    void Update(){
    }

    public void RideSlideButton()//플레이어가 탑승버튼을 눌렀을때
    {
        if (RideRange)     
        {
            if (!slideRide)                  
            {
                if(Slide)transform.parent = Slide.transform;    //플레이어가 썰매의 자식으로 들어갑니다
                slideRide = true;                              
                if(player_Rigid)player_Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ
                    | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                                                                //탑승하면서 회전 이동하지 못하도록 Freeze를 시킵니다.
                PlayerAni.SetBool("Ride", true);                
                if (!RideOn)                                       
                {
                    RideOn = true;
                    if (Slide) transform.position = Slide.gameObject.transform.position + new Vector3(0.47f, 0.85f, 1.08f);//탑승시 플레이어의 위치가 썰매의 뒤쪽을 향하도록 보정값을 넣어줍니다
                    prePositionDog= new Vector3(0.47f, 0.85f, 1.08f);   //썰매의 보정값을 기록해둡니다
                    prePositionPeng = new Vector3(0.47f, 0.85f, 1.08f);
                }
                else                                       
                {
                    if (transform.parent.gameObject.tag == "DogSlide") 
                    {
                        if (Slide) transform.position = Slide.gameObject.transform.position + prePositionDog;   //플레이어의 위치가 썰매 뒤쪽에 고정될 수 있도록합니다
                    }
                    if (transform.parent.gameObject.tag == "PengSlide")
                    {
                        if (Slide) transform.position = Slide.gameObject.transform.position + prePositionPeng;  //플레이어의 위치가 썰매 뒤쪽에 고정될 수 있도록합니다
                    }
                }
                if (Slide) transform.eulerAngles = Slide.transform.eulerAngles + new Vector3(0, 90f, 0);    //플레이어의 회전값을 썰매의 회전값과 같이 하지만 썰매의 fbx파일이 기본적으로 90도 돌아가있어 보정값을 넣습니다.
                gameObject.GetComponent<JoystickMove>().enabled = false;        
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            else                                
            {
                PlayerAni.SetBool("Ride", false);                         
                gameObject.GetComponent<JoystickMove>().enabled = true;    
                if(player_Rigid)player_Rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                slideRide = false; 
                gameObject.GetComponent<CapsuleCollider>().enabled = true;  
                if(transform.parent.gameObject.tag== "DogSlide")
                {                                                   //썰매에서 내릴떄의 포지션을 지정합니다
                    if (Slide) prePositionDog = transform.position - Slide.gameObject.transform.position;
                }
                if(transform.parent.gameObject.tag== "PengSlide")
                {                                               
                    if (Slide) prePositionPeng = transform.position - Slide.gameObject.transform.position;
                }
                transform.parent = null;                 
            }
        }
    }
}
