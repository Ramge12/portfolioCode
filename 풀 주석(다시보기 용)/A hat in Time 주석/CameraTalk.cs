using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTalk : MonoBehaviour {

    public GameObject mainCamera;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
      Quaternion rotation = Quaternion.Euler(mainCamera.GetComponent<CameraCtr>().y, mainCamera.GetComponent<CameraCtr>().x, 0);
      transform.rotation = rotation;
      //항상 카메라의 정면을 바라볼 수 있도록 카메라의 회전값에 따라 같이 회전합니다
    }
}
