using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideAnimal : MonoBehaviour {

    //플레이어가 썰매를 탈때 dog인지 penguin인지 구분하여 타기위한 함수입니다.

    public DogMove dog;                 
    public DogJoyStic dogjoystick;      
    public PenguinMove peng;
    public PengJoyStic pengjoystick;    
    GameObject slide;                                                    


    void Start () {
	}

	void Update () {
        if(transform.GetComponent<PlayerControl>().slideRide)  
        {
            if (transform.parent.gameObject.tag == "DogSlide")
            {
                dog.enabled = true;
                dogjoystick.enabled = true;
                if(peng)peng.enabled = false;
                if(pengjoystick)pengjoystick.enabled = false; 
            }
            if (transform.parent.gameObject.tag == "PengSlide")
                dog.enabled = false;
                dogjoystick.enabled = false;
                if (peng) peng.enabled = true;
                if (pengjoystick) pengjoystick.enabled = true;  
            }
        }

    }
}
