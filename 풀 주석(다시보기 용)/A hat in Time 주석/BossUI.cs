using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossUI : MonoBehaviour {
    /* 패턴마다 그 패턴의 이미지를 모아 패턴에 맞춰서 그림을 출력합니다 */
    public GameObject Idle;
    public GameObject Patton1;
    public GameObject Patton2;
    public GameObject Patton3;
    public GameObject Death;
    public GameObject Boss;
    public Text BossPatton;
    string BossTime;

    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        switch (Boss.GetComponent<MainBoss>().BossSt)
        {
            case MainBoss.Boss_State.Idle:
                Idle.SetActive(true);
                Patton1.SetActive(false);
                Patton2.SetActive(false);
                Patton3.SetActive(false);
                Death.SetActive(false);
                BossTime = "";
                BossPatton.text = BossTime;
                break;
            case MainBoss.Boss_State.Patton1:
                Idle.SetActive(false);
                Patton1.SetActive(true);
                Patton2.SetActive(false);
                Patton3.SetActive(false);
                Death.SetActive(false);
                BossTime = "저리 비켜!";
                BossPatton.text = BossTime;
                break;
            case MainBoss.Boss_State.Patton2:
                Idle.SetActive(false);
                Patton1.SetActive(false);
                Patton2.SetActive(true);
                Patton3.SetActive(false);
                Death.SetActive(false);
                BossTime = "이것도 피해보시지!";
                BossPatton.text = BossTime;
                break;
            case MainBoss.Boss_State.Patton3:
                Idle.SetActive(false);
                Patton1.SetActive(false);
                Patton2.SetActive(false);
                Patton3.SetActive(true);
                Death.SetActive(false);
                BossTime = "이건 힘들걸?";
                BossPatton.text = BossTime;
                break;
            case MainBoss.Boss_State.Die:
                Idle.SetActive(false);
                Patton1.SetActive(false);
                Patton2.SetActive(false);
                Patton3.SetActive(false);
                Death.SetActive(true);
                BossTime = "내가 졌어...";
                BossPatton.text = BossTime;
                break;
        }
	}
}
