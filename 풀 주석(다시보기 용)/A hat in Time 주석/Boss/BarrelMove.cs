using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMove : MonoBehaviour {           //통들의 전체적인 움직임 클래스입니다
    public float throwPower = 5.0f;                 //던지는 힘
    public float Speed = 10.0f;                     //속도
    public bool Bstart = false;                     //나무통의 출발여부
    Rigidbody Barrel_rigid;                         //나무통의 리지드 바디
    Transform barrels;                              //나무통의 transform
    int LeftRightRand = 0;                                   //좌, 우 어느방향으로 움직일지에 대한 랜덤값

    // Use this for initialization
    public void Start () {
        transform.Rotate (90, 0, 0);                //눕혀서 던집니다
        barrels = this.transform.Find("barrels ");  //이 트랜스폼안에있는 barrels를 찾습니다
        Bstart = false;                             //아직 출발하지 않았으므로 false를 줍니다
        LeftRightRand = Random.Range(0,2);          //무작위 0,1 값을 부여합니다           
    }
	// Update is called once per frame
	void Update () {
        if (LeftRightRand == 0)transform.Rotate(0, 0, Speed * Time.deltaTime);
        else transform.Rotate(0, 0, -Speed * Time.deltaTime);   // 0일떄와 1일때가 서로 다르게 움직이도록합니다
    }
    public void ThrowBarrel(Vector3 Dir)                        //던지는 방향을 외부에서 받아 통을 던지는 함수입니다.
    {
        if(Bstart == false)                                     //아직 던져지지않은 통이라면
        {
            Bstart = true;                                      //던졌다는 불값을 주고
            barrels = this.transform.Find("barrels ");
            barrels.GetComponent<Barrel>().startRoll(Dir);      //barrel의 startRoll(구르기)함수를 실행합니다
        }
    }

}
