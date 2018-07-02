using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour {

    public Transform target;    //카메라가 잡을 대상
    public float dist = 5.0f;   //카메라와 대상의 거리(간격)
    public float xSpeed = 220.0f;   //카메라x축의 회전 속도
    public float ySpeed = 100.0f;   //y축의 회전 속도
    public float x = 0.0f;      //카메라 rotation.x값을 저장할 변수
    public float y = 0.0f;      //카메라 rotation.y값을 저장할 변수
    public float yMinLimit = -20f;  //y이 최대 최소값
    public float yMaxLimit = 80f;

    private Transform cam;

    //Vector3 originalPos;
    float shakeAmount = 0.7f;
    float shake = 0;

    // Use this for initialization

    float ClampRange(float angle, float min, float max) //최대값, 최소값 제한
    {
        if (angle < -360)
            angle += 360;   //-360보다 낮아지면 360을 더해 낮아지지 않도록 한다
        if (angle > 360)
            angle -= 360;   //360을 넘어가면 -360을 빼서 넘어가지 않도록 한다
        return Mathf.Clamp(angle, min, max);
    }
    void Start()
    {
        cam = GetComponent<Transform>();    //씬 시작시 카메라 위치를 잡는다
        Vector3 angles = transform.eulerAngles; //현재 카메라 회전값을 euler오일러 값으로 받는다
        x = angles.y;   //카메라의 x,y값
        y = angles.x;
    }
    void Update()
    {
      
    }
    void LateUpdate()   //카메라 관련함수로는 LateUpdate
    {
        if (target) //대상이 있을때
        {
            if (Input.GetKey(KeyCode.Mouse0))   //마우스를 클릭하여 화면 조종가능
            {
               x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
               y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;//마우스 위치에 따라 카메라 회전
               y = ClampRange(y, yMinLimit, yMaxLimit);//Y축은 최대 최소값을 지정해 둡니다
            }
            dist -= 0.5f * Input.mouseScrollDelta.y;    //마우스 스크롤을 사용하여 간격조절
            dist = ClampRange(dist, 1, 20);//간격의 최대값과 최소값을 넘어가지 않도록 합니다

            Quaternion rotation = Quaternion.Euler(y + 30, x, 0);//유니티 관련서적에 자료있음
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
            //originalPos = transform.position;
        }
    }
}
