﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runaway : MonoBehaviour    //코뿔소가 적 캐릭터와 충돌시 적캐릭터가 도망가도록하는 함수
{

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            other.GetComponent<Enemy>().RunAway();  
        }
    }
}