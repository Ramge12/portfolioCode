using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMenu : MonoBehaviour {

    public GameObject MainMenu;             //메인 메뉴 UI
    public GameObject MenuButton;           //메인 메뉴 버튼

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void SetMenu()//메뉴버튼을 불러오는함수
    {
        Time.timeScale = 0;                 //게임 시간을 정지시킵니다
        MainMenu.SetActive(true);           //메인 메뉴 UI를 활성화시킵니다
        MenuButton.SetActive(false);        //메인 메뉴 버튼 UI를 비활성화 시킵니다.
    }
    public void ContinuGame()//게임을 계속합니다 버튼에서 호출할 함수
    {
        Time.timeScale = 1;                 //시간을 원래대로 되돌립니다
        MainMenu.SetActive(false);          //메인 메뉴 UI를 비활성화 시킵니다
        MenuButton.SetActive(true);         //메인 메뉴 버튼 UI를 활성화 시킵니다.
    }
    public void goMainMenu()//메인메뉴(타이틀씬)으로 이동하는 함수
    {
        SceneManager.LoadScene("TitleScene");   //타이틀씬으로 이동합니다
    }
    public void EndGame()//게임을 종료하는 함수
    {
        Application.Quit();
    }
}
