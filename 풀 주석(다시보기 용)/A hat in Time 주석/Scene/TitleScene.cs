using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("HP", 4);    //플레이어 HP를 타이틀에서 미리 셋팅
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");//스페이스바로 Main으로 이동
        }
    }
}
