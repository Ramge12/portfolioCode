using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour
{
    //카메라를 자요롭게 조절할 수 있는 클래스

    public Transform target;                
    public float dist = 5.0f;               
    public float xSpeed = 220.0f;           
    public float ySpeed = 100.0f;           
    public float x = 0.0f;                  
    public float y = 0.0f;                  
    public float yMinLimit = -20f;          
    public float yMaxLimit = 80f;
    public bool playerControll = false;               

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
        Vector3 angles = transform.eulerAngles; 
        x = angles.y; 
        y = angles.x;
    }

    void Update() {
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetKey(KeyCode.Mouse0) && playerControll == false)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;                      
                y = ClampRange(y, yMinLimit, yMaxLimit);            
            }
            dist -= 0.5f * Input.mouseScrollDelta.y;                
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
