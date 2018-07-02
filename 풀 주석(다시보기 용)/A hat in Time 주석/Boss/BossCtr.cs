using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossCtr : MonoBehaviour {

    public Text BossTalk;               //보스캐릭터 대화창
    public GameObject Player;           //플레이어
    public GameObject barels;           //나무통
    public GameObject barelsPosition;   //나무통 생성 위치

    int barrelsCount;                   //나무통의 갯수를 세는 카운트
    float NextTimer;                    //패턴지 종료된 후 마지막 보스로 가기까지 시간을 재는 카운트
    float PattonTimer;                  //패턴 경과 시간을 재는 카운터
    float rotationSpeed = 1.5f;         //나무통의 회전속도
    bool animeCheck = false;            //
    bool t = false;                     //
    bool m_idle;                        //보스의 상태문 보스는 idle인 상태와 idle이 아닌 상태 2가지 상태를 가지고 있다
    string talk;                        //대화문
    Animator BossAni;                   //중간 보스 캐릭터 애니메이션
    GameObject Ballon;                  //말풍선

    // Use this for initialization
    void Start () {
        BossAni = transform.GetComponent<Animator>();   //보스 캐릭터의 애니메이터를 가져옵니다
        barrelsCount = 0;                               //나무통의 갯수를 셀 캐운터를 초기화합니다
    }
	// Update is called once per frame
	void Update () {
        PattonTimer += Time.deltaTime;  //패턴의 경과 시간을 확인하기위해 deltaTime을 사용합니다
        // 보스 캐릭터와 플레이어 캐릭터의 위치를 확인하고 보스 캐릭터가 플레이어 캐릭터를 바라볼수 있도록 회전시킵니다
        Vector3 targetDir = Player.transform.position - transform.position;
        targetDir.y = 0;
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);  
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (PattonTimer >= 40)                      //40초가 경과하면 마지막 보스신으로 넘어가도록 합니다
        {
            NextTimer += Time.deltaTime;            //패턴이 마친후 다음 보스로 넘어가기 까지 시간을 재는 카운터
            BossAni.SetBool("RoundKick", false);    //공격 애니메이션을 정지합니다
            if(!m_idle)
            {
                m_idle = true;
                BossAni.SetBool("Idle", true);      //보스의 IDLE애니메이션을 재생합니다
            }
            talk = "자네 제법이군! 잠시만 기다려주게\n 내가 보스에게 연락해보지";
            BossTalk.text = talk;
            int Hp = Player.GetComponent<PlayerCtr>().PlayerHP;     //플레이어 HP를 프리팹에 저장합니다
            PlayerPrefs.SetInt("HP", Hp);
            if (NextTimer>5.0f) SceneManager.LoadScene("BigBoss");  //마지막 보스씬으로 넘어갑니다
        }
        else if(PattonTimer>=10)    //10초가 넘어가면 공격을 시작합니다
        {
            UpdateAnicheck();
            if (PattonTimer >= 30)
            {
                talk = "쿨가이는 거짓말 하지 않는다!";
                BossTalk.text = talk;
            }
            else if (PattonTimer >= 20)
            {
                talk = "마침 몸이 근질거렸거든";
                BossTalk.text = talk;
            }
            else
            {
                talk = "명심하게 딱 30초야!\n 더 주지도 덜 주지도 못한다고!";
                BossTalk.text = talk;
            }
        }
        else if(PattonTimer>=7)//시간의 경과에 따라 대화문을 바꾸어 넣어줍니다
        {
            talk = "딱 30초만 내 공격을 버티면 \n 보스에게 데려다 주지";
            BossTalk.text = talk;
        }
        else if (PattonTimer >=4)
        {
            talk = "모래시계를 원한다고 들었네\n하지만 그건 우리 보스에게 있지";
            BossTalk.text = talk;
        }
        else 
        {
            talk = "안녕하신가 나는 쿨가이라고 하네";
            BossTalk.text = talk;
        }
    }
    void UpdateAnicheck()
    {
        float animationTime = BossAni.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)BossAni.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //현재 진행중인 애니메이션의 전체 부분중 얼마나 왔는지 확인하는 부분
        if (BossAni.GetCurrentAnimatorStateInfo(0).IsName("RoundKick") & animationTime >= 0.35f)//보스 애니메이션이 0.35를 넘었을때 
        {
            Vector3 Dir = Player.transform.position - transform.position;
            Dir.y = 3;                                                      //보스가 플레이어를 바라보는 방향을 구한다
            if ( !animeCheck)                                               //애니메이션 체크
            {
                animeCheck = true;                                          //애니메이션 전환하며 나무통을 발사합니다
                barrelsCount++;
                 GameObject obBarels=Instantiate(barels, barelsPosition.transform.position, Quaternion.identity);
                obBarels.GetComponent<BarrelMove>().Bstart = false;
                obBarels.GetComponent<BarrelMove>().ThrowBarrel(Dir * 5.0f);    //나무통을 생성해서 던집니다
            }
        }
        if (BossAni.GetCurrentAnimatorStateInfo(0).IsName("RoundKick") & animationTime >= 0.95f)
        {
            animeCheck = false; //0.95부분이 넘어가면 다음 공격 애니메이션에 발사할수 있도록 false처리를합니다
        }
    }
}
