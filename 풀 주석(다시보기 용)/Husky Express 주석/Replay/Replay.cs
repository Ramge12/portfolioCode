using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour {

    public FileInput File_Input;                                //기록된 파일을 읽을 파일입출력 매니저입니다.
    public Text ReplayEnd;                                      //리플레이가 끝났을때 보여줄 UI입니다
    public int player_num;                                      //플레이어 정보를 받아올 변수입니다.(Player1,2)

    List<Vector3> player_position = new List<Vector3>();        //플레이어 리플레이 포지션 리스트값
    List<float> player_rotationn = new List<float>();           //플레이어 리플레이 회전 리스트값
    int replayCount;                                            //리플레이 총 정보수 비교를 위한 변수(리스트에사용)         
    int TimerCount;                                             //리플레이가 얼마나 진행해는지 체크하는 변수(카운트 비교에 사용)
    float replayTime = 0;                                       //
    float replayCheck = 10;                                     //기록과 마찬가지로 0.01초 단위로 실행하기 위한 변수입니다.

    // Use this for initialization
    void Start () {
        player_position = File_Input.ReadData(player_num);      //리플레이정보를 받아옵니다
        player_rotationn = File_Input.ReadData_RO(player_num);  //리플레이정보를 받아옵니다
    }
	// Update is called once per frame
	void Update () {
    }
    private void FixedUpdate()
    {
        ReadRecord();
    }
    void ReadRecord()//리플레이를 실행합니다
    {
        replayTime += Time.deltaTime;
        float lookTimer = Mathf.Floor(replayTime * 100) * 0.01f;//소수 2번쨰자리수까지만 받아옵니다.
        if (lookTimer != replayCheck)                           //0.01초마다 기록한 값이라 0.01초마다 읽을 수 있도록
        {                                                       
            if (player_position.Count>TimerCount)               //플레이어 레코드 라인의 길이가 플레이어가 읽은 길이보다 클때(아직 읽을 데이터가 남아있을때)
            {
                transform.position = player_position[replayCount];  
                transform.localEulerAngles = new Vector3(0, player_rotationn[replayCount], 0);
                replayCheck = lookTimer;                        //같은 시간대 값을 못읽도록 replaycheck값을 바꾸어 lookTimer != replayCheck에서 false값이 오도록 합니다
                replayCount++;                                  //replayCount를 인덱스삼아 리스트를 읽습니다
                TimerCount++;                                   //TimeCount를 통해 현재 얼마나 읽었는지 파악합니다
            }
            else
            {
                ReplayEnd.text = "리플레이가 모두 종료되었습니다";
            }
        }
    }
}
