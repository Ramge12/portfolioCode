using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {   //중간보스가 사용한 나무통에 대한 클래스입니다
    public float rollSpeed=10.0f;   //나무통의 스피드
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, rollSpeed * Time.deltaTime, 0); 
    }
    public void startRoll(Vector3 dir)  //나무통이 처음 던져지는 순간입니다.
    {
        transform.GetComponent<Rigidbody>().useGravity = true;  //중력을 활성화 시키고
        transform.GetComponent<Rigidbody>().AddForce(dir * 1.0f, ForceMode.Impulse);// 주어진 방향으로 힘을줍니다
    }
}
