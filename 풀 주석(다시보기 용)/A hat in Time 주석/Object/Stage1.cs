using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour             //stage에서 플레이어가 다음 스테이지로 넘어갈 수 있도록 하는 클래스
{
    public GameObject balloon;                  //메시지박스 

    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")              //플레이어와 충돌 중 일 경우
        {
            balloon.SetActive(true);            //메시지박스를 활성화 시킨다
            if (Input.GetKeyDown(KeyCode.Q))    //Q키를 누르면
            {
                int Hp = other.GetComponent<PlayerCtr>().PlayerHP;
                PlayerPrefs.SetInt("HP", Hp);
                SceneManager.LoadScene("boss"); //HP를 프리팹으로 저장하고 다음 씬으로 넘어갑니다
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") balloon.SetActive(false);    //플레이어가 벗어날경우 말풍선을 비활성화시킵니다
    }
}
