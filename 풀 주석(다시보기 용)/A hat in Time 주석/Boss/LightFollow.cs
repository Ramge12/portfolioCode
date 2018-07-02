using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour {

    public GameObject target;       //전등이 비출 대상
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target)
        {
            Vector3 targetDir = target.transform.position - transform.position; //자신과 대상의 방향을 파악합니다
            targetDir.x = 0;
            var targetRotation = Quaternion.LookRotation(targetDir, new Vector3(1,0,0));    
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);//(1,0,0) 기준으로 대상을 회전시킵니다
        }
	}
}
