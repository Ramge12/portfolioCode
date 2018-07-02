using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour {

    public PlayerControl Player;      
    public Animator MainDog;          
    public Animator DummyDog1;                                     
    public Animator DummyDog2;


    void Start () {
	}

	void Update () {
		if(Player.slideRide)gameObject.GetComponent<DogJoyStic>().enabled=true; 
        else gameObject.GetComponent<DogJoyStic>().enabled = false;             
        if (MainDog.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)     
        {
            DummyDog1.speed = Random.Range(0.5f, 1.0f);                         
            DummyDog2.speed = Random.Range(0.5f, 1.0f)-0.1f;
        }
        if (MainDog.GetBool("Run"))                                             
        {
            DummyDog1.SetBool("Run", true);
            DummyDog2.SetBool("Run", true);
        }
        else
        {
            DummyDog1.SetBool("Run", false);
            DummyDog2.SetBool("Run", false);
        }
	}
}
