﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTalk : MonoBehaviour {
    //카메라에서 RaycastHit을 이용하여 NPC를 클릭하면 NPC와 대화할수있도록 하는 클래스입니다.

    public Camera MainCamera;       //Ray를 쏠 메인카메라
    public float RayRange;          //길이      
    Ray ray;

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObj;
            if(Physics.Raycast(ray,out hitObj, RayRange))
            {
                //클릭시 카메라에서 ray를 쏴서 hit한 오브젝트의 tag가 NPC일 경우
                if(hitObj.transform.tag.Equals("NPC"))
                {
                    //대화창을 활성화 시킵니다.
                    if(hitObj.transform.GetComponent<NPC1>())
                        hitObj.transform.GetComponent<NPC1>().TalkNPC();
                    if (hitObj.transform.GetComponent<NPC2>())
                        hitObj.transform.GetComponent<NPC2>().UIclick();
                    if (hitObj.transform.GetComponent<LasttNPC>())
                        hitObj.transform.GetComponent<LasttNPC>().Warning_Message();
                }
            }
        }
	}
}
