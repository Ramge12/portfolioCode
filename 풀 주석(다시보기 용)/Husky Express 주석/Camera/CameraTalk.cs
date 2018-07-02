using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTalk : MonoBehaviour {       //NPC들의 말풍선이 항상 카메라를 바라보도록 하는 클래스

    public GameObject mainCamera;               //메인카메라

    // Use this for initialization
    void Start(){
    }
	// Update is called once per frame
	void Update () {
       Quaternion rotation = Quaternion.Euler(mainCamera.transform.localEulerAngles.x, mainCamera.transform.localEulerAngles.y, 0);
       transform.rotation = rotation;           //카메라의 회전값에 따라 이 오브젝트도 회전시킨다
    }
}
