using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControlller : MonoBehaviour {

    [Header("Transform")]
    [SerializeField] private Transform playerTransforn;
    [SerializeField] private Transform droneBodyTransform;

    [Header("DroneValue")]
    [SerializeField] private float droneInSpeed;
    [SerializeField] private float droneOutSpeed;
    [SerializeField] private float droneOutSpeedMax;
    [SerializeField] private float droneDelayTime;
    [SerializeField] private float droneRotationSpeed = 200f;

    private Vector3 droneDirection;
    private Vector3 playerBackPostion;
    private Transform droneTransform;

    private bool droneInCheck = false;
    private bool droneOutCheck = false;
    private bool droneInPlayerRange=false;
    private bool droneOutPlayerRange=true;

    void Start () {
        droneTransform = GetComponent<Transform>();
        StartCoroutine(RandomDirection());
    }
	
	void Update () {
        playerBackPostion = playerTransforn.position - playerTransforn.forward*0.5f + playerTransforn.up;
        float distance = Vector3.Distance(droneTransform.position, playerBackPostion);
        droneDirection = (droneTransform.rotation* droneDirection);

        if (distance > 20f) droneTransform.position = playerBackPostion;

        if (distance < 0.5f && droneInPlayerRange)
        {
            droneOutSpeed = 0.3f;
            droneOutPlayerRange = false;
            droneTransform.Translate(droneDirection * droneInSpeed * Time.deltaTime);
        }
        else if (distance < 0.5f && !droneInPlayerRange)
        {
            droneOutSpeed = 0.3f;
            droneOutPlayerRange = false;
            if (!droneInCheck) StartCoroutine(DroneInPlayer());
        }
        else if (distance > 0.5f && droneOutPlayerRange)
        {
            droneInPlayerRange = false;
            if(droneOutSpeed< droneOutSpeedMax) droneOutSpeed += Time.deltaTime;
            droneDirection = (playerBackPostion - droneTransform.position).normalized;
             droneTransform.Translate(droneDirection * droneOutSpeed * Time.deltaTime);
        }
        else if (distance > 0.5f && !droneOutPlayerRange)
        {
            droneInPlayerRange = false;
            if (!droneOutCheck) StartCoroutine(DroneOutPlayer());
        }

        Vector3 targetDir = (playerTransforn.position - droneTransform.position).normalized;        
        targetDir.y = 0;                                              
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        droneBodyTransform.rotation = Quaternion.Slerp(droneBodyTransform.rotation, targetRotation, droneRotationSpeed * Time.deltaTime);  
    }

    IEnumerator RandomDirection()
    {
        float xValue = 0;
        float yValue = 0;
        float zValue = 0;
        int orderNum=0;

        while (true)
        {
            switch (orderNum)
            {
                case 0:
                    xValue = Random.Range(-10, 10);
                    orderNum = 1;
                    break;
                case 1:
                    yValue = Random.Range(-10, 10);
                    orderNum = 2;
                    break;
                case 2:
                    zValue = Random.Range(-10, 10);
                    orderNum = 0;
                    break;
            }
            droneDirection=new Vector3(xValue,yValue,zValue).normalized;
            yield return new WaitForSeconds(droneDelayTime);
        }
        yield return null;
    }

    IEnumerator DroneInPlayer()
    {
        droneInCheck = true;
        yield return new WaitForSeconds(droneDelayTime);
        droneInPlayerRange = true;
        droneInCheck = false;
        yield return null;
    }

    IEnumerator DroneOutPlayer()
    {
        droneOutCheck = true;
        yield return new WaitForSeconds(droneDelayTime);
        droneOutPlayerRange = true;
        droneOutCheck = false;
        yield return null;
    }
}
