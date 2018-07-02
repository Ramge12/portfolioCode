using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public GameObject MessageBox;   

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
            MessageBox.SetActive(true); 
            if (Input.GetKey(KeyCode.Q))   
            {
                SceneManager.LoadScene("Home");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MessageBox.SetActive(false);  
        }
    }
}