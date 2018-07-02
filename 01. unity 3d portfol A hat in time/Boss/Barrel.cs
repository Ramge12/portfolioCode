using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {   //중간보스가 사용한 나무통에 대한 클래스입니다

    public float rollSpeed=10.0f; 


	void Start () {
	}

	void Update () {
        transform.Rotate(0, rollSpeed * Time.deltaTime, 0); 
    }

    public void startRoll(Vector3 dir)  
    {
        transform.GetComponent<Rigidbody>().useGravity = true;  
        transform.GetComponent<Rigidbody>().AddForce(dir * 1.0f, ForceMode.Impulse);
    }
}
