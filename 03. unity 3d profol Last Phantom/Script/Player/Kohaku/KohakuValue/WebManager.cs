using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WebManager {

    public float webLength;
    public Transform playerTransform;
    public Transform wepPointTransform;
    public WebController webController;

    private Vector3 parentPosition;
    private Vector3 previousPosition;

    public Vector3 MoveBob(Vector3 curPos, Vector3 prevPos, float time)
    {
        webController.velocity += GetConstrainedVelocity(curPos, prevPos, time);
        webController.ApplyGravity();
        webController.ApplyDamping();
        webController.CapMaxSpeed();

        curPos += webController.velocity * time;
        previousPosition = curPos;
        if (Vector3.Distance(curPos,parentPosition) < webLength)
        {
            webLength = Vector3.Distance(curPos, parentPosition);
        }
        return webController.velocity * time;
    }

    public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPosition, float time)
    {   
        float distanceToTether; 
        Vector3 constrainedPosition;    //플레이어 부터 거미줄을 쏜 지점까지의 단위 벡터에 거미줄 값을 곱한 벡터
        Vector3 predictedPosition;      //이동값을 예측한 위치로부터의 방향을 구했을 때의 방향 벡터입니다.

        distanceToTether = Vector3.Distance(currentPos, parentPosition);   

        if (distanceToTether > webLength) 
        {
            constrainedPosition = Vector3.Normalize(currentPos - parentPosition) * webLength;
            constrainedPosition += parentPosition;
            predictedPosition = (constrainedPosition - previousPosition).normalized;
            return predictedPosition;
        }
        return Vector3.zero;
    }  

    public void SwitchTether(Vector3 newPosition)
    {
        wepPointTransform.position = newPosition;
        parentPosition = wepPointTransform.position;
        webLength = Vector3.Distance(playerTransform.position, parentPosition);
    }

    public Vector3 Fall(Vector3 pos, float time)    
    {
        webController.ApplyGravity();
        webController.ApplyDamping();
        webController.CapMaxSpeed();
        
        return webController.velocity * time;
    }
}
