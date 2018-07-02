using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterNode : MonoBehaviour {

    public GameObject nextNode;     //다음 노드를 미리 받아옵니다

	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Penguin") //펭귄이 AI이므로 펭귄이 충돌했다면
        {
            if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.Midle_speed)
            {   //펭귄이 Middle스피드 였다면
                other.GetComponent<ExpressMove>().CurrenPosition = other.transform.position;//ExpressMove의 CurrenPosition을 충동할 떄의 포지션으로 바꾼다
                if (nextNode)   //이 Node에 NextNode가 있다면 
                {
                    other.GetComponent<ExpressMove>().nextNode = nextNode;  //ExpressNode의 nextNode의 이 Node의 NextNode를 넣어줍니다
                }
                else    //NextNode가 없다면 
                {
                    other.GetComponent<ExpressMove>().nextNode = null;  //ExpressNode에 null값을 넣어줍니다
                }
            }
            if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.High_speed)//AI스피드가 High_speed일 경우
            {
                if(other.GetComponent<ExpressMove>().final) //이 노드가 마지막 노드일경우
                {
                    other.GetComponent<ExpressMove>().CurrenPosition = other.transform.position;//ExpressMove에 CurrenPosition을 충돌할 떄의 위치로 줍니다
                    other.GetComponent<ExpressMove>().nextNode = null;//마지막 노드 이므로 NextNode에 null값을 줍니다
                }
            }
            if (other.GetComponent<ExpressMove>() && other.GetComponent<ExpressMove>().m_AI_speed == AI_Speed.Low_speed)//AI가 충돌했을떄 Low_speed일경우
            {
                if(nextNode)    //현재 노드가 nextNode가 있을경우
                {
                    other.GetComponent<ExpressMove>().CurrenPosition = other.transform.position;    
                    if (nextNode.GetComponent<ExpressNode>().NextNode)
                    {
                        other.GetComponent<ExpressMove>().nextNode = getByNodeFar(nextNode.GetComponent<ExpressNode>().NextNode, nearNode());   
                        //Next노드는 getByNodeFar을 통애 인접한 노드에서 가장 먼노드를 nextNode에 넣습니다
                    }
                    else
                    {
                        other.GetComponent<ExpressMove>().nextNode = getByNodeFar(nextNode, nearNode());
                    }
                }
                else //현재 노드가 nextNode가 없을 경우
                {
                    other.GetComponent<ExpressMove>().nextNode = null;  //ExpressMove에nextNode에 null값을 넣어줍니다
                }
            }
            if (other.GetComponent<ExpressMove>().nextNode)//Next노드가 있을 경우
            {   //AI가 진행할 방향을 결정합니다
                other.GetComponent<ExpressMove>().ExpressDir = other.GetComponent<ExpressMove>().nextNode.transform.position - other.transform.position;
            }
        }
    }
    GameObject[] nearNode() //트랙사이를 거쳐갈 노드
    {
        GameObject[] near_Node = new GameObject[3];
        //12개의 점과 다음 노드의 중점은 이미 정해져있으므로 따로 불러올 것은 없습니다.
        float Min_Dist = 100;
        for (int i = 0; i < 3; i++) //12개의 노드의 거리를 비교하여 가장 가까운 2점을 골라 3개의 점을 반환합니다
        {
            if (getDistance(transform.parent.parent.GetComponent<ExpressNode>().Left_Node[i], nextNode) < Min_Dist)
            {
                Min_Dist = getDistance(transform.parent.parent.GetComponent<ExpressNode>().Left_Node[i], nextNode);
                near_Node = transform.parent.parent.GetComponent<ExpressNode>().Left_Node;
            }
            if (getDistance(transform.parent.parent.GetComponent<ExpressNode>().Right_Node[i], nextNode) < Min_Dist)
            {
                Min_Dist = getDistance(transform.parent.parent.GetComponent<ExpressNode>().Right_Node[i], nextNode);
                near_Node = transform.parent.parent.GetComponent<ExpressNode>().Right_Node;
            }
            if (getDistance(transform.parent.parent.GetComponent<ExpressNode>().Top_Node[i], nextNode) < Min_Dist)
            {
                Min_Dist = getDistance(transform.parent.parent.GetComponent<ExpressNode>().Top_Node[i], nextNode);
                near_Node = transform.parent.parent.GetComponent<ExpressNode>().Top_Node;
            }
            if (getDistance(transform.parent.parent.GetComponent<ExpressNode>().Bottom_Node[i], nextNode) < Min_Dist)
            {
                Min_Dist = getDistance(transform.parent.parent.GetComponent<ExpressNode>().Bottom_Node[i], nextNode);
                near_Node = transform.parent.parent.GetComponent<ExpressNode>().Bottom_Node;
            }
        }
        return near_Node; //12개의 점들중 가장 가까운 거리를 가지는 점의 가장자리에 있는 3개의 점을 반환합니다
    }
    GameObject getByNodeFar(GameObject fromNode, GameObject[] Cross_Node)//가장 먼 거리의 노드
    {
        //추려진 3개의 노드와 그 다음노드(next의 next)의 위치를 비교합니다
        float Max_Dist = 0;
        GameObject ByNode = null;
        for (int i = 0; i < Cross_Node.Length; i++) //가장 먼거리의 점을 구합니다
        {
            if (getDistance(fromNode, Cross_Node[i]) > Max_Dist)
            {
                Max_Dist = getDistance(fromNode, Cross_Node[i]);
                ByNode = Cross_Node[i];
            }
        }
        return ByNode;  //역시 3개의 점들중 가장 먼거리의 점을 반환합니다.
    }

    float getDistance(GameObject obj1, GameObject obj2) //게임 오브젝트 2개가 주어지면 두 오브젝트 사이의 거리를 구합니다
    {
        float distance, distanceX, distanceZ;
        distanceX = obj1.transform.position.x - obj2.transform.position.x;
        distanceZ = obj1.transform.position.z - obj2.transform.position.z;
        distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));
        return distance;
    }
}
