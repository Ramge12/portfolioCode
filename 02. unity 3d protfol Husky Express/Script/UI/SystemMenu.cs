using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemMenu : MonoBehaviour {

    public GameObject MainMenu;       
    public GameObject MenuButton;        


	void Start () {
	}

	void Update () {
	}

    public void SetMenu()//메뉴버튼을 불러오는함수
    {
        Time.timeScale = 0;                 //게임 시간을 정지시킵니다
        MainMenu.SetActive(true);           
        MenuButton.SetActive(false);            
    }

    public void ContinuGame()//게임을 계속합니다 버튼에서 호출할 함수
    {
        Time.timeScale = 1;                 //시간을 원래대로 되돌립니다
        MainMenu.SetActive(false);        
        MenuButton.SetActive(true);               
    }

    public void goMainMenu()//메인메뉴(타이틀씬)으로 이동하는 함수
    {
        SceneManager.LoadScene("TitleScene");  
    }

    public void EndGame()//게임을 종료하는 함수
    {
        Application.Quit();
    }
}
