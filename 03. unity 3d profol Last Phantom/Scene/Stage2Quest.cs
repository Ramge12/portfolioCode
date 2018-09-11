using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Quest : MonoBehaviour
{
    [Header("Stage2 Scripts")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private BikeSystem bikeSystem;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Stage2 UI")]
    [SerializeField] private Text questUIText;
    [SerializeField] private Text questTimerUI;
    [SerializeField] private GameObject Enemys;

    [SerializeField] private float limitTIme;
    private float timer = 0;
    private bool stage2Game = false;

    void Start()
    {
        questUIText.text = limitTIme.ToString("N0")+"초 안에 깃발지점까지 \n 도착해야합니다";
    }
    
    void Update()
    {
        if (timer < limitTIme && !stage2Game && bikeSystem.ride)
        {
            timer += Time.deltaTime;
        }
        else if(timer>limitTIme)
        {
            stage2Game = true;
            FailStage();
            timer = limitTIme;
        }
        if(!stage2Game)questTimerUI.text = (limitTIme - timer).ToString("N0") + "초 남았습니다";
    }

    void FailStage()
    {
        questUIText.text = "이번라운드 도전에 실패했습니다\n 다시도전을 시작합니다";
        Enemys.SetActive(false);
        GameManager.gManagerCall().ThisSceneReLoad();
    }

    public void NextStage()
    {
        stage2Game = true ;
        Enemys.SetActive(false);
        questUIText.text = "도전 성공 \n 다음라운드로 넘어값니다";
        playerInventory.Save();
        playerStats.Save();
        GameManager.gManagerCall().NextSceneLoad();
    }
}
