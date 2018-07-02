using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlideControl : MonoBehaviour {

    public GameObject Dog;                                                  
    public Vector3 Dir;                    
    public Vector3 front;                  
    public float MaxRange = 5.0f;          
    public float SlideSpeed = 2.0f;        
    public bool Slide_Move = false;        
    public bool NPC_Mode;                  


    void Start () {
    }

	void Update () {
        SlideMove();                       
        if (NPC_Mode) Slide_Move = true;                            
    }

    void SlideMove()//NPC의 썰매 컨트롤함수
    {
        if (Slide_Move)
        {
            float distance;
            float distanceX = Mathf.Abs(Dog.transform.position.x - this.transform.position.x);
            float distanceZ = Mathf.Abs(Dog.transform.position.z - this.transform.position.z);
            distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));           
            Dir = Dog.transform.position - this.transform.position;
            Dir = Vector3.Normalize(Dir);                                                            
            if (distance > MaxRange)transform.Translate(Dir * SlideSpeed * Time.deltaTime);
        }
    }
}
