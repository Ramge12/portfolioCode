using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public GameObject MessageBox;   //문에 표시할 메시지박스
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
        if (other.tag == "Player")  //플레이어가 문에 충돌하면
        {
            MessageBox.SetActive(true); //메시지박스를 활성화한다
            if (Input.GetKey(KeyCode.Q))    //충돌상태에서 Q를 누르면
            {
                SceneManager.LoadScene("Home"); //Home씬으로 이동합니다
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MessageBox.SetActive(false);    //플레이어가 충돌에서 벗어나면 메시지창 비활성화
        }
    }
}