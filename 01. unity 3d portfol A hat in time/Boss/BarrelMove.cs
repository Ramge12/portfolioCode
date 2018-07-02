using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMove : MonoBehaviour {//통들의 전체적인 움직임 클래스입니다
    public float throwPower = 5.0f;                 
    public float Speed = 10.0f;                     
    public bool Bstart = false;                     
    Rigidbody Barrel_rigid;                         
    Transform barrels;                              
    int LeftRightRand = 0;                               


    public void Start () {
        transform.Rotate (90, 0, 0);               
        barrels = this.transform.Find("barrels "); 
        Bstart = false;                            
        LeftRightRand = Random.Range(0,2);          
    }

	void Update () {
        if (LeftRightRand == 0)transform.Rotate(0, 0, Speed * Time.deltaTime);
        else transform.Rotate(0, 0, -Speed * Time.deltaTime);   
    }

    public void ThrowBarrel(Vector3 Dir)//던지는 방향을 외부에서 받아 통을 던지는 함수입니다.
    {
        if(Bstart == false)                                     
        {
            Bstart = true;                                      
            barrels = this.transform.Find("barrels ");
            barrels.GetComponent<Barrel>().startRoll(Dir);         
        }
    }

}
