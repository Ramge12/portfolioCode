using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{

    GameObject player;      
    public bool Jump = false;
    int WallNum = 0;
    Vector3 WallNormal;

    void Start()//벽의 태그값에 따라 Normal 벡터를 지정해둡니다
    {
        player = GameObject.Find("Player");
        if (this.gameObject.tag == "LeftWall")
        {
            WallNormal = new Vector3(1, 0, 0);
            WallNum = 1;
        }
        if (this.gameObject.tag == "RightWall")
        {
            WallNormal = new Vector3(-1, 0, 0);
            WallNum = 2;
        }
        if (this.gameObject.tag == "BackWall")
        {
            WallNormal = new Vector3(0, 0, -1);
            WallNum = 3;
        }
        if (this.gameObject.tag == "FrontWall")
        {
            WallNormal = new Vector3(0, 0, 1);
            WallNum = 4;
        }
    }

    void Update() {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                player.GetComponent<PlayerCtr>().WallJump(this.gameObject, WallNormal);
                Jump = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerCtr>().onWall = false;
            Jump = false;
        }
    }
}
