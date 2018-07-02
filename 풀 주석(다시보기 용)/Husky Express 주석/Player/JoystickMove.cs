using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour {

    private Vector3 MoveVector;             //플레이어 이동벡터
    private Transform Transform;            //플레이어 트랜스폼
    public JoyStick joystick;               //조이스틱 스크립트
    public Animator M_ani;                  //플레이어 애니메이션
    public float MoveSpeed;                 //플레이어 이동속도
    public float jumpPower = 5.0f;          //플레이어 점프
    public float turnSpeed = 100.0f;        //플레이어 회전속도

    // Use this for initialization
    void Start () {
        Transform = gameObject.transform;   //플레이어 트랜스폼을 받아옵니다
        MoveVector = Vector3.zero;          //플레이어 이동방향을 초기화 시켜줍니다.
    }
	// Update is called once per frame
	void Update () {
        AnimationCheck();                   //플레이어 애니메이션을 체크합니다
    }
    void FixedUpdate()
    {
        HandleInput();                      //플레이어의 방향을 구합니다             
        Move();                             //플레이어 이동
    }
    public void HandleInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        MoveVector =  moveDir;              //조이스틱의 vertical값과 horizontal값을 이용해 방향을 구합니다
    }
    public void Move()
    {
        float v = joystick.GetVerticalValue();
        if (Mathf.Abs(v) >=0.1f && v>0)
        {
            Transform.Translate(Vector3.forward * MoveSpeed * v * Time.deltaTime);
            M_ani.SetBool("Run", true);     //달리기 애니메이션을 활성화하고 이동을 시작합니다.
        }
        else if (Mathf.Abs(v) < 0.1f)
        {
            M_ani.SetBool("Run", false);
        }
    }
    public void Jump()//플레이어가 점프하는 함수입니다.
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
