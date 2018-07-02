using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_Speed//AI상태문
{
    Low_speed,
    Midle_speed,
    High_speed
}

public class ExpressMove : MonoBehaviour {

    public AI_Speed m_AI_speed;                 
    public PlayerInformation player_info;       //PlayerInformation 플레이어 정보를 가져옵니다
    public GameObject currentNode;              //현재있는 노드
    public GameObject nextNode;                 //다음으로 향할 노드
    public Vector3 CurrenPosition;              //현재 포지션
    public Vector3 ExpressDir;                 
    public float AI_speed;                     
    public float Slow_speed;                   
    public float Fast_speed;                   
    public bool Move;                               
    public bool final;                          //마지막노드(골지점)에 왔는지에 대한 불값
    public int TrackCount;                      //자신이 몇개의 트랙을 거쳤는지

    void Start ()
    {
        Move = false;                          
        final = false;
        TrackCount = 0;                       
        ExpressDir = nextNode.transform.position - currentNode.transform.position;  
    }

	void Update () { 
    }

    private void FixedUpdate()
    {
        if (nextNode)                          
        {
            AI_Move();                         
            AI_Rotation();                                 
        }
    }

    void AI_Move()
    {
        ExpressDir = ExpressDir.normalized;                                     
        transform.Translate(ExpressDir * AI_speed * Time.deltaTime);               
    }

    void AI_Rotation()   
    {
        Vector3 targetDir = CurrenPosition-nextNode.transform.position ;       
        targetDir.y = 0;
        targetDir = targetDir.normalized;
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);   
        transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, targetRotation, 10 * Time.deltaTime);//적을 회전시킵니다
    }

    public void Set_Speed() 
    {
        AI_speed = 5.0f;
    }
}
