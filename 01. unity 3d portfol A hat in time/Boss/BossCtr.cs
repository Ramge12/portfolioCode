using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossCtr : MonoBehaviour {

    public Text BossTalk;                                                             
    public GameObject Player;           
    public GameObject barels;           
    public GameObject barelsPosition;   

    int barrelsCount;                   
    float NextTimer;                    
    float PattonTimer;                  
    float rotationSpeed = 1.5f;         
    bool animeCheck = false;            
    bool t = false;                     
    bool m_idle;                        //보스의 상태 보스는 idle인 상태와 idle이 아닌 상태 2가지 상태를 가지고 있다
    string talk;                        
    Animator BossAni;                   
    GameObject Ballon;                                          


    void Start () {
        BossAni = transform.GetComponent<Animator>();   
        barrelsCount = 0;                                   
    }

	void Update () {
        PattonTimer += Time.deltaTime; 
        Vector3 targetDir = Player.transform.position - transform.position;
        targetDir.y = 0;

        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);  
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (PattonTimer >= 40)                      
        {
            NextTimer += Time.deltaTime;            
            BossAni.SetBool("RoundKick", false);                                   
            if(!m_idle)
            {
                m_idle = true;
                BossAni.SetBool("Idle", true);     
            }
            talk = "자네 제법이군! 잠시만 기다려주게\n 내가 보스에게 연락해보지";
            BossTalk.text = talk;
            int Hp = Player.GetComponent<PlayerCtr>().PlayerHP;    
            PlayerPrefs.SetInt("HP", Hp);
            if (NextTimer>5.0f) SceneManager.LoadScene("BigBoss");  
        }
        else if(PattonTimer>=10)   
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
        else if(PattonTimer>=7)
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
            Dir.y = 3;                                                     
            if ( !animeCheck)                                              
            {
                animeCheck = true;                                           
                barrelsCount++;
                 GameObject obBarels=Instantiate(barels, barelsPosition.transform.position, Quaternion.identity);
                obBarels.GetComponent<BarrelMove>().Bstart = false;
                obBarels.GetComponent<BarrelMove>().ThrowBarrel(Dir * 5.0f);   
            }
        }
        if (BossAni.GetCurrentAnimatorStateInfo(0).IsName("RoundKick") & animationTime >= 0.95f)
        {
            animeCheck = false;
        }
    }
}
