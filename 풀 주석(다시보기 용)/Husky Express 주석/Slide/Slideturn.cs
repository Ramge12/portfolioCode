using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slideturn : MonoBehaviour
{
    public GameObject Dog;          //썰매가 따라갈 동물(개)
    public float Range = 5.0f;      //썰매를 따라갈 범위(개와 5만큼 멀어지면 개를 따라갑니다)

    // Use this for initialization
    void Start(){
    }
    // Update is called once per frame
    void Update(){
        SlideTurn();
    }
    void SlideTurn()
    {
        /*썰매와 개 사이 거리를 실시간으로 체크하며 정해둔 기준을 넘어갈 경우 썰매가 개를 바라보도록 만들었습니다*/
        float distance;
        float distanceX = Mathf.Abs(Dog.transform.position.x - this.transform.position.x);
        float distanceZ = Mathf.Abs(Dog.transform.position.z - this.transform.position.z);
        distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));//썰매와 개 사이의 거리

        float angle = Mathf.Atan2(Dog.transform.position.x - this.transform.position.x,Dog.transform.position.z - this.transform.position.z) * 180 / Mathf.PI;
        //개와 썰마의 위치를 이용하여 각도를 구합니다(높이 제외하고 사용)
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);//쿼터니언값으로 변환합니다
        if (distance > Range)transform.rotation = rotation;                             //거리가 지정된 범위를 넘어가면 회전하도록 합니다
    }
}
