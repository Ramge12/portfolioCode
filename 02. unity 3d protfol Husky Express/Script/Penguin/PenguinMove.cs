using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinMove : MonoBehaviour {

    //플레이어가 펭귄을 타고다닐경우

    public PlayerControl Player;  


    void Start(){
    }

    void Update()
    {
        if (Player.slideRide)gameObject.GetComponent<PengJoyStic>().enabled = true;
        else gameObject.GetComponent<PengJoyStic>().enabled = false;
    }
}
