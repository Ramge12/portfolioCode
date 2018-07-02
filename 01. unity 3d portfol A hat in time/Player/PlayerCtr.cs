using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState //플레이어 상태문
{
    IDLE,
    JUMP_START,
    JUMP,
    JUMP_END,
    DOUBLE_JUMP,
    RUN_JUMP_START,
    RUN_JUMP,
    RUN_JUMP_END,
    Hang,
    ITem_GET,
    Attack,
    Hit,
    Jump_Attack,
    FallDown,
    Ride
}

public class PlayerCtr : MonoBehaviour
{
    public PlayerState ps_State;
    public GameObject playerRandCheck;              //플레이어가 땅에 충돌했는지 체크하는 부분 다리역할
    public GameObject DoubleJump_Attack_Target;     //더블 점프 후 공격할 대상    
    public GameObject Apple;                        //플레이어가 라이딩 할 때 필요한 아이템 (사과)
    public CapsuleCollider playerColliderBody;      //플레이어 몸의 콜라이터 (충돌 체크용)
    public ParticleSystem DustSmoke;            
    public ParticleSystem JumpEffect;           
    public ParticleSystem RandEffect;                                                   

    public float moveSpeed = 1.0f;             
    public float turnSpeed = 1.0f;             
    public float jumpPower = 1.0f;             
    public bool onWall = false;                
    public bool MovePlayerFoward = true;       
    public bool Riding;                        
    public bool Rhino_here;                    
    public bool DubleJumpAttack = false;       
    public int StarPoint = 0;                  
    public int PlayerHP = 4;                                                                     

    Vector3 lookDir;                            
    Animator player_Ani;                        
    Rigidbody player_rigid;                     
    GameObject ItemClock;                       

    bool Run_Charator = false;                  
    bool Jump_Charactor = false;                
    bool HurtCharactor = false;                 
    bool Death;                                 
    float DeathTimer;                            

    //----------------------------------------------------------------------------------------------------------

    public void Hurt()               
    {
        if (!HurtCharactor)                       
        {
            playSound("Hurt");                     
            HurtCharactor = true;                  
            player_Ani.SetBool("Hit", true);       
            player_Ani.SetBool("Attack", false);   
            ps_State = PlayerState.Hit;            
            PlayerHP--;                                                 
        }
    }

    public void HangOutPlayer()//플레이어가 벽에서 매달리는걸 벗어날때
    {
        player_Ani.SetBool("Hang", false);
        player_Ani.SetBool("Jump", true);
        ps_State = PlayerState.JUMP;                
    }

    public void CharactorFallDown()//캐릭터가 폭탄에 맞아서 떨어지는 부분
    {
        if (!player_Ani.GetBool("FallDown"))        
        {
            playSound("Drop");                      
            player_Ani.SetBool("FallDown", true);   
            ps_State = PlayerState.FallDown;        
            PlayerHP--;                                                              
            player_rigid.AddForce(new Vector3(0, -1, 0) * jumpPower * 2, ForceMode.Impulse);  //플레이어는 아래로 떨어지도록
        }
    }

    public void ItemGet(GameObject Item)
    {
        if (Item.tag == "Item")                   
        {
            playSound("Clock");
            player_Ani.SetBool("ITem", true);     
            ItemClock = Item;                     
            ps_State = PlayerState.ITem_GET;
        }
        else
        {
            playSound("Star");
            StarPoint += 10;                      
            ItemClock = Item;                                                                      
            Destroy(ItemClock);
        }
    }

    public void HangPlayer(float player_Y)//플레이어가 매달릴떄
    {
        //플레이어 위치에 따라 매달리는 방향을 결정하는 방식
        player_rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;  
        player_Ani.SetBool("Hang", true);
        player_Ani.SetBool("DoubleJump", false);    
        if (gameObject.transform.localEulerAngles.y >= 45 && gameObject.transform.localEulerAngles.y < 135) gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (gameObject.transform.localEulerAngles.y >= 135 && gameObject.transform.localEulerAngles.y < 225) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (gameObject.transform.localEulerAngles.y >= 225 && gameObject.transform.localEulerAngles.y < 315) gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        else gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        ps_State = PlayerState.Hang;  
    }

    public void Ride_Rhino(GameObject Rhino)
    {
        if (Input.GetKeyDown(KeyCode.R) && Rhino_here && Apple.activeSelf)   
        {
            if (!Riding)
            {
                Riding = true;                      
                player_Ani.SetBool("Drive", true);  
                ps_State = PlayerState.Ride;                
            }
            else 
            {
                Riding = false;                    
                player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);   
                player_Ani.SetBool("Drive", false); 
                ps_State = PlayerState.IDLE;                           
            }
        }
        if (Riding)  
        {
            transform.localEulerAngles = Rhino.transform.localEulerAngles;              
            transform.position = Rhino.transform.position + new Vector3(0, 6.5f, 0);      
        }
    }

    public void WallJump(GameObject Wall, Vector3 WallNormal)   //벽점프 계산 함수
    {
        Vector3 ColliderNomal = WallNormal;                     //충돌한 벽의 노말 벡터를 가져옵니다
        Vector3 playerVec = transform.forward;                  //플레이어가 바라보는 방향의 벡터    
        playerVec = playerVec.normalized;                       //벡터를 정규화시킵니다
        float replect = GetAngle(playerVec, ColliderNomal);     //반사각을 구합니다
        transform.Rotate(0, 180 - 2 * replect, 0);              //반사각에 따라 플레이어를 회전시킵니다
        ps_State = PlayerState.JUMP; 
    }

    void Start()
    {
        PlayerHP = PlayerPrefs.GetInt("HP");        
        player_Ani = GetComponent<Animator>();      
        player_rigid = GetComponent<Rigidbody>();   
        ps_State = PlayerState.IDLE;                
        DeathTimer = 0f;                            
        Death = false;                              
        Riding = false;                             
        Rhino_here = false;                         
        DustSmoke.Stop();                                                                                          
        JumpEffect.Stop();
        RandEffect.Stop(); 
    }

    void Update()
    {
        if (!Death) 
        {
            if (ps_State != PlayerState.Attack && ps_State != PlayerState.FallDown && ps_State != PlayerState.Ride)
            {
                if (SceneManager.GetActiveScene().name == "BigBoss") 
                {
                    BossMoveCharactor();   
                    player_rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX; 

                }
                else MoveCharactor();   
            }

            if (ps_State != PlayerState.Hit && ps_State != PlayerState.FallDown) Attack();        
            if (ps_State != PlayerState.Attack && ps_State != PlayerState.Ride) JumpCharactor();  

            AnimationCheck(); 
            Jump_attack();      
        }
        else 
        {
            DeathTimer += Time.deltaTime;   
            if(DeathTimer>4.0f)                                              
            {
                PlayerPrefs.SetInt("HP", 4);    
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            }
        }
        DiePlayer();   
    }

    void AnimationCheck()                  
    {
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("ItemGet") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {   //아이템 애니메이션 재생이 일정 이상 되면 애니메이션을 멈추고 idle상태로 돌아온다.
            Destroy(ItemClock); 
            player_Ani.SetBool("ITem", false);
            ps_State = PlayerState.IDLE;
        }
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Hit") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.65f)
        {   //플레이 애니메이션이 일정이상 진행되면 애니메이션을 종료하고 IDLE상태로 돌아옵니다
            HurtCharactor = false;
            ps_State = PlayerState.IDLE;
            player_Ani.SetBool("Hit", false);
        }
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("FallDown") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
        {   //플레이 애니메이션이 일정이상 진행되면 애니메이션을 종료하고 IDLE상태로 돌아옵니다
            ps_State = PlayerState.IDLE;
            player_Ani.SetBool("FallDown", false);  
            player_Ani.SetBool("Run", false);
            player_Ani.SetBool("Jump", false);
            player_Ani.SetBool("DoubleJump", false);
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.Tab) && 
            ps_State != PlayerState.JUMP && ps_State != PlayerState.JUMP_START && ps_State != PlayerState.JUMP_END
            && ps_State != PlayerState.RUN_JUMP && ps_State != PlayerState.RUN_JUMP_START && ps_State != PlayerState.RUN_JUMP_END && ps_State != PlayerState.DOUBLE_JUMP)
        {  
            ps_State = PlayerState.Attack;      
            player_Ani.SetBool("Attack", true); 
            playSound("Attack");                                     
        }
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("AttackCharactor") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
        {   //어택 애니메이션을 재생하다 일정 이상 재생하면 idle로 돌아옵니다
            ps_State = PlayerState.IDLE;
            player_Ani.SetBool("Attack", false);
        }
    }

    void Jump_attack()
    {
        DoubleJump_Attack_Target = GameObject.Find("HitPoint");
        if (DoubleJump_Attack_Target)
        {
            float dist = Vector3.Distance(DoubleJump_Attack_Target.transform.position, transform.position);
            if (ps_State == PlayerState.DOUBLE_JUMP && DubleJumpAttack) 
            {
                if (Input.GetKeyDown(KeyCode.Q))        
                {
                    playSound("JumpAttack");             
                    ps_State = PlayerState.Jump_Attack;  
                    JumpEffect.Play();                           
                    Vector3 targetDir = DoubleJump_Attack_Target.transform.position - transform.position;    //타겟까지의 거리를 구한다
                    player_rigid.AddForce(targetDir * jumpPower / 2, ForceMode.Impulse);                     //타겟의 방향으로 AddForce를 준다
                }
            }
        }
    }

    void DiePlayer()                            
    {
        if (PlayerHP <= 0)                             
        {
            if (!Death) player_Ani.SetTrigger("Die");  
            Death = true;                                         
        }
    }

    void JumpCharactor()        
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            ps_State != PlayerState.JUMP_START &&
            ps_State != PlayerState.JUMP_END &&
            Jump_Charactor == false)
        {  
            if (ps_State == PlayerState.Hang) player_rigid.AddForce(transform.forward * jumpPower * 2, ForceMode.Impulse); 
            player_rigid.constraints = RigidbodyConstraints.FreezeRotation;

            Jump_Charactor = true;
            playSound("Jump");                         
            StartCoroutine("onRand_Check");            
            playerColliderBody.enabled = false;        

            if (ps_State == PlayerState.JUMP)                       
            {
                player_Ani.SetBool("Hit", false);
                player_Ani.SetBool("Attack", false);
                player_Ani.SetBool("DoubleJump", true); 
                ps_State = PlayerState.DOUBLE_JUMP;     
                playSound("DoubleJump");                
                JumpEffect.Play();                                   
                player_rigid.AddForce(transform.up * jumpPower * 2, ForceMode.Impulse); 
            }

            else if (ps_State != PlayerState.JUMP && ps_State != PlayerState.DOUBLE_JUMP)
            {   
                ps_State = PlayerState.JUMP;
                player_Ani.SetBool("Jump", true);
                player_rigid.constraints = RigidbodyConstraints.FreezeRotation;

                if (onWall) player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);
                else player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);                          
            }
        }

        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Double_Jump") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {   //더블 점프 애니메이션 체크 , 일정이상 진행하면 애니메이션을 멈춘다
            player_Ani.SetBool("DoubleJump", false);
        }

        if (ps_State == PlayerState.JUMP ||
        ps_State == PlayerState.DOUBLE_JUMP || ps_State == PlayerState.Jump_Attack)
        {  
            if (!Jump_Charactor && playerRandCheck.GetComponent<RandCheck>().onRand)
            {  
                moveSpeed = 5.0f;                      
                player_Ani.SetBool("Jump", false);     
                player_Ani.SetBool("DoubleJump", false);
                if (ps_State == PlayerState.Jump_Attack) RandEffect.Play();  
                ps_State = PlayerState.IDLE;          
                player_Ani.speed = 1.0f;              
                playerColliderBody.enabled = true;                
                player_rigid.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void MoveCharactor()         
    {
        float xx = Input.GetAxisRaw("Vertical");    
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;   

        if (Input.GetKey(KeyCode.W) && MovePlayerFoward )                     
        {
            if (ps_State != PlayerState.JUMP_END && ps_State != PlayerState.Hang && ps_State != PlayerState.ITem_GET)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            player_Ani.SetBool("Run", true);       
            playSounding("footWalk");              
            Run_Charator = true;
                                  
            if (DustSmoke && DustSmoke.isPlaying == false) DustSmoke.Play(true);    
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            player_Ani.SetBool("Run", true);
            Run_Charator = true;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {  
            moveSpeed = 5.0f;
            player_Ani.SetBool("Run", false);     
            player_Ani.SetFloat("Move", 0f);      
            Run_Charator = false;
            if (DustSmoke)DustSmoke.Stop();       
        }                                                                                    

        if (!player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))DustSmoke.Stop(); 

        if (Input.GetKey(KeyCode.A))    
        {
            player_Ani.SetFloat("Move", -1f);      
            if (ps_State == PlayerState.Hang)                                                
            {
                moveSpeed = 2.0f;              
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); 
                player_Ani.SetFloat("Move", -1f);
            }
            else transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f); 
        }

        if (Input.GetKey(KeyCode.D))
        {
            player_Ani.SetFloat("Move", 1f);        

            if (ps_State == PlayerState.Hang)                                                         
            {
                moveSpeed = 2.0f;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);    
                player_Ani.SetFloat("Move", 1f);
            }
            else transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f); 
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        { 
            player_Ani.SetFloat("Move", 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {   
            moveSpeed = 1.0f;
            player_Ani.SetBool("Crawl", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {   
            moveSpeed = 5.0f;
            player_Ani.SetBool("Crawl", false);
        }
    }

    void BossMoveCharactor()//마지막 보스 캐릭터 움직임 함수
    {   
        //마지막 보스 씬에서 플레이어 움직임입니다 일반적인 부분은 똑같지만 이 맵에서는
        // 좌우로만 움직일 수 있도록 만들었습니다

        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;

        if (Input.GetKey(KeyCode.A) && MovePlayerFoward)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); 
            if (ps_State != PlayerState.JUMP_END && ps_State != PlayerState.Hang && ps_State != PlayerState.ITem_GET) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            player_Ani.SetBool("Run", true);
            player_Ani.SetFloat("Direction", 1f);
            Run_Charator = true;
            if (DustSmoke && DustSmoke.isPlaying == false) DustSmoke.Play(true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);  
            if (ps_State != PlayerState.JUMP_END && ps_State != PlayerState.Hang && ps_State != PlayerState.ITem_GET) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            player_Ani.SetBool("Run", true);
            player_Ani.SetFloat("Direction", 1f);
            Run_Charator = true;
            if (DustSmoke && DustSmoke.isPlaying == false) DustSmoke.Play(true);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moveSpeed = 5.0f;
            player_Ani.SetBool("Run", false);
            player_Ani.SetFloat("Direction", 0f);
            player_Ani.SetFloat("Move", 0f);
            Run_Charator = false;
            if (DustSmoke) DustSmoke.Stop();
        }

        if (!player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Run")) DustSmoke.Stop();
    }

    IEnumerator onRand_Check()               
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);  //착지후 0.1f초 후부터 jump_charactor를 false로 바꾸어준다
            Jump_Charactor = false;
        }
    }

    void playSound(string snd)                 
    {
        GameObject.Find(snd).GetComponent<AudioSource>().Play();
    }

    void playSounding(string snd)             
    {
        if (!GameObject.Find(snd).GetComponent<AudioSource>().isPlaying) GameObject.Find(snd).GetComponent<AudioSource>().Play();   
    }

    float GetAngle(Vector3 vec1, Vector3 vec2)  //두 벡터의 사이각을 구하는 함수
    {
        float theta = Vector3.Dot(vec1, vec2) / (vec1.magnitude * vec2.magnitude); 
        Vector3 dirAngle = Vector3.Cross(vec1, vec2);    
        float angle = Mathf.Acos(theta) * Mathf.Rad2Deg; 
        if (dirAngle.z < 0.0f) angle = 360 - angle;
        if (dirAngle.z > 360f) angle = angle - 360;                      
        return angle;
    }
}
