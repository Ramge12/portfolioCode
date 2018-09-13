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
    {   //현재 위치, 이전 위치, 시간
        float distanceToTether; //접점까지의 거리
        Vector3 constrainedPosition;    //강요된?
        Vector3 predictedPosition;      //예상된

        distanceToTether = Vector3.Distance(currentPos, parentPosition);   //현재 위치에서 접점까지의 ㄱ리

        if (distanceToTether > webLength)  //거리가 줄길이보다 길면
        {
            constrainedPosition = Vector3.Normalize(currentPos - parentPosition) * webLength;
            constrainedPosition += parentPosition;
            predictedPosition = (constrainedPosition - previousPosition).normalized;
            return predictedPosition;
        }
        return Vector3.zero;
    }   //현재위치의 normalize값

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
