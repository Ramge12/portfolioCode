using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDog : MonoBehaviour {

    public GameObject mainDog;          //DummyDog들이 MainDog의 회전값과 같이 움직일 수 있도록 합니다

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        transform.rotation = mainDog.transform.rotation;
    }
}
