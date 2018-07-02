using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystemManager : MonoBehaviour {

    //리플레이 모드에서 플레이어를 선택해서 카메라의 타겟을 바꾸는 함수입니다

    public GameObject player1;                               
    public GameObject player2;             
    public CameraCtr m_camera;             
    public MiniMapCamera miniCamera;       


	void Start () {
	}

	void Update () {
	}

    public void Player_1_Move()
    {
        m_camera.target = player1.transform;  
        miniCamera.target = player1;          
    }

    public void Player_2_Move()
    {
        m_camera.target = player2.transform;  
        miniCamera.target = player2;               
    }
}
