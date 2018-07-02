using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    GameObject player;         
    public GameObject Enemy;         
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Enemy.GetComponent<Enemy>().currState == ENEMY_STATE.ATTACK)   
        {
            if (other.tag == "Player")                                     
            {
                player.GetComponent<PlayerCtr>().Hurt();                         
            }
        }
    }
}
