using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideRange : MonoBehaviour {

    //썰매의 자식으로 있는 범위에서 사용하는 클래스 입니다

    public GameObject Player;                  
    public GameObject Slide;                      
  

	void Start () {
        Slide = transform.parent.gameObject;    //썰매는 자식의 부모 오브젝트입니다.
    }

	void Update () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")              
        {
            Player.GetComponent<PlayerControl>().RideRange = true;                             
            Player.GetComponent<PlayerControl>().Slide = transform.parent.gameObject;          
            transform.parent.parent.gameObject.GetComponent<SlideControl>().Slide_Move = true; 
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")              
        {
            Player.GetComponent<PlayerControl>().RideRange = false;                            
            Player.GetComponent<PlayerControl>().Slide = null;                                 
            transform.parent.parent.gameObject.GetComponent<SlideControl>().Slide_Move = false;         
        }
    }
}
