using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTalk : MonoBehaviour {       //NPC들의 말풍선이 항상 카메라를 바라보도록 하는 클래스

    public GameObject mainCamera; 


    void Start(){
    }

	void Update () {
       Quaternion rotation = Quaternion.Euler(mainCamera.transform.localEulerAngles.x, mainCamera.transform.localEulerAngles.y, 0);
       transform.rotation = rotation;    
    }
}
