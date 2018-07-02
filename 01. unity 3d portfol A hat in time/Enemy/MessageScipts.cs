using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScipts : MonoBehaviour {

    GameObject Player;              
    public GameObject Message;         

	void Start () {
        Player = GameObject.Find("Player");
    }

	void Update () {
        Vector3 targetDir = Player.transform.position - transform.position;            
        targetDir.y = 0;                                                               
        float dist = Vector3.Distance(Player.transform.position, transform.position);  
        if (dist < 15f)Message.SetActive(true);                                        
        else Message.SetActive(false);
    }
}
