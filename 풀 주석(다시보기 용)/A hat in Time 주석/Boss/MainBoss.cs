using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainBoss : MonoBehaviour {
    public enum Boss_State
    {
        Idle,
        Patton1,
        Patton2,
        Patton3,
        Die
    }
    public Boss_State BossSt;           //보스의 상태문   
    public Text bossText;               //보스 캐릭터의 말을 표시할 텍스트
    public Animator Boss_Ani;           //보스캐릭터의 애니메이션 
    public GameObject sandClock;        //보스캐릭터 사망후 표시할 시계아이템
    public GameObject Box;              //보스 캐릭터 패턴에서 사용할 폭발 박스
    public GameObject Pos1;             //박스의 포지션 1
    public GameObject Pos2;             //박스의 포지션 2
    public GameObject Pos3;             //박스의 포지션 3
    public GameObject Pos4;             //박스의 포지션 4
    public GameObject flag;             //보스 캐릭터 사망후 표시할 깃발
    public ParticleSystem DustSmoke;    //보스 캐릭터 이동시 표시할 이펙트
    public bool Drop = false;           //폭탄이 떨어지는지 체크하는 불값
    public bool m_Left = true;          //보스 캐릭터 이동시 좌 우 판정하는 불값

    int Boss_Hp = 10;                   //보스 캐릭터의 체력
    int RandNum = 0;                    //보스 캐릭터 패턴에서 랜덤으로 상자가 떨어지는 체크하는 수
    float walkSpeed = 10.0f;            //보스 캐릭터의 이동속도
    float BTimer = 0f;                  //폭탄의 타이머
    float EndTimer;                     //보스 사망후 엔딩까지 체크할 타이머
    float BossTimer = 0;                //보스의 패턴을 재는 타이머
    bool Patton2 = false;               //패턴2를 확인하는 불값
    bool Patton3 = false;               //패턴3를 확인하는 불값
    Rigidbody Charactor;                //캐릭터의 리지드바디

    // Use this for initialization
    void Start () {
        EndTimer = 0;                   //타이머를 초기화 시켜준다
        DustSmoke.Stop();               //흙먼지 효과를 멈추어준다
        Charactor = transform.GetComponent<Rigidbody>();    //리지드바디를 받아 넣어줍니다
        BossSt = Boss_State.Idle;       //상태는 IDLE로 둡니다
    }
    // Update is called once per frame
    void Update()
    {
        BossTimer += Time.deltaTime;                //보스 패턴의 시간을 deltaTime을 통해서 잽니다
        BTimer += Time.deltaTime;                   //폭탄의 타이머를 deltaiTine을 통해서 잽니다
        Charactor.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX; //회전이나 X축 이동을 하지 못하게 막아둡니다
         if (BossTimer > 10.0f)                     //보스 타이머가 10초가 넘어갈 경우 패턴을 시작합니다
        {
            if (Boss_Hp > 7 && BTimer > 2.0f)       //보스의 채력이 7보다 크고 2초가 경과했을때
            {
                Boss_Ani.SetBool("patton1", true);  //패턴1을 시작합니다
                BossSt = Boss_State.Patton1;
                BossPatton1();
            }
            else if (Boss_Hp <= 7 && Boss_Hp > 3)   //보스의 체력이 7이하이고 3보다 클때
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                Boss_Ani.SetBool("Patton2", true);
                BossSt = Boss_State.Patton2;        //패턴2를 시작합니다
                BossPatton2();
            }
            else if (Boss_Hp <= 3 && Boss_Hp > 0)   //보스이 체력이 3이하이고 0보다 클때
            {
                Boss_Ani.SetBool("Patton2", false);
                Boss_Ani.SetBool("patton1", true);
                BossSt = Boss_State.Patton3;        //패턴3을 시작합니다
                BossPatton2();                      //패턴3은 패턴1과 패턴2를 동시에 사용합니다
                BossPatton1();
            }
            else if (Boss_Hp <= 0)                  //보스의 체력이 0이하이면
            {
                EndTimer += Time.deltaTime;
                if(EndTimer>7.0f) SceneManager.LoadScene("Endig");  //7초뒤 엔딩씬으로 넘어갑니다
                flag.SetActive(true);
                BossSt = Boss_State.Die;            //보스가 죽은 후 각종 아이템이 나타납니다
                Boss_Ani.SetBool("Death", true);
                if(sandClock)sandClock.SetActive(true); 
            }
        }
        BossTalk();
    }
    public void BossTalk()//보스가 대화부분에 출력할 string을 고르는 함수입니다
    {
        if (BossSt == Boss_State.Idle)  //상태와 시간에 따라 출력할 말을 고릅니다
        {  
            if (BossTimer>0 && BossTimer < 3) bossText.text = "다 들었어 꼬마녀석! \n 모래시계를 원한다고?";
            else if (BossTimer >3 && BossTimer < 7) bossText.text = "이걸 어쩐다! 이젠 내껀데";
            else if (BossTimer > 7 && BossTimer < 10) bossText.text = "날 쓰러트리면 다시 생각해보지!";
            else bossText.text = "";
        }
        else if (BossSt == Boss_State.Die) bossText.text = "내가 졌어...\n 너 정말 강하구나!";
        else bossText.text = "";
    }
    public void Hurt()//보스의 체력이 감소되는 함수
    {
        Boss_Hp--;
    }
    void BossPatton1()//보스 패턴1
    {
        if(m_Left)//좌우를 판단하여 
        {
            transform.localEulerAngles = new Vector3(0, -180, 0);                   //캐릭터를 회전시킨뒤
            transform.position += (transform.forward * walkSpeed * Time.deltaTime); //정면으로 이동합니다
            DustSmoke.Play();
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position += (transform.forward * walkSpeed * Time.deltaTime);
            DustSmoke.Play();
        }
    }
    void BossPatton2()
    {
        if(BTimer%5<0.1 && Drop)    //BTimer가 5초가 경과하고 Drop이 true이면
        {   
            Drop = false;
            Patton2 = false;        //초기화를 해준다
        }
        if(!Patton2)
        {
            Patton2 = true;
            RandNum = Random.Range(1, 5);   //1,2,3,4 의 수에 각각 박스 스폰 위치를 연결해두고
                                            //무작위로 나온 한 수를 제외하고 스폰을 한다
            GameObject boxSpone1;
            GameObject boxSpone2;
            GameObject boxSpone3;
            switch (RandNum)                //미리 지정해둔 위치에서 한 자리를 제외한 3자리에서 스폰시킵니다
            {
                case 1:
                    boxSpone1 = Instantiate(Box, Pos2.transform.position, Quaternion.identity);
                    boxSpone2 = Instantiate(Box, Pos3.transform.position, Quaternion.identity);
                    boxSpone3 = Instantiate(Box, Pos4.transform.position, Quaternion.identity);
                    break;
                case 2:
                    boxSpone1 = Instantiate(Box, Pos1.transform.position, Quaternion.identity);
                    boxSpone2 = Instantiate(Box, Pos3.transform.position, Quaternion.identity);
                    boxSpone3 = Instantiate(Box, Pos4.transform.position, Quaternion.identity);
                    break;
                case 3:
                    boxSpone1 = Instantiate(Box, Pos1.transform.position, Quaternion.identity);
                    boxSpone2 = Instantiate(Box, Pos2.transform.position, Quaternion.identity);
                    boxSpone3 = Instantiate(Box, Pos4.transform.position, Quaternion.identity);
                    break;
                case 4:
                    boxSpone1 = Instantiate(Box, Pos1.transform.position, Quaternion.identity);
                    boxSpone2 = Instantiate(Box, Pos2.transform.position, Quaternion.identity);
                    boxSpone3 = Instantiate(Box, Pos3.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
    void BossPatton3()
    {
        if (Patton3)        //패턴3는 모두 사용합니다
        {
            BossPatton1();
            BossPatton2();
        }
        else                //패턴3가 시작될떄 패턴2에서 보스캐릭터가 바닥에 있으므로 공위로 올라가기 위해서 점프합니다
        {
            Charactor.AddForce(transform.up * 5.0f * 2, ForceMode.Impulse);
            Patton3 = true;
        }
    }
}
