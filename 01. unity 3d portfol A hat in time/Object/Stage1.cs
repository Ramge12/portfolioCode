using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour             
{
    public GameObject balloon;                                                                          

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")             
        {
            balloon.SetActive(true);           
            if (Input.GetKeyDown(KeyCode.Q))                   
            {
                int Hp = other.GetComponent<PlayerCtr>().PlayerHP;
                PlayerPrefs.SetInt("HP", Hp);
                SceneManager.LoadScene("boss"); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") balloon.SetActive(false);   
    }
}
