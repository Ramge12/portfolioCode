using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraTalk : MonoBehaviour {

    public GameObject mainCamera;
    private Transform balloonTransform;

    private void Awake()
    {
        balloonTransform = transform;
    }

    void Update () {
        Vector3 moveVector          = (mainCamera.transform.position - balloonTransform.position).normalized;
        var targetRotation          = Quaternion.LookRotation(moveVector, Vector3.up);
        balloonTransform.rotation   = Quaternion.Slerp(balloonTransform.rotation, targetRotation, 100 * Time.deltaTime);
    }
}
