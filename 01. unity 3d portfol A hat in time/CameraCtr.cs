using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour {

    public Transform target;            //카메라가 잡을 대상
    public float dist = 5.0f;           //카메라와 대상의 거리(간격)
    public float xSpeed = 220.0f;       //카메라x축의 회전 속도
    public float ySpeed = 100.0f;       //y축의 회전 속도
    public float x = 0.0f;              //카메라 rotation.x값을 저장할 변수
    public float y = 0.0f;              //카메라 rotation.y값을 저장할 변수
    public float yMinLimit = -20f;      //y이 최대 최소값
    public float yMaxLimit = 80f;

    private Transform cam;

    float shakeAmount = 0.7f;
    float shake = 0;

    float ClampRange(float angle, float min, float max) //최대값, 최소값 제한
    {
        if (angle < -360)
            angle += 360;   
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        cam = GetComponent<Transform>();   
        Vector3 angles = transform.eulerAngles; //현재 카메라 회전값을 euler오일러 값으로 받는다
        x = angles.y;
        y = angles.x;
    }

    void Update(){ 
    }

    void LateUpdate()   
    {
        if (target)
        {
            if (Input.GetKey(KeyCode.Mouse0))   
            {
               x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
               y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
               y = ClampRange(y, yMinLimit, yMaxLimit); //Y축은 최대 최소값을 지정해 둡니다
            }
            dist -= 0.5f * Input.mouseScrollDelta.y;    //마우스 스크롤을 사용하여 간격조절
            dist = ClampRange(dist, 1, 20);

            Quaternion rotation = Quaternion.Euler(y + 30, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.0f, -dist) + target.position + new Vector3(0.34f, 0.5f, 0.59f);

            transform.rotation = rotation;
            transform.position = position;

            RaycastHit hitInfo;
            if (Physics.Linecast(target.position, transform.position, out hitInfo,
                1 << LayerMask.NameToLayer("Building")))
            {
                transform.position = hitInfo.point;
                //카메라 위치에서 대상을 향해 선을 쏴서 Building이라는 레이어를 가진 물체와 충돌하면
                //물체상에서 선과 충돌한 위치가 카메라의 위치가 된다.
            }
        }
    }
}
