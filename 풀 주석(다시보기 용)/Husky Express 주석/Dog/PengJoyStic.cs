using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PengJoyStic : MonoBehaviour {
    
    private Vector3 MoveVector;             //펭귄 이동벡터
    private Transform m_Transform;          //펭귄 트랜스폼
    public JoyStick joystick;               //조이스틱 스크립트
    public Animation M_ani;                 //펭귄 애니메이션
    public AnimationClip M_idle;            //idle 애니메이션 클립
    public AnimationClip M_run;             //run  애니메이션 클립
    public AnimationClip M_walk;            //walk 애니메이션 클립
    public float turnSpeed = 100.0f;        //펭귄 회전 속도  (유니티 쪽에서 수정가능 하도록 public으로 선언합니다)
    public float MoveSpeed;                 //펭귄 이동 속도
    
    // Use this for initialization
    void Start()
    {
        M_ani.clip = M_idle;                //애니메이션 클립을 idle로 둡니다
        M_ani.Play();                       //애니메이션을 재생합니다
        m_Transform = gameObject.transform; //트랜스폼을 불러옵니다
        MoveVector = Vector3.zero;          //MoveVector을 초기화 시킵니다.
    }
    // Update is called once per frame
    void Update(){
    }
    void FixedUpdate()
    {
        HandleInput();
        Move();
    }
    public void HandleInput()//조이스틱 값을 이용하여 방향을 결정합니다
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        MoveVector = moveDir;
    }
    void Move()//조이스틱의 값을 이용하여 오브젝트를 이동시킵니다
    {
        float v = joystick.GetVerticalValue();
        if (Mathf.Abs(v) >= 0.1f && v > 0)      //vectical값이 0.1보다 클때만 움직입니다
        {
            m_Transform.Translate(Vector3.forward * -MoveSpeed * v * Time.deltaTime);
            M_ani.clip = M_walk;
            M_ani.Play();
        }
        else if (Mathf.Abs(v) < 0.1f)
        {
            M_ani.clip = M_idle;
            M_ani.Play();
        }
    }
}
