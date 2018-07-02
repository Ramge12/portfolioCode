using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCheck : MonoBehaviour {

    public GameObject Barrel;           
    public GameObject Player;           
    public GameObject parents;          
    public ParticleSystem explosion;    
    float JumpPower = 5.0f;                    


	void Start () {
        explosion.Stop();
        Player = GameObject.Find("Player");
    }
	

	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 Dir = new Vector3(0, 5, 0);                                                 
        if (other.tag == "Rand")                                                            
        {
            playSound("BounsSound");                                                        
            Barrel.GetComponent<Rigidbody>().AddForce(Dir * JumpPower, ForceMode.Impulse);  
            parents.GetComponent<BarrelMove>().enabled = true;                              
        }
        if (other.tag == "Item" )//나무통끼리 충돌할경우
        {
            explosion.Play();                                                              
            playSound("BoxBoomSound");                                                     
            transform.parent.GetComponent<MeshRenderer>().enabled = false;                 
            transform.parent.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;                     
        }
        if ( other.tag == "Player")                                                        
        {
            explosion.Play();
            playSound("BoxBoomSound");
            Player.GetComponent<PlayerCtr>().CharactorFallDown();                          
            transform.parent.GetComponent<MeshRenderer>().enabled = false;
            transform.parent.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void playSound(string snd)  
    {
        GameObject.Find(snd).GetComponent<AudioSource>().Play();
    }
}
