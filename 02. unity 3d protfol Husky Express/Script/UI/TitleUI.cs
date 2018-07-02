using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour {

    //타이틀 씬에서의 씬 이동 함수들 입니다. 버튼을 클릭하여 불러옵니다.


	void Start () {
	}

	void Update () {
	}
    public void single_mode()
    {
        SceneManager.LoadScene("Town");
    }
    public void AI_mode()
    {
        SceneManager.LoadScene("AI_Battler");
    }
    public void Replay_mode()
    {
        SceneManager.LoadScene("Replay");
    }
    public void Multy_Mode()
    {
        SceneManager.LoadScene("NetWorkScene");
    }
    public void Title_mode()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
