using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangPlayer : MonoBehaviour
{
    GameObject player;      
    bool hang = false;          

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hang")        
        {
            player.GetComponent<PlayerCtr>().HangPlayer(transform.position.y); 
            hang = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hang")
        {
            player.GetComponent<PlayerCtr>().HangOutPlayer();   
            hang = false;
        }
    }
}
