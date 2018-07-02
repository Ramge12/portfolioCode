using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //카메라의 움직임을 플레이어의 뒤로 제한하는 클래스

    public Transform target;            //카메라가 고정할 대상
    private Transform tr;               //카메라 자체의 트랜스폼
    float dist = 3.0f;                  //카메라까지의 거리
    float height = 3.0f;                //카메라까지의 높이
    float smoothRotate = 5.0f;          //카메라가 부드럽게 회전할 수 있는 속도
    float rotX=30.0f;                   //카메라의 x축 회전값(살짝 어긋나 있기에 보정해준다)

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>(); //카메라 자체의 트랜스폼을 가져온다
    }
    // Update is called once per frame
    void Update() {
    }
    void LateUpdate()
    {
        float currTAngle =Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime); 
        //현재 카메라의 회전값을 타겟의 회전값과 같게 할때 시간에 따른 변화 값을 구합니다
        Quaternion rot = Quaternion.Euler(0, currTAngle, 0);                                        //값을 쿼터니언 값으로 변화시킵니다
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);     //타켓 포지션에서 회전값의 dist만큼 물러나고 height만큼 올라갑니다
        /*
        camera
        l     
        l
     height
        l
        ㄴㅡㅡㅡdistㅡㅡㅡㅡtarget
        */
        tr.LookAt(target);                                      //카메라가 타겟을 바라봅니다
        Vector3 rotationCam= new Vector3(-rotX, 0, 0);          //구동 결과 타겟 fbx파일 자체가 30도 정도 회전되어있어 보정값을 더해줍니다
        tr.eulerAngles =transform.eulerAngles+ rotationCam;
    }
}
