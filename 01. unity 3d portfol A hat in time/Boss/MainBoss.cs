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

    public Boss_State BossSt;           
    public Text bossText;               
    public Animator Boss_Ani;           
    public GameObject sandClock;        
    public GameObject Box;              //보스 캐릭터 패턴에서 사용할 폭발 박스
    public GameObject Pos1;             
    public GameObject Pos2;             
    public GameObject Pos3;             
    public GameObject Pos4;             
    public GameObject flag;             
    public ParticleSystem DustSmoke;    
    public bool Drop = false;           
    public bool m_Left = true;             

    int Boss_Hp = 10;                 
    int RandNum = 0;                    //보스 캐릭터 패턴에서 랜덤으로 상자가 떨어지는 체크하는 수
    float walkSpeed = 10.0f;            
    float BTimer = 0f;                  
    float EndTimer;                     
    float BossTimer = 0;                
    bool Patton2 = false;               
    bool Patton3 = false;               
    Rigidbody Charactor;                


    void Start () {
        EndTimer =                 0;                   
        DustSmoke.Stop();               
        Charactor = transform.GetComponent<Rigidbody>();    
        BossSt = Boss_State.Idle;    
    }
                       
    void Update()
    {
        BossTimer += Time.deltaTime;               
        BTimer += Time.deltaTime;                           
        Charactor.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX; 
         if (BossTimer > 10.0f)                     
        {
            if (Boss_Hp > 7 && BTimer > 2.0f)       
            {                                                                                            
                Boss_Ani.SetBool("patton1", true);
                BossSt = Boss_State.Patton1;
                BossPatton1();
            }
            else if (Boss_Hp <= 7 && Boss_Hp > 3)   
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                Boss_Ani.SetBool("Patton2", true);
                BossSt = Boss_State.Patton2;        
                BossPatton2();
            }
            else if (Boss_Hp <= 3 && Boss_Hp > 0)   
            {
                Boss_Ani.SetBool("Patton2", false);
                Boss_Ani.SetBool("patton1", true);
                BossSt = Boss_State.Patton3;        
                BossPatton2();                      
                BossPatton1();
            }
            else if (Boss_Hp <= 0)                                          
            {
                EndTimer += Time.deltaTime;
                if(EndTimer>7.0f) SceneManager.LoadScene("Endig");  
                flag.SetActive(true);
                BossSt = Boss_State.Die;        
                Boss_Ani.SetBool("Death", true);
                if(sandClock)sandClock.SetActive(true); 
            }
        }
        BossTalk();
    }

    public void BossTalk()
    {
        if (BossSt == Boss_State.Idle)  
        {  
            if (BossTimer>0 && BossTimer < 3) bossText.text = "다 들었어 꼬마녀석! \n 모래시계를 원한다고?";
            else if (BossTimer >3 && BossTimer < 7) bossText.text = "이걸 어쩐다! 이젠 내껀데";
            else if (BossTimer > 7 && BossTimer < 10) bossText.text = "날 쓰러트리면 다시 생각해보지!";
            else bossText.text = "";
        }
        else if (BossSt == Boss_State.Die) bossText.text = "내가 졌어...\n 너 정말 강하구나!";
        else bossText.text = "";
    }

    public void Hurt()
    {
        Boss_Hp--;
    }

    void BossPatton1()
    {
        if(m_Left)
        {
            transform.localEulerAngles = new Vector3(0, -180, 0);                  
            transform.position += (transform.forward * walkSpeed * Time.deltaTime);
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
        if(BTimer%5<0.1 && Drop)    
        {   
            Drop = false;
            Patton2 = false;       
        }
        if(!Patton2)
        {
            Patton2 = true;
            RandNum = Random.Range(1, 5);   
                                            
            GameObject boxSpone1;
            GameObject boxSpone2;
            GameObject boxSpone3;

            switch (RandNum)                  
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
        if (Patton3)        
        {
            BossPatton1();
            BossPatton2();
        }
        else                
        {                                                                                                                
            Charactor.AddForce(transform.up * 5.0f * 2, ForceMode.Impulse);
            Patton3 = true;
        }
    }
}
