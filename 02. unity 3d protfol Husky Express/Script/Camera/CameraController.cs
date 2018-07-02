using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //카메라의 움직임을 플레이어의 뒤로 제한하는 클래스

    public Transform target;            
    private Transform tr;               
    float dist = 3.0f;                  
    float height = 3.0f;                
    float smoothRotate = 5.0f;          
    float rotX=30.0f;                   

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update() {
    }
    void LateUpdate()
    {
        float currTAngle =Mathf.LerpAngle(tr.eulerAngles.y, target.eulerAngles.y, smoothRotate * Time.deltaTime); 
        Quaternion rot = Quaternion.Euler(0, currTAngle, 0);                                    
        tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);     //타켓 포지션에서 회전값의 dist만큼 물러나고 height만큼 올라갑니다
        /*
         
        camera
        l     
        l
     height
        l
        ㄴㅡㅡㅡdistㅡㅡㅡㅡtarget

        */
        tr.LookAt(target);                                      
        Vector3 rotationCam= new Vector3(-rotX, 0, 0);          //타겟 fbx파일 자체가 30도 정도 회전되어있어 보정값을 더해줍니다
        tr.eulerAngles =transform.eulerAngles+ rotationCam;
    }
}
