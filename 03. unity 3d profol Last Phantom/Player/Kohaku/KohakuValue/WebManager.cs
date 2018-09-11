using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WebManager {

    public float webLength;
    public Transform playerTransform;
    public Transform wepPointTransform;
    public WebController webController;

    private Vector3 position;
    private Vector3 previousPosition;

    public Vector3 MoveBob(Vector3 pos, Vector3 prevPos, float time)
    {
        webController.velocity += GetConstrainedVelocity(pos, prevPos, time);
        webController.ApplyGravity();
        webController.ApplyDamping();
        webController.CapMaxSpeed();

        pos += webController.velocity * time;

        if (Vector3.Distance(pos,position) < webLength)
        {
            pos = Vector3.Normalize(pos - position) * webLength;
            webLength = Vector3.Distance(pos, position);
            return webController.velocity * time;
        }
        previousPosition = pos;
        return webController.velocity * time;
    }

    public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPosition, float time)
    {   //현재 위치, 이전 위치, 시간
        float distanceToTether; //접점까지의 거리
        Vector3 constrainedPosition;    //강요된?
        Vector3 predictedPosition;      //예상된

        distanceToTether = Vector3.Distance(currentPos, position);   //현재 위치에서 접점까지의 ㄱ리

        if (distanceToTether > webLength)  //거리가 줄길이보다 길면
        {
            constrainedPosition = Vector3.Normalize(currentPos - position) * webLength;
            predictedPosition = (constrainedPosition - previousPosition).normalized;
            return predictedPosition;
        }
        return Vector3.zero;
    }

    public void SwitchTether(Vector3 newPosition)
    {
        playerTransform.transform.parent = null;
        wepPointTransform.position = newPosition;
        playerTransform.transform.parent = wepPointTransform;
        position = wepPointTransform.InverseTransformPoint(newPosition);
        webLength = Vector3.Distance(playerTransform.transform.localPosition, position);
    }

    public Vector3 Fall(Vector3 pos, float time)    
    {
        webController.ApplyGravity();
        webController.ApplyDamping();
        webController.CapMaxSpeed();
        
        return webController.velocity * time;
    }
}
