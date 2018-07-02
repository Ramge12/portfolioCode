using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSeletButton : MonoBehaviour {

    //플레이어1,2 구분을 위한 함수

    string PLAYER_SELECT;           //플레이어 구분할 string을 기록하기 위한 string

    void Start () {
	}
	
	void Update () {
	}

    public void Select_Player1()//플레이어1 을 선택하는 함수
    {
        PLAYER_SELECT = "Player_1";
        PlayerPrefs.SetString("PLAYER_SELECT", PLAYER_SELECT);  //프리팹에 정보를 기록하고
        SceneManager.LoadScene("netWorkPlay");                  //네트워크 플레이 씬으로 넘어간다
    }

    public void Select_Player2()//플레이어2 을 선택하는 함수
    {
        PLAYER_SELECT = "Player_2";
        PlayerPrefs.SetString("PLAYER_SELECT", PLAYER_SELECT);  //프리팹에 정보를 기록하고
        SceneManager.LoadScene("netWorkPlay");                  //네트워크 플레이 씬으로 넘어간다
    }

    public void title_scene()//타이틀로 돌아가는 함수
    {
        SceneManager.LoadScene("TitleScene");                   //아무것도 고르지않을 경우 타이틀씬으로 넘어간다
    }
}
