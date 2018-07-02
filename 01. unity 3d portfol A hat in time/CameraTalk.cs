using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTalk : MonoBehaviour {

    public GameObject mainCamera;

	void Start () {
    }
	
	void Update () {
      Quaternion rotation = Quaternion.Euler(mainCamera.GetComponent<CameraCtr>().y, mainCamera.GetComponent<CameraCtr>().x, 0);
      transform.rotation = rotation;
    }
}
