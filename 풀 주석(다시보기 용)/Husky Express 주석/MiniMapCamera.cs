using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {
   
    public GameObject target;           //미니맵에서 중심적으로 잡을 타겟을 설정합니다

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        //Y축을 제외한 X,Z축을 타겟에게 고정합니다
    }
}
