using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject Message;    
    public GameObject ShopUI;          


    void Start(){
    }

    void Update(){
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")Message.SetActive(true); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerCloth" )                    
        {
            if (Input.GetKeyDown(KeyCode.F))                 
            {
                if (ShopUI.activeSelf)ShopUI.SetActive(false);
                else if (!ShopUI.activeSelf)ShopUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")Message.SetActive(false);
    }
}
