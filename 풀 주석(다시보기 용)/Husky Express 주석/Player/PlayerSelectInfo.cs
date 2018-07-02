using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectInfo : MonoBehaviour {

    public GameObject game_player;      //플레이어 판별기준(PlayerNum)을 받아옵니다
    public int player_select_Num;       //플레이어 판별기준을 기록합니다
	
    // Use this for initialization
	void Start () {
        player_select_Num = 0;      //초기화
        DontDestroyOnLoad(this);    //판별기준은 다음씬까지 남아있어야 하기 때문에 파괴되지않도록 합니다
    }
	
	// Update is called once per frame
	void Update () {
        if(GameObject.Find("PlayerNum"))    //업데이트문에서 검색을 합니다
        {
            game_player = GameObject.Find("PlayerNum");// 플레이어 판별기준 다음씬까지 dontDestroy로 파괴되지 않도록 하고 다음씬에서 넘겨줍니다
            game_player.GetComponent<PlayerNum>().player_select_Num = player_select_Num;
        }
	}
    public void Select_player1()//플레이어1일경우 1을 저장합니다
    {
        player_select_Num = 1;
    }
    public void Select_player2()//플레이어2일경우 2을 저장합니다
    {
        player_select_Num = 2;
    }
}
