using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystemManager : MonoBehaviour {
    //리플레이 모드에서 플레이어를 선택해서 카메라의 타겟을 바꾸는 함수입니다

    public GameObject player1;                  //플레이어1
    public GameObject player2;                  //플레이어2
    public CameraCtr m_camera;                  //카메라 컨트롤러
    public MiniMapCamera miniCamera;            //미니맵 카메라

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
	}
    public void Player_1_Move()//
    {
        m_camera.target = player1.transform;    //카메라 
        miniCamera.target = player1;            //미니맵 카메라 타겟을 player1으로 바꿉니다
    }
    public void Player_2_Move()
    {
        m_camera.target = player2.transform;    //카메라
        miniCamera.target = player2;            //미니맵 카메라 타겟을 Player2로 바꿉니다
    }
}
