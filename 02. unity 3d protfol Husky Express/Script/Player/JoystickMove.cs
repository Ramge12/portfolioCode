using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour {

    private Vector3 MoveVector;            
    private Transform Transform;           
    public JoyStick joystick;              
    public Animator M_ani;                 
    public float MoveSpeed;                
    public float jumpPower = 5.0f;         
    public float turnSpeed = 100.0f;                

    void Start () {
        Transform = gameObject.transform;   
        MoveVector = Vector3.zero;          
    }

	void Update () {
        AnimationCheck();                   
    }

    void FixedUpdate()
    {
        HandleInput();                               
        Move();                                              
    }

    public void HandleInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        MoveVector =  moveDir;               
    }

    public void Move()
    {
        float v = joystick.GetVerticalValue();
        if (Mathf.Abs(v) >=0.1f && v>0)
        {
            Transform.Translate(Vector3.forward * MoveSpeed * v * Time.deltaTime);
            M_ani.SetBool("Run", true);    
        }
        else if (Mathf.Abs(v) < 0.1f)
        {
            M_ani.SetBool("Run", false);
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * jumpPower, ForceMode.Impulse);
        M_ani.SetBool("Jump", true);        
    }

    void AnimationCheck()//플레이어의 애니메이션을 체크하는 함수입니다.
    {
        if (M_ani.GetCurrentAnimatorStateInfo(0).IsName("JUMP") & M_ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f)
        {
            M_ani.SetBool("Jump", false);   //점프 애니메이션이 0.85만큼 진행되었을때 점프애니메이션을 끝냅니다
        }
    }
}
