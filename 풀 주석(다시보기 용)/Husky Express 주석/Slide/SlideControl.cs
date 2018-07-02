using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlideControl : MonoBehaviour {

    public GameObject Dog;                  //동물 오브젝트
    public Vector3 Dir;                     //방향
    public Vector3 front;                   //정면
    public float MaxRange = 5.0f;           //최대범위  
    public float SlideSpeed = 2.0f;         //썰매속도     
    public bool Slide_Move = false;         //NPC의 썰매모드
    public bool NPC_Mode;                   //NPC가 사용할지에 대한 불값

    // Use this for initialization
    void Start () {
    }
	// Update is called once per frame
	void Update () {
        SlideMove();                        //NPC가 썰매에 타는 경우
        if (NPC_Mode) Slide_Move = true;    //
    }
    void SlideMove()//NPC의 썰매 컨트롤함수
    {
        if (Slide_Move)
        {
            float distance;
            float distanceX = Mathf.Abs(Dog.transform.position.x - this.transform.position.x);
            float distanceZ = Mathf.Abs(Dog.transform.position.z - this.transform.position.z);
            distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));           //개와 썰매 사이의 거리
            Dir = Dog.transform.position - this.transform.position;
            Dir = Vector3.Normalize(Dir);                                                       //방향을 구해서 이동시킵니다
            if (distance > MaxRange)transform.Translate(Dir * SlideSpeed * Time.deltaTime);
        }
    }
}
