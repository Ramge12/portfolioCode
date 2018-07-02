using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayer : MonoBehaviour
{   //멀티플레이에서 상태 플레이어를 컨트롤 하기 위한 함수
    public float TurnSpeed=100.0f;      //회전속도
    public float Movespeed=5.0f;        //이동속도
    public float m_vertical;            //조이스틱의 vertical값
    public float m_horizontal;          //조이스틱의 horizonatl값

    // Use this for initialization
    void Start() {
    }
    // Update is called once per frame
    void Update() {
    }
    public void remote(float horizontal, float vertical)//horizontal, vertical값을 통해 이동값을 따라값니다
    {
        m_vertical = vertical;
        m_horizontal = horizontal;
        getMoveDir( horizontal, vertical);                                                  //방향을 구합니다
        if (Mathf.Abs(vertical) >= 0.1f && vertical > 0)                                    //플레이어 조이스틱과 같은 조건을 주고
        {
            transform.Translate(Vector3.forward * Movespeed * vertical * Time.deltaTime);   //이동시킵니다
        }
    }
    Vector3 getMoveDir(float horizontal, float vertical)//horizontal, vertical값을 통해 방향을 구합니다
    {
        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
        if (Mathf.Abs(horizontal) > 0.5f)   //플레이어 joystick과 같은 조건에서 할수 있도록 값을 정한다
        {
            transform.Rotate(0f, horizontal * TurnSpeed * Time.deltaTime, 0f);
        }
        return moveDir;
    }
}
