using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressNode : MonoBehaviour {

    //노드를 구성하는 클래스

    public GameObject[] Left_Node   = new GameObject[3];
    public GameObject[] Right_Node   = new GameObject[3];
    public GameObject[] Top_Node    = new GameObject[3];
    public GameObject[] Bottom_Node = new GameObject[3];    //상하좌우 각각 3개의 노드를 가진다

    public GameObject NextNode;        
    public GameObject Node_Center;     
    public GameObject Node_Goal;       
    public GameObject Player;                                

    void Start (){
    }

	void Update (){
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            Player.GetComponent<PlayerInformation>().TrackCount++; 
        }

        if (other.tag == "Penguin")
        {
            if (other.GetComponent<ExpressMove>())      
            {
                MoveController(other);                                     
                other.GetComponent<ExpressMove>().TrackCount++;                   
                GameObject dummyData = EnterNode(other);                                        //12개의 노드 중에서 충돌한 오브젝트와 가장 가까운거리에 있는 노드(인접노드)
                other.GetComponent<ExpressMove>().currentNode = dummyData;                      //12개의 노드 중 충돌한 점의 위치파악
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
                if (Player.GetComponent<PlayerInformation>().TrackCount > other.GetComponent<ExpressMove>().TrackCount)
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.High_speed;                               
                    if (Player.GetComponent<PlayerInformation>().TrackCount - other.GetComponent<ExpressMove>().TrackCount > 3)
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed + 1.5f;
                    }
                    else if (Player.GetComponent<PlayerInformation>().TrackCount - other.GetComponent<ExpressMove>().TrackCount > 2)
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed;
                    }
                    else                                                                                                
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Fast_speed - 1.5f;
                    }
                }
                if (Player.GetComponent<PlayerInformation>().TrackCount < other.GetComponent<ExpressMove>().TrackCount)
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.Low_speed;
                    if (other.GetComponent<ExpressMove>().TrackCount - Player.GetComponent<PlayerInformation>().TrackCount > 1)
                    {
                        other.GetComponent<ExpressMove>().AI_speed = 2f;
                    }
                    else 
                    {
                        other.GetComponent<ExpressMove>().AI_speed = other.GetComponent<ExpressMove>().Slow_speed - 0.5f;
                    }
                }
                if (Player.GetComponent<PlayerInformation>().TrackCount == other.GetComponent<ExpressMove>().TrackCount)
                {
                    other.GetComponent<ExpressMove>().m_AI_speed = AI_Speed.Midle_speed;
                    other.GetComponent<ExpressMove>().AI_speed = 4.0f;
                }
            }
        }
    }
}
