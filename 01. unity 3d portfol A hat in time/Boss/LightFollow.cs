using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour {

    public GameObject target;       

	void Start () {
	}

	void Update () {
		if(target)
        {
            Vector3 targetDir = target.transform.position - transform.position; 
            targetDir.x = 0;
            var targetRotation = Quaternion.LookRotation(targetDir, new Vector3(1,0,0));    
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
        }
	}
}
