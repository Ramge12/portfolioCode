using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulityPlay : MonoBehaviour {

    public Vector3 Player1_position;        
    public Vector3 Player2_position;        
    public float Player1_rotation;          
    public float Player2_rotation;          
    public int Player1_run = 0;             
    public int Player2_run = 0;              

    //cameara
    public Camera MainCamera;               //메인카메라
    public MiniMapCamera minimap;           //미니맵 카메라
    
    //player1
    public GameObject player_1Slide;        //플레이어 1의 썰매
    public GameObject Player_1;             //플레이어 1
    public DogMove P1_DogMove;              //플레이어 1의 개의 움직임 클래스
    public DogJoyStic P1_DogJoyStic;        //플레이어 1의 개의 조이스틱
    public RideAnimal P1_RideAnimal;        //플레이어 1의 RideAnimal 클래스
    public RemotePlayer P1_remote;          //플레이어 1의 원격조정할 클래스

    //player2
    public GameObject player_2Slide;        //플레이어 2의 썰매
    public GameObject   Player_2;           //플레이어 2
    public PengJoyStic  P2_PengJoyStic;     //플레이어 2의 조이스틱
    public PenguinMove  P2_PengMove;        //플레이어 2의 움직임 클래스
    public RemotePlayer P2_remote;          //플레이어 2를 원격조정할 클래스
    
    //UI
    public Text p1_position;                //플레이어1 포지션
    public Text p2_position;                //플레이어2 포지션
    public Text player_select;              //플레이어 선택 정보 Text UI
    public Text m_info_text;                //정보 UI에 출력할 text
    public GameObject m_info_UI;            //정보 UI

    public FileInput m_input;               //파일 입출력 정보
    public GameObject player_selected;      //플레이어 선택클래스 PlayerSelectinfo

    public bool Player_1_check;             //플레이어 1이 접속했는지에 대한 불값
    public bool Player_2_check;             //플레이어 2가 접속했는지에 대한 불값
    public bool goal;                       //Goal여부에 대한 불값
    public float P1_time;                   //플레이어1 레코드 기록 카운터
    public float P2_time;                   //플레이어2 레코드 기록 카운터
    float mulityTimer;                      //플레이어가 모두 접속했을때 시간을 잴 카운터

    public GameClient2 m_net;               //멀티 플레이 정보를 받아 올 수 있는 클라리언트 클래스
    public RecordPosition player1_rec;      //플레이어1 레코드 정보
    public RecordPosition player2_rec;      //플레이어2 레코드 정보

    void Start ()
    {
        if (GameObject.Find("PlayerInfo"))
        {
            player_selected = GameObject.Find("PlayerInfo");    
        }

        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 1)   
        {
            P2_remote.enabled = true;           //플레이어2 리모트 컨트롤 활성화
            P1_remote.enabled = false;          //플레이어1 리모트 컨트롤 비활성화
            P2_PengJoyStic.enabled = false;     //Player2 컨트롤 포기
            P2_PengMove.enabled = false;        //
            m_net.Start();                      //멀티플레이 네트워크 클라이언트 start()를 합니다
            m_net.m_bRequestNewVille = true;    //플레이어1이 호스트 권한을 가지고 있기때문에 newVille을 만드는것(방만들기)에 true값을 줍니다
            m_net.IssueConnect();               //port와 ip를 맞춥니다
            P1_time = player1_rec.replayTime;   //리플레이 타임을 받아옵니다
        }

        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 2)
        {
            P2_remote.enabled = false;          //Player2 원격제어를 포기한다
            P1_remote.enabled = true;           //Player1 원격제어를 실행합니다
            P1_DogMove.enabled = false;         //Player1 컨트롤 포기
            P1_DogJoyStic.enabled = false;      //
            P1_RideAnimal.enabled = false;
            m_net.Start();                      //멀티플레이 클라이언트 시작
            m_net.m_bRequestNewVille = false;   //Player2는 게스트이기때문에 호스트권한을 가지지않습니다
            m_net.IssueConnect();               //port와 ip를 맞춥니다
            P2_time = player2_rec.replayTime;   //리플레이 타임을 받아옵니다
        }

        Player1_position = player_1Slide.transform.position;            //플레이어 1의 포지션은 P1의 썰매 포지션을 받아옵니다
        Player2_position = player_2Slide.transform.position;            //플레이어 2의 포지션은 P2의 썰매 포지션을 받아옵니다
        Player1_rotation = player_1Slide.transform.localEulerAngles.y;  //플레이어 1의 회전값을 받아옵니다
        Player2_rotation = player_1Slide.transform.localEulerAngles.y;  //플레이어 2의 회전값을 받아옵니다.
    }

    void Update()
    {
        if (Player_1_check == true && Player_2_check == true && goal == false) 
        {
            mulityTimer += Time.deltaTime;                              
            m_info_text.text = "출발 까지 남은시간:" + ((int)(3.0f - mulityTimer)).ToString();
            if (mulityTimer > 3.0f)                                       
            {
                player1_rec.RecordMode = true;     
                player2_rec.RecordMode = true;     
                m_info_UI.SetActive(false);        
                P2_PengJoyStic.MoveSpeed = 5.0f;   
                P1_DogJoyStic.MoveSpeed = 5.0f;
            }
        }
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 1)
        {
            minimap.target = Player_1;                                                  //미니맵이 중심으로 잡을 타겟 =player1
            MainCamera.GetComponent<CameraController>().target = Player_1.transform;    //메인카메라 타겟도 Player1을 잡습니다
            Player1_rotation = player_1Slide.transform.localEulerAngles.y;              //Player1의 정보를 받아옵니다(기록용)
            Player1_position = player_1Slide.transform.position;
        }
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 2)
        {

            minimap.target = Player_2;                                                  //미니맵이 중심으로 잡을 타겟 =player
            MainCamera.GetComponent<CameraController>().target = Player_2.transform;    //메인카메라 타겟도 Player1을 잡습니다
            Player2_rotation = player_2Slide.transform.localEulerAngles.y;              //Player1의 정보를 받아옵니다(기록용)
            Player2_position = player_2Slide.transform.position;
        }
        Set_Joystick();
    }

    void Set_Joystick()//보내기(원래는 조이스틱정보를 보냈으나, 위치정보로 수정함;;)
    {
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 1)        //플레이어 번호가 1번일때
        {
            player_2Slide.transform.position = Player2_position;                        
            player_2Slide.transform.rotation = Quaternion.Euler(0, Player2_rotation, 0);    //플레이어 2의 위치와 회전값을 적용시킵니다.(원격제어 부분)
        }
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num ==2)         //플레이어 번호가 2번일때
        {
            player_1Slide.transform.position = Player1_position;
            player_1Slide.transform.rotation = Quaternion.Euler(0, Player1_rotation, 0);    //플레이어 1의 위치와 회전값을 적용시킵니다.(원격제어 부분)
        }
        player_select.text = player_selected.GetComponent<PlayerSelectInfo>().player_select_Num.ToString(); 
        p1_position.text = player_1Slide.transform.position.ToString();                   
        p2_position.text = player_2Slide.transform.position.ToString();                     
    }

    public void Record_player()//멀티 플레이에서 레코드 라인을 구분하는 하여 기록하는 함수(위치기록)
    {
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 1)
        {
            player1_rec.Write_Record();     //player1의 위치를 기록하는 함수를 실행합니다
        }
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 2)
        {
            player2_rec.Write_Record();     //Player2의 위치를 기록하는 함수를 실행합니다
        }  
    }

    public void Record_time_score()//멀티 플레이에서의 기록을 저장하는 함수(점수기록)
    {
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 1)
        {
            m_input.Write_score(player1_rec.replayTime);    //플레이어1 점수를 기록합니다
        }
        if (player_selected.GetComponent<PlayerSelectInfo>().player_select_Num == 2)
        {
            m_input.Write_score(player2_rec.replayTime);    //플레이어2 점수를 기록합니다
        }
    }
}
