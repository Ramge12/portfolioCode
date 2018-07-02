using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCheck : MonoBehaviour {    //플레이어의 다리부분에서 다리에 닿는게 뭔지 판단하는 클래스

    public GameObject Boss;             
    public GameObject Player;           
    public bool onRand = false;         
    bool DJ_Jump = false;               
    bool PlayerAttack = false;            

	void Start () {
	}

	void Update () {
        if (Player)
        {
            if (Player.GetComponent<PlayerCtr>().ps_State == PlayerState.IDLE)
            {
                PlayerAttack = false;      
            }
        }

    }
    public void Dj_Jump_Check()
    {
        DJ_Jump = true;              
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rand")onRand = true;          
        if (other.tag == "HitPoint")DJ_Jump = true;           
        if (other.tag == "Boss")
        {
            if (Player.GetComponent<PlayerCtr>().ps_State == PlayerState.Jump_Attack && PlayerAttack==false)
            {
                PlayerAttack = true;                    
                Boss.GetComponent<MainBoss>().Hurt();   
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rand")onRand = false;         
        if (other.tag == "HitPoint") DJ_Jump = false;
    }
}
