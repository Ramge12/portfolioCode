using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject Message;      //메시지박스
    public GameObject ShopUI;       //상점 UI

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")Message.SetActive(true);  //플레이어와 충돌하면 메시지박스를 활성화한다
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerCloth" )                    //플레이어의 일부와 충돌한다면
        {
            if (Input.GetKeyDown(KeyCode.F))                //플레이어와 충돌한 상태에서 F키를 누를 경우 UI를 활성화 하거나 비활성화합니다
            {
                if (ShopUI.activeSelf)ShopUI.SetActive(false);
                else if (!ShopUI.activeSelf)ShopUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")Message.SetActive(false); //플레이어와 멀어지면 메시지박스를 끕니다
    }
}
