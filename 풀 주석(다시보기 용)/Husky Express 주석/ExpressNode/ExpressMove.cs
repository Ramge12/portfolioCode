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

    public AI_Speed m_AI_speed;                 //AI스피트 상태문
    public PlayerInformation player_info;       //PlayerInformation 플레이어 정보를 가져옵니다
    public GameObject currentNode;              //현재있는 노드
    public GameObject nextNode;                 //다음으로 향할 노드
    public Vector3 CurrenPosition;              //현재 포지션
    public Vector3 ExpressDir;                  //다음으로 향할 방향
    public float AI_speed;                      //AI의 기본스피드         
    public float Slow_speed;                    //AI의 저속스피드
    public float Fast_speed;                    //AI의 빠른 스피트
    public bool Move;                           //AI가 이동할 수 있는 것에 대한 불값
    public bool final;                          //마지막노드(골지점)에 왔는지에 대한 불값
    public int TrackCount;                      //자신이 몇개의 트랙을 거쳤는지 카운트 하는 수

    void Start ()
    {
        Move = false;                           //시작할때 기본값들을 초기화 시켜줍니다
        final = false;
        TrackCount = 0;                         //
        ExpressDir = nextNode.transform.position - currentNode.transform.position;  //AI의 방향은 다음 노드와 현재노드 포지션을 이용하여 구합니다
    }
	void Update () { 
    }
    private void FixedUpdate()
    {
        if (nextNode)                           //다음 노드가 있다면
        {
            AI_Move();                          //AI가 방향을 따라 이동합니다
            AI_Rotation();                      //대상을 찾아 회전합니다
        }
    }
    void AI_Move()
    {
        ExpressDir = ExpressDir.normalized;                                     //방향을 정규화 시킨다
        transform.Translate(ExpressDir * AI_speed * Time.deltaTime);            //방향으로 이동합니다
    }
    void AI_Rotation()   //찾은 대상을 향해 이동합니다
    {
        Vector3 targetDir = CurrenPosition-nextNode.transform.position ;        //자신과 대상의 방향을 파악합니다
        targetDir.y = 0;
        targetDir = targetDir.normalized;
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);    //AI가 대상을 방향으로 회전합니다
        transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, targetRotation, 10 * Time.deltaTime);//적을 회전시킵니다
    }
    public void Set_Speed() //스피드를 셋팅합니다
    {
        AI_speed = 5.0f;
    }
}
