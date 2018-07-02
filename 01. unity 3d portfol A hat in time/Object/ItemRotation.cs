using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour {

    public Vector3 HeadOn;                       

    GameObject player;                         
    bool onPlayer=false;                       
    float turnSpeed = 10.0f;                   

    void Start () {
        player = GameObject.Find("Player");    
    }

	void Update () {
        transform.Rotate(0, turnSpeed * Time.deltaTime,0);                     
        if(onPlayer)transform.position = player.transform.position + HeadOn;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          player.GetComponent<PlayerCtr>().ItemGet(this.gameObject);          
          onPlayer = true;
        }
    }
}
