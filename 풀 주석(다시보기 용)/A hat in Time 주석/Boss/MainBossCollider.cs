using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBossCollider : MonoBehaviour {
	// Use this for initialization
	void Start () {
    }
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) //마지막 보스가 양쪽 벽에 충돌하면 반대로 움직입니다
    {
        if (other.tag == "LeftWall")
        {
            transform.parent.GetComponent<MainBoss>().m_Left = true;
      
        }
        if (other.tag == "RightWall")
        {
            transform.parent.GetComponent<MainBoss>().m_Left = false;
        }
    }
}
