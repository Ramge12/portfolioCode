using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressNode : MonoBehaviour {
    //노드를 구성하는 클래스
    public GameObject[] Left_Node   = new GameObject[3];
    public GameObject[] Right_Node   = new GameObject[3];
    public GameObject[] Top_Node    = new GameObject[3];
    public GameObject[] Bottom_Node = new GameObject[3];    //상하좌우 각각 3개의 노드를 가진다

    public GameObject NextNode;         //다음으로 향할 노드
    public GameObject Node_Center;      //노드의 중심에 하나 있는 노드
    public GameObject Node_Goal;        //골지점
    public GameObject Player;           //플레이어

    void Start (){
    }
	void Update (){
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            Player.GetComponent<PlayerInformation>().TrackCount++;  //Dog태그와 충돌할 경우 PlayerInformation에 TrackCount를 하나 추가합니다
        }
        if (other.tag == "Penguin")
        {
            if (other.GetComponent<ExpressMove>())      //Penguin태그와 충돌할 경우 ExpressMove가 있다면 
            {
                MoveController(other);                                      //MoveControoler에서 AI의 속도를 제어합니다
                other.GetComponent<ExpressMove>().TrackCount++;             //ExpressMove에 TrackCount를 하나 추가합니다
                GameObject dummyData = EnterNode(other);                    //12개의 노드 중에서 충돌한 오브젝트와 가장 가까운거리에 있는 노드(인접노드)
                other.GetComponent<ExpressMove>().currentNode = dummyData;  //12개의 노드 중 충돌한 점의 위치파악
                other.GetComponent<ExpressMove>().CurrenPosition = other.transform.position;    //Express노드에 CurrenPosition에 충돌할 떄의 포지션을 넣어줍니다
                if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.High_speed)
                {
                    //가장 가깝게 움직일 수 있는 노드를 고르는 방법
                  
                    if (NextNode)
                    {
                        if (NextNode.GetComponent<ExpressNode>().NextNode)
                        {
                            other.GetComponent<ExpressMove>().nextNode =
                            getByNodeNear(NextNode.GetComponent<ExpressNode>().NextNode, nearNode());
                            // 먼저 사각형의 4가지 가장자리에 있는 12개의 점들중에서 다음 노드와 가장 가까운 3개를 추려냅니다.
                            // ExpressMove의 nextNode에 다음 노드에 추가합니다
                        }
                        else
                        {
                            other.GetComponent<ExpressMove>().nextNode = NextNode;
                        }
                    }
                    else
                    {
                        other.GetComponent<ExpressMove>().nextNode = Node_Goal; //next노드가 없다면 nextNode에 Goal을 넣습니다
                        other.GetComponent<ExpressMove>().final = true;         //final불값에 true를 줍니다
                    }
                }
                if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.Midle_speed)
                {
                    other.GetComponent<ExpressMove>().nextNode = Node_Center;   //Middle_speed면 노드 중심으로 갑니다
                }
                if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.Low_speed)
                {
                    other.GetComponent<ExpressMove>().nextNode = Node_Center;//Low_speed면 노드 중심으로 갑니다
                }
                if (other.GetComponent<ExpressMove>().nextNode)//포지션을 사용해서 방향을 구합니다
                {
                    other.GetComponent<ExpressMove>().ExpressDir = other.GetComponent<ExpressMove>().nextNode.transform.position - other.transform.position;
                }
            }
        }
    }
    GameObject EnterNode(Collider other)  //충돌할떄 노드
    {
        float Min_Dist = 100;
        GameObject Move_curNode = null;
        for (int i = 0; i < 3; i++)
        {
            if (getDistance(Left_Node[i], other.gameObject) < Min_Dist)
            {
                Min_Dist = getDistance(Left_Node[i], other.gameObject);
                Move_curNode = Left_Node[i];
            }
            if (getDistance(Right_Node[i], other.gameObject) < Min_Dist)
            {
                Min_Dist = getDistance(Right_Node[i], other.gameObject);
                Move_curNode = Right_Node[i];
            }
            if (getDistance(Top_Node[i], other.gameObject) < Min_Dist)
            {
                Min_Dist = getDistance(Top_Node[i], other.gameObject);
                Move_curNode = Top_Node[i];
            }
            if (getDistance(Bottom_Node[i], other.gameObject) < Min_Dist)
            {
                Min_Dist = getDistance(Bottom_Node[i], other.gameObject);
                Move_curNode = Bottom_Node[i];
            }
        }
        //12개의 노드중 오브젝트가 충돌 했을 때 가장 가까운 노드를 구해 CurrNode값으로 사용합니다
        return Move_curNode;
    }
    GameObject[] nearNode() //트랙사이를 거쳐갈 노드
    {
        GameObject[] near_Node = new GameObject[3];
        //12개의 점과 다음 노드의 중점은 이미 정해져있으므로 따로 불러올 것은 없습니다.
        float Min_Dist = 100;
        for(int i=0; i<3;i++)
        {
            if (getDistance(Left_Node[i], NextNode) < Min_Dist)
            {
                Min_Dist = getDistance(Left_Node[i], NextNode);
                near_Node = Left_Node;
            }
            if (getDistance(Right_Node[i], NextNode) < Min_Dist)
            {
                Min_Dist = getDistance(Right_Node[i], NextNode);
                near_Node = Right_Node;
            }
            if (getDistance(Top_Node[i], NextNode) < Min_Dist)
            {
                Min_Dist = getDistance(Top_Node[i], NextNode);
                near_Node = Top_Node;
            }
            if (getDistance(Bottom_Node[i], NextNode) < Min_Dist)
            {
                Min_Dist = getDistance(Bottom_Node[i], NextNode);
                near_Node = Bottom_Node;
            }
        }
        //12개의 점들중 가장 가까운 거리를 가지는 점의 가장자리에 있는 3개의 점을 반환합니다
        return near_Node;
    }
    GameObject getByNodeNear(GameObject fromNode, GameObject[] Cross_Node)//가장 가까운 거리의 노드
    {
        //추려진 3개의 노드와 그 다음노드(next의 next)의 위치를 비교합니다
        float Min_Dist = 100;
        GameObject ByNode=null;
        for (int i=0; i< Cross_Node.Length;i++)
        {
            if(getDistance(fromNode, Cross_Node[i])<Min_Dist)
            {
                Min_Dist = getDistance(fromNode, Cross_Node[i]);
                ByNode = Cross_Node[i];
            }
        }
        //3개의 점들중 가장가까운 점을 반환합니다.
        return ByNode;
    }
    float getDistance(GameObject obj1, GameObject obj2)//오브젝트 2개의 거리를 반환하는 함수
    {
        float distance, distanceX, distanceZ;
        distanceX = obj1.transform.position.x - obj2.transform.position.x;
        distanceZ = obj1.transform.position.z - obj2.transform.position.z;
        distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));
        return distance;
    }
    void MoveController(Collider other)
    {
        //속도 제어 구간===========================================================================================================
        if (other.GetComponent<ExpressMove>())
        {
            if (other.GetComponent<ExpressMove>().Move)
            {
                if (Player.GetComponent<PlayerInformation>().TrackCount > other.GetComponent<ExpressMove>().TrackCount)//플레이어가 자기보다 빠르다 1등. 플레이어 2. AI
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.High_speed;                                 //속도는 Hight_speed로 합니다
                    if (Player.GetComponent<PlayerInformation>().TrackCount - other.GetComponent<ExpressMove>().TrackCount > 3)//차이가 많이벌어짐
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed + 1.5f;//속도를 살짝 더해준다
                    }
                    else if (Player.GetComponent<PlayerInformation>().TrackCount - other.GetComponent<ExpressMove>().TrackCount > 2)//차이가 적게벌어짐
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed;
                    }
                    else                                                                                                 //차이가 비슷비슷하다
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed - 1.5f;//속도를 살짝 빼준다
                    }
                }
                if (Player.GetComponent<PlayerInformation>().TrackCount < other.GetComponent<ExpressMove>().TrackCount)//AI가 플레이어를 이길때
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.Low_speed;
                    if (other.GetComponent<ExpressMove>().TrackCount - Player.GetComponent<PlayerInformation>().TrackCount > 1)//차이가 많이벌어짐
                    {
                        other.GetComponent<ExpressMove>().AI_speed = 2f;
                    }
                    else //차이가 많이벌어짐
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Slow_speed - 0.5f;
                    }
                }
                if (Player.GetComponent<PlayerInformation>().TrackCount == other.GetComponent<ExpressMove>().TrackCount)//AI가 플레이어를 이길때
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.Midle_speed;
                    other.GetComponent<ExpressMove>().AI_speed = 4.0f;
                }
            }
        }
    }
}
