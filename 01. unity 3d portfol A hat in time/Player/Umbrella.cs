using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour {

    GameObject Enemy;           
    GameObject Player;          
    bool attack=false;          
    bool Player_attack = false;                                          

    void Start () {
        Enemy = GameObject.Find("Enemy");  
        Player = GameObject.Find("Player");    
    }

	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if(Player.GetComponent<PlayerCtr>().ps_State == PlayerState.Attack)
        {
            Player_attack = true;   
        }
        else
        {
            attack = false;
            Player_attack = false;  
        }
        if (other.tag == "Enemy")                                        
        {
            if (Player_attack==true && attack ==false) 
            {
                other.GetComponent<Enemy>().currState = ENEMY_STATE.Hit;   
                attack = true;                         
            }
        }
    }
}
