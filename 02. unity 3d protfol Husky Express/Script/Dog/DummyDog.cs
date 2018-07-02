using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDog : MonoBehaviour {

    public GameObject mainDog;          


	void Start () {
	}

	void Update () {
        transform.rotation = mainDog.transform.rotation;
    }
}
