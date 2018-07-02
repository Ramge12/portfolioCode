using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaterScore : MonoBehaviour {

    public Text timer_text;                           
    public Text gold_text;          
    public GameObject ClearScore;   
    public PlayerScore ps_Score;    

  
    void Start () {
	}

	void Update () {
    }

    public void goal()//플레이어가 골인한경우
    {
        ClearScore.SetActive(true);        
        timer_text.text = "걸린시간:"+ps_Score.timer.ToString();       
        gold_text.text = "완료금액:"+ps_Score.player_gold.ToString();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")goal();      //플레이어가 골인지점에 충돌하면 goal함수를 실행한다
    }
}
