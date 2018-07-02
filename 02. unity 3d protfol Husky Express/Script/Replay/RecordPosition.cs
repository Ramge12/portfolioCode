using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPosition : MonoBehaviour {

    public PlayerChractor m_player_info;                    
    public FileInput File_input;                            
    public float replayTime = 0;                              
    public bool RecordMode = false;                         

    List<Vector3> Player_position = new List<Vector3>();    //플레이어 포지션을 기록할 리스트
    List<float> Rotation_y = new List<float>();             //플레이어 회전값을 기록하 리스트
    float replayCheck = 10;                                 //시간을 기록할떄 쓰는 변수
    int recordCount = 0;                                    //총기록된 레코드의 갯수


    void Start () {
	}

	void Update () {
         if(RecordMode)
         { 
            replayTime += Time.deltaTime;
            float lookTimer = Mathf.Floor(replayTime * 100) * 0.01f;    
            //시간을 기록하며 소수둘째자리까지만 체크한다
            if (lookTimer != replayCheck)
            {
                Rotation_y.Add(transform.localEulerAngles.y);
                Player_position.Add(transform.position);        //리플레이 리스트에 포지션, 회전값을 추가합니다
                replayCheck = lookTimer;
                recordCount++;
                //소수둘째짜리까지만 체크하지만, 업데이트는 더 작은 시간에서도 돌아갑니다
                //따라서 0.1초에 단한번만 체크하기위헤 replayCheck 기록된 시간을 저장하고 
                //소수 둘째자리까지 일치하는 경우에는 기록되지않도록합니다.
            }
        }
    }

    public void RecordStop()//기록정지함수입니다.
    {
        RecordMode = false;  
    }

    public void Write_Record()//지금까지한 기록을 모두 파일에 저정합니다.
    {
       File_input.Write_Player_Position(recordCount, Player_position, Rotation_y,m_player_info.playerCase);
        Debug.Log("기록완료 Player" + m_player_info.playerCase.ToString());
    }
}
