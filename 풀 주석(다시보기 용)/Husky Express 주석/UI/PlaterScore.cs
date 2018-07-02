using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaterScore : MonoBehaviour {

    public Text timer_text;                 //Timer를 표시할 textUI
    public Text gold_text;                  //Gold를 표시할 goldUI
    public GameObject ClearScore;           //게임 스코어 UI
    public PlayerScore ps_Score;            //플레이어 sore정보를 가져올 클래스

    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
    }
    public void goal()//플레이어가 골인한경우
    {
        ClearScore.SetActive(true);         //UI를 활성화한다
        timer_text.text = "걸린시간:"+ps_Score.timer.ToString();       
        gold_text.text = "완료금액:"+ps_Score.player_gold.ToString();    //text를 출력한다
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")goal();      //플레이어가 골인지점에 충돌하면 goal함수를 실행한다
    }
}
