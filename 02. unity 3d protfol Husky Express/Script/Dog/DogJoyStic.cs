using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogJoyStic : MonoBehaviour {

    private Vector3 MoveVector;             
    private Transform m_Transform;          
    public FileInput File_input;            
    public JoyStick joystick;               
    public Animator M_ani;                  
    public float MoveSpeed;                 
    public float turnSpeed = 100.0f;                                                                


    void Start()
    {
        m_Transform = gameObject.transform;
        MoveVector = Vector3.zero;         
    }

    void Update() {
    }

    void FixedUpdate()
    {
        HandleInput();                     
        Move();                                                   
    }

    void HandleInput()//조이스틱의 값을 통히 방향을 구합니다
    {
        float h = joystick.GetHorizontalValue();            
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;                        
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);  //조이스틱중 horizontal의 값이 0.5 이상이 될때에만 horizontal 값으로 회전한다
        MoveVector = moveDir;                         
    }

    void Move()//조이스틱을 통해 구한 방향값으로 이동하는 함수
    {
        float v = joystick.GetVerticalValue();              
        if (Mathf.Abs(v) >= 0.1f && v > 0)                  //vertical값이 0.1이상일 때만 움직입니다
        {
            m_Transform.Translate(Vector3.forward * MoveSpeed * v * Time.deltaTime);
            if(M_ani)M_ani.SetBool("Run", true);
        }
        else if (Mathf.Abs(v) < 0.1f)                       //0.1보다 작을 경우 움직이지 않습니다.
        {
            if (M_ani) M_ani.SetBool("Run", false);
        }
    }
}
