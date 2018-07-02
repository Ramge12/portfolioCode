using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
public class GameClient2 : MonoBehaviour {

    public enum eState { Connecting, LoggingOn, InVille, Failed }   
    HostID m_p2pGroupID = HostID.HostID_None;                       
    public MulityPlay m_MulityPlay;                                 
    public GameObject player_selected;                              //플레이어 1,2를 구분하는 오브젝트
    public eState m_eState;                                         
    public bool m_bRequestNewVille = false;                         //호스트권한

    string m_szServerAddr = "localhost";                            //서버 address는 localhost
    string m_szVilleName = "Janna";                                 //서버 명                   

    NetClient m_netClient = null;                                   // ProudNet NetClient
    SocialC25.Proxy m_C2SProxy = new SocialC25.Proxy();             // ProudNet Proxy
    SocailS2C.Stub m_S2CStub = new SocailS2C.Stub();                // ProudNet Stub
    SocialC2C.Stub m_C2CStub = new SocialC2C.Stub();                // C2C 클라이언 to 클리이언트, S2C 서버 to 클라이언트 , C25 클라이언트 to 서버
    SocialC2C.Proxy m_C2Croxy = new SocialC2C.Proxy();              // p2p

    private void Awake()
    {
        m_netClient = new NetClient();                              //네트워크 클라이언트에 프라우드넷 클라이언트를 불러옵니다(Nettention.Proud)
    }

    public void Start()
    {
        m_netClient.AttachProxy(m_C2SProxy);
        m_netClient.AttachStub(m_S2CStub);                          //Proxy와 Stub값을 넣어줍니다
        m_eState = eState.LoggingOn;                                //상태값을 바꾸어줍니다
        m_netClient.JoinServerCompleteHandler = JoinServerCompleteHandler;
        m_netClient.LeaveServerHandler = LeaveServerHandler;        //서버가 끊긴것에 대하 정보
        m_S2CStub.ReplyLogon = ReplyLogon;                          // RequestLogon 후 이벤트
        m_netClient.AttachProxy(m_C2Croxy);                         //p2p(클라이언트 간의) Proxy와
        m_netClient.AttachStub(m_C2CStub);                          //stub값을 넣어줍니다
        m_C2CStub.Player_1Point = Player_1Point;                    //클라이언트 간에 교환할 Player1,2의 위치값 정보
        m_C2CStub.Player_2Point = Player_2Point;
        m_netClient.P2PMemberJoinHandler = (HostID memberHostID, HostID groupHostID, int memberCount, ByteArray customField) =>
        {
            m_p2pGroupID = groupHostID;                             //P2P그룹에서 HostID 를 받아온다
        };
    }

    private void OnDestroy()
    {
        m_netClient.Dispose();//클라이언트를 끕니다
    }

    void Update()
    {
        if (GameObject.Find("PlayerNum"))                   
        {
            player_selected = GameObject.Find("PlayerNum");    
        }
        switch (m_eState)
        {
            case eState.InVille:
                Update_Vile();//서버상에서 실행하는 함수(연결된 상태가 InVille)
                break;
        }
        // 서버에서 도착하는 메시지나 이벤트를 처리하는 함수. 호출해 주어야 이벤트가 콜백이 됨.
        if (m_netClient != null)
            m_netClient.FrameMove();
    }

    void Update_Vile()
    {
        bool pushing = Input.GetMouseButton(0);
        bool clicked = Input.GetMouseButtonUp(0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject pickedObject = null;
        if (Physics.Raycast(ray, out hit)) pickedObject = hit.transform.root.gameObject;
        RmiContext r = RmiContext.UnreliableSend.Clone();
        r.enableLoopback = true;
        m_C2Croxy.ScribblePoint(m_p2pGroupID, r, hit.point);  //scriiblePoint함수를 실행합니다
    }

    void CheckPlayer()
    {
        RmiContext r = RmiContext.UnreliableSend.Clone();
        r.enableLoopback = true;
        if (player_selected.GetComponent<PlayerNum>().player_select_Num == 1)//플레이어 1일 경우
        {   //플레이어1의 정보를 서버에 넘거 상대 클라이언트로 전달합니다
            m_C2Croxy.Player_1Point(m_p2pGroupID, r, m_MulityPlay.Player1_position, m_MulityPlay.Player1_rotation,1); 
        }
        if (player_selected.GetComponent<PlayerNum>().player_select_Num ==2)//플레이어 2일 경우
        {   //플레이어2의 정보를 서버에 넘거 상대 클라이언트로 전달합니다
            m_C2Croxy.Player_2Point(m_p2pGroupID, r, m_MulityPlay.Player2_position ,m_MulityPlay.Player2_rotation, 1);
        }
    }

    public void IssueConnect()
    {
        m_eState = eState.InVille;
        NetConnectionParam cp = new NetConnectionParam();
        cp.serverIP = m_szServerAddr;
        cp.serverPort = 12349;
        // 프로토콜 버전 : 서버랑 클라이언트랑 버전이 같은지 판별, 두 버전이 같을 때만 접속이 허가됩니다.
        cp.protocolVersion = new Guid("{0x9213733,0x4631,0x4034,{0x90,0x58,0xbf,0xe6,0x23,0x53,0x9b,0xff}}");
        // 이 함수는 비동기로 실행됩니다. 그래서 호출을 하면 바로 리턴하고 백그라운드로 서버와 연결이 진행됩니다.
        m_netClient.Connect(cp);
    }

    // Request, Response
    public void JoinServerCompleteHandler(ErrorInfo info, ByteArray replyFromServer)//서버에 성공적으로 접속했을떄
    {
        if (info.errorType == ErrorType.ErrorType_Ok)
        {
            m_eState = eState.LoggingOn;
            m_C2SProxy.RequestLogon(HostID.HostID_Server, RmiContext.ReliableSend, m_szVilleName, m_bRequestNewVille);
            //로그인 정보를 전달합니다
        }
    }

    public void LeaveServerHandler(ErrorInfo info)//서버에 접속 못했을때
    {
        m_MulityPlay.Player_2_check = false;    //양쪽의 플레이어 check의 false값을 줍니다
        m_MulityPlay.Player_1_check = false;
    }

    public bool ReplyLogon(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int result, string comment)
    {
        switch (result)
        {
            case 0:
                m_eState = eState.InVille;
                break;

            default:
                m_eState = eState.Failed;
                break;
        }
        return true;
    }

    public bool ScribblePoint(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point)
    {
        //서로 접속을 했을 때 실행하는 함수
        CheckPlayer();
        return false;
    }

    public bool Player_1Point(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
    {
        m_MulityPlay.Player1_position = point;      //플레이어 1의 정보를 서버를 통해 상대 클라이언트에 전달합니다
        m_MulityPlay.Player1_rotation = rotation;
        m_MulityPlay.Player1_run = 1;
        m_MulityPlay.Player_1_check = true;
        return  true;
    }

    public bool Player_2Point(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, UnityEngine.Vector3 point, float rotation, int run)
    {
        m_MulityPlay.Player2_position = point;      //플레이어 2의 정보를 서버를 통해 상태 클라이언트에 전달합니다
        m_MulityPlay.Player2_rotation = rotation;
        m_MulityPlay.Player2_run = 1;
        m_MulityPlay.Player_2_check = true;
        return true;
    }
}
