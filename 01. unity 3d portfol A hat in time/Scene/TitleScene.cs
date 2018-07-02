using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScene : MonoBehaviour {


	void Start () {
        PlayerPrefs.SetInt("HP", 4);    //플레이어 HP를 타이틀에서 미리 셋팅
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
