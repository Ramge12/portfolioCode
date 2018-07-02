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

    float m_timer;  
    string timestr;    

    void Start () {
    }
	
	void Update () {
        m_timer += Time.deltaTime;  
        if (SceneManager.GetActiveScene().name == "boss")
        {
            if (TimeScore) 
            {
                if (m_timer > 10f && m_timer < 40f)     
                {

                    float timer = m_timer - 10.0f;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                    timestr = timer.ToString("00.00");  
                    timestr = timestr.Replace(".", ":");
                    TimeScore.text = timestr;
                }
                if (m_timer > 40f) m_timer = 40f;       

            }
        }
        else if (SceneManager.GetActiveScene().name == "BigBoss") { }
        else
        {
            if (TimeScore) timestr = Player.GetComponent<PlayerCtr>().StarPoint.ToString();
        }
        if (TimeScore) TimeScore.text = timestr;
        HurtUI();
    }

    void HurtUI()  
    {
        int Hp = Player.GetComponent<PlayerCtr>().PlayerHP;
        switch (Hp) 
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
