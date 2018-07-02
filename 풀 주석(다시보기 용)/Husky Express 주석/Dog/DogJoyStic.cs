using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogJoyStic : MonoBehaviour {

    private Vector3 MoveVector;             //Dog의 이동벡터
    private Transform m_Transform;          //Dog의 트랜스폼
    public FileInput File_input;            //파일 입출력을 위한 매니저
    public JoyStick joystick;               //조이스틱 스크립트
    public Animator M_ani;                  //Dog의 애니메이션
    public float MoveSpeed;                 //Dog의 이동속도(유니티 쪽에서 수정가능 하도록 public으로 선언합니다)
    public float turnSpeed = 100.0f;        //Dog의 회전속도

    // Use this for initialization
    void Start()
    {
        m_Transform = gameObject.transform; //트랜스폼은 게임오브젝트의 트랜스폼입니다
        MoveVector = Vector3.zero;          //MoveVector초기화
    }
    // Update is called once per frame
    void Update() {
    }
    void FixedUpdate()
    {
        HandleInput();                      //조이스틱의 값에 따라 방향을 결정합니다
        Move();                             //방향에 따라 이동합니다
    }
    void HandleInput()//조이스틱의 값을 통히 방향을 구합니다
    {
        float h = joystick.GetHorizontalValue();            //키보드가 아닌 Joystick의 vertical, horizontal값을 가져온다
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;  //조이스틱 입력값을 통해 이동방향을 구한다
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);  //조이스틱중 horizontal의 값이 0.5 이상이 될때에만 horizontal 값으로 회전한다
        MoveVector = moveDir;                               //움직일 방향을 정한다
    }
    void Move()//조이스틱을 통해 구한 방향값으로 이동하는 함수
    {
        float v = joystick.GetVerticalValue();              //조이스틱에서 vertical값을 가져옵닏.
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
