using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public Text TimeScore;  //시간을 표시할 UI
    public GameObject Hp4;  //HP를 표현할 이미지 UI들
    public GameObject Hp3;
    public GameObject Hp2;
    public GameObject Hp1;
    public GameObject Hp0;
    public GameObject Player;

    float m_timer;  //시간 카운트
    string timestr; //시간을 string으로 표현할 문자열

    // Use this for initialization

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;  //deltaTime을 이용해 현재 시간을 구한다
        if (SceneManager.GetActiveScene().name == "boss")   //Boss(중간보스씬)의 경우 시간제한이 있는 맵이기 때문에 시간을 체크합니다
        {
            if (TimeScore)  //Time을 나타낼 UI가 있는지 확인
            {
                if (m_timer > 10f && m_timer < 40f)
                //Boss씬은 10뒤에 게임이 진행됩니다. 시간제한은 30초이므로
                //씬 시작후 10초부터 카운트 하기 시작해 40초에 끝이납니다
                {

                    float timer = m_timer - 10.0f;    //10초뒤에 시작이므로 10을 빼줍니다
                    timestr = timer.ToString("00.00");  //소수 2번째 자리까지 표현
                    timestr = timestr.Replace(".", ":");
                    TimeScore.text = timestr;
                }
                if (m_timer > 40f) m_timer = 40f;//씬 시작후 40초가 지나면 시간제한인 30초가 지났으므로 시간을 고정합니다

            }
        }
        else if (SceneManager.GetActiveScene().name == "BigBoss") { }//마지막 보스 씬에서는 아무것도 출력하지 않습니다
        else
        {
            if (TimeScore) timestr = Player.GetComponent<PlayerCtr>().StarPoint.ToString();
            //중간보스, 마지막 보스 씬 이외에는 starPoint(별 획득 점수)를 출력합니다
        }
        if (TimeScore) TimeScore.text = timestr;
        HurtUI();
    }

    void HurtUI()   //플레이어 체력 표시 UI
    {
        int Hp = Player.GetComponent<PlayerCtr>().PlayerHP;//HP는 PlayerCtr에서 가져옵니다
        switch (Hp) //HP상태에 따른 UI변화를 보여줍니다
        {
            case 4:
                Hp4.SetActive(true);
                Hp3.SetActive(false);
                Hp2.SetActive(false);
                Hp1.SetActive(false);
                Hp0.SetActive(false);
                break;
            case 3:
                Hp4.SetActive(false);
                Hp3.SetActive(true);
                Hp2.SetActive(false);
                Hp1.SetActive(false);
                Hp0.SetActive(false);
                break;
            case 2:
                Hp4.SetActive(false);
                Hp3.SetActive(false);
                Hp2.SetActive(true);
                Hp1.SetActive(false);
                Hp0.SetActive(false);
                break;
            case 1:
                Hp4.SetActive(false);
                Hp3.SetActive(false);
                Hp2.SetActive(false);
                Hp1.SetActive(true);
                Hp0.SetActive(false);
                break;
            case 0:
                Hp4.SetActive(false);
                Hp3.SetActive(false);
                Hp2.SetActive(false);
                Hp1.SetActive(false);
                Hp0.SetActive(true);
                break;
        }

    }
}
