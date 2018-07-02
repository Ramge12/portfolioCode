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
    public GameObject playerRandCheck;          //플레이어가 땅에 충돌했는지 체크하는 부분 다리역할
    public GameObject DoubleJump_Attack_Target; //더블 점프 후 공격할 대상    
    public GameObject Apple;                    //플레이어가 라이딩 할 때 필요한 아이템 (사과)
    public CapsuleCollider playerColliderBody;  //플레이어 몸의 콜라이터 (충돌 체크용)
    public ParticleSystem DustSmoke;            //파티클 발에서 나는연기
    public ParticleSystem JumpEffect;           //점프 이펙트
    public ParticleSystem RandEffect;           //착지 이펙트

    public float moveSpeed = 1.0f;              //플레이어 이동속도
    public float turnSpeed = 1.0f;              //플레이어 회전속도
    public float jumpPower = 1.0f;              //플레이어 점프력
    public bool onWall = false;                 //벽이 있는지 체크하는 불값
    public bool MovePlayerFoward = true;        //벽이 있다면 앞으로 갈수있고 없으면 못가게 하는 불값        
    public bool Riding;                         //라이딩 상태인지 아닌지 불값
    public bool Rhino_here;                     //근처에 코뿔소가 있는지 확인하는 불값
    public bool DubleJumpAttack = false;        //더블 점프 어택으로 충돌시 데미지가 한번만 들어가도록 하는 불값
    public int StarPoint = 0;                   //별획득 점수
    public int PlayerHP = 4;                    //플레이어의 체력

    Vector3 lookDir;                            //플레이어가 바라보는 방향
    Animator player_Ani;                        //플레이어 애니메이션
    Rigidbody player_rigid;                     //플레이어 리지드바디
    GameObject ItemClock;                       //시계 아이템

    bool Run_Charator = false;                  //캐릭터가 뛰는지 확인 불값
    bool Jump_Charactor = false;                //캐릭터가 점프하는지 확인 불값
    bool HurtCharactor = false;                 //캐릭터가 공격당하는지 확인 불값
    bool Death;                                 //캐릭터가 죽었는지 확인 불값
    float DeathTimer;                           //죽고나서 시간을 재는 타이머 일정시간이 지나면 엔딩씬으로    

    //----------------------------------------------------------------------------------------------------------

    public void Hurt()                          //플레이어 피격함수
    {
        if (!HurtCharactor)                         //플레이어가 hurt상태가 아니라면
        {
            playSound("Hurt");                      //사운드출력
            HurtCharactor = true;                   //Hurt상태 불값 true
            player_Ani.SetBool("Hit", true);        //hit애니메이션 재생
            player_Ani.SetBool("Attack", false);    //공격중에 피격당할 수 있으므로 attack초기화
            ps_State = PlayerState.Hit;             //상태를 Hit상태로 바꾸고
            PlayerHP--;                             //플레이어 HP를 감소시킨다
        }
    }
    public void HangOutPlayer()                     //플레이어가 벽에서 매달리는걸 벗어날때
    {
        player_Ani.SetBool("Hang", false);
        player_Ani.SetBool("Jump", true);
        ps_State = PlayerState.JUMP;                //애니메이션값을 해제해주고 점프를 통해 벗어나기 때문에 상태를 JUMP로 바꾼다
    }
    public void CharactorFallDown()                 //캐릭터가 폭탄에 맞아서 떨어지는 부분
    {
        if (!player_Ani.GetBool("FallDown"))        //플레이어 애니메이션FallDown이 재생중이 아닐때
        {
            playSound("Drop");                      //Drop사운드를 재생하고
            player_Ani.SetBool("FallDown", true);   //플레이어 애니메이션FallDown을 재생한다
            ps_State = PlayerState.FallDown;        //플레이어 상태는 FallDonw으로 바꾼다
            PlayerHP--;                             //HP가 1깍인다
            player_rigid.AddForce(new Vector3(0, -1, 0) * jumpPower * 2, ForceMode.Impulse);  //플레이어는 아래로 떨어지도록 AddForce를 준다
        }
    }
    public void ItemGet(GameObject Item)        //아이템 획득함수
    {
        if (Item.tag == "Item")                     //아이템(시계)일 경우
        {
            playSound("Clock");
            player_Ani.SetBool("ITem", true);       //아이템 획득애니메이션 재생
            ItemClock = Item;                       //획득한 아이템 정보를 받아 게임에서 지웁니다
            ps_State = PlayerState.ITem_GET;
        }
        else
        {
            playSound("Star");
            StarPoint += 10;                        //스타 점수 획득
            ItemClock = Item;
            Destroy(ItemClock);
        }
    }
    public void HangPlayer(float player_Y)      //플레이어가 매달릴떄
    {
        //플레이어 위치에 따라 매달리는 방향을 결정하는 방식
        player_rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;   //플레이어는 X,Y축으로 움직이 못학 고정하고 회전하지않는다
        player_Ani.SetBool("Hang", true);
        player_Ani.SetBool("DoubleJump", false);    //애니메이션값을 준다
        if (gameObject.transform.localEulerAngles.y >= 45 && gameObject.transform.localEulerAngles.y < 135) gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);//건물 정면
        else if (gameObject.transform.localEulerAngles.y >= 135 && gameObject.transform.localEulerAngles.y < 225) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);//건물 정면
        else if (gameObject.transform.localEulerAngles.y >= 225 && gameObject.transform.localEulerAngles.y < 315) gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);//건물 정면
        else gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);//건물 정면
        ps_State = PlayerState.Hang;    //플레이어 상태는 매달림 상태가 된다
    }
    public void Ride_Rhino(GameObject Rhino)    //라이딩함수
    {
        if (Input.GetKeyDown(KeyCode.R) && Rhino_here && Apple.activeSelf)   //apple있고, 코뿔소가 근처에있을때 R키를 누르면
        {
            if (!Riding)//탑승한 상태가 아니라면 탑승한다
            {
                Riding = true;                      //Riding을 true로
                player_Ani.SetBool("Drive", true);  //Drive애니메이션 재생
                ps_State = PlayerState.Ride;        //Ride상태로 둔다
            }
            else //이미 탑승한 상태이면 내린다
            {
                Riding = false;                     //Riding을 false로
                player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);    //내릴떄는 점프를 한다
                player_Ani.SetBool("Drive", false); //드라이브 애니메이션을 재생하지않는다
                ps_State = PlayerState.IDLE;        //IDLE상태로 돌아온다
            }
        }
        if (Riding)  //탑승할 상태일 경우
        {
            transform.localEulerAngles = Rhino.transform.localEulerAngles;              //플레이어의 회전값은 코뿔소의 회전값과 같다
            transform.position = Rhino.transform.position + new Vector3(0, 6.5f, 0);    //플레이어의 포지션을 코뿔소 등위치(6.5f)로 고정한다
        }
    }
    public void WallJump(GameObject Wall, Vector3 WallNormal)   //벽점프 계산 함수
    {
        Vector3 ColliderNomal = WallNormal;                     //충돌한 벽의 노말 벡터를 가져옵니다
        Vector3 playerVec = transform.forward;                  //플레이어가 바라보는 방향의 벡터    
        playerVec = playerVec.normalized;                       //벡터를 정규화시킵니다
        //Vector3 reflectVector = Vector3.Reflect(playerVec, ColliderNomal);
        //reflectVector = reflectVector.normalized;
        float replect = GetAngle(playerVec, ColliderNomal);     //반사각을 구합니다
        transform.Rotate(0, 180 - 2 * replect, 0);              //반사각에 따라 플레이어를 회전시킵니다
        ps_State = PlayerState.JUMP;                            //플레이어상태를 점프로 바꿉니다
    }

    void Start()
    {
        PlayerHP = PlayerPrefs.GetInt("HP");        //플레이어의 HP값을 프리펩에서 가져온다(매씬마다 이전씬에서 저장한 값을 가져올수 있다)
        player_Ani = GetComponent<Animator>();      //플레이어 애니메이션을 가져온다
        player_rigid = GetComponent<Rigidbody>();   //플레이어 리지드바디를 가져온다
        ps_State = PlayerState.IDLE;                //플레이어 상태를 IDLE로 둔다
        DeathTimer = 0f;                            //플레이어가 죽고나서 재는 시간을 0으로 둔다(죽고나서 재생)
        Death = false;                              //플레이어가 죽지않았으므로 false를 준다
        Riding = false;                             //아무것도 타지않았으므로 false를 준다   
        Rhino_here = false;                         //근처에 코뿔소가 없으므로 false를 준다
        DustSmoke.Stop();                           //모든 파티클을 멈춰둔다
        JumpEffect.Stop();
        RandEffect.Stop(); 
    }
    void Update()
    {
        if (!Death) //플레이어가 죽지 않았다면
        {
            if (ps_State != PlayerState.Attack && ps_State != PlayerState.FallDown && ps_State != PlayerState.Ride)//Player가 Attack, FallDown, Ride상태일때는 움직일수없다
            {
                if (SceneManager.GetActiveScene().name == "BigBoss")    //마지막 보스씬에서는
                {
                    BossMoveCharactor();    //플레이어의 움직임이 바뀌기 때문에
                    player_rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;  //회전과 X축 이동을 고정한다

                }
                else MoveCharactor();   //나머지 씬에서는 이 함수를 써서 움직인다
            }
            if (ps_State != PlayerState.Hit && ps_State != PlayerState.FallDown) Attack();          //FallDonw, Hit상태가 아니면 공격할 수 있다
            if (ps_State != PlayerState.Attack && ps_State != PlayerState.Ride) JumpCharactor();    //공격, 라이딩 상태가 아니면 점프 할 수 있다 
            AnimationCheck();   //애니메이션 체크함수
            Jump_attack();      //점프 공격에서 사용하는 함수
        }
        else //플레이어가 죽었다면
        {
            DeathTimer += Time.deltaTime;   //다음씬을 재생하기 위한 타이머를 재생하고 
            if(DeathTimer>4.0f)             //4초가 넘어가면
            {
                PlayerPrefs.SetInt("HP", 4);    //플레이어 HP를 다시 4로 만들고
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //다음씬으로 넘어간다
            }
        }
        DiePlayer();    //HP를 체크하여 죽었는지 판단한다
    }

    void AnimationCheck()                       //플레이어 애니메이션 체크함수
    {
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("ItemGet") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {   //아이템 애니메이션 재생이 일정 이상 되면 애니메이션을 멈추고 idle상태로 돌아온다.
            Destroy(ItemClock); //아이템을 게임에서 제거합니다
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
            player_Ani.SetBool("FallDown", false);  //점프중에 떨어질수 있어 모든 불값 초기화
            player_Ani.SetBool("Run", false);
            player_Ani.SetBool("Jump", false);
            player_Ani.SetBool("DoubleJump", false);
        }
    }
    void Attack()                               //플레이어 공격 함수
    {
        if (Input.GetKey(KeyCode.Tab) && 
            ps_State != PlayerState.JUMP && ps_State != PlayerState.JUMP_START && ps_State != PlayerState.JUMP_END
            && ps_State != PlayerState.RUN_JUMP && ps_State != PlayerState.RUN_JUMP_START && ps_State != PlayerState.RUN_JUMP_END && ps_State != PlayerState.DOUBLE_JUMP)
        {   //플레이어가 위와 같은 상태가 아닐때 tab키를 눌러서 공격
            ps_State = PlayerState.Attack;      //플레이어 상태attak
            player_Ani.SetBool("Attack", true); //애니메이션 attack 에 true값을 줍니다
            playSound("Attack");                //attack사운드 재생
        }
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("AttackCharactor") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
        {   //어택 애니메이션을 재생하다 일정 이상 재생하면 idle로 돌아옵니다
            ps_State = PlayerState.IDLE;
            player_Ani.SetBool("Attack", false);
        }
    }
    void Jump_attack()                          //플레이어 점프 어택
    {
        DoubleJump_Attack_Target = GameObject.Find("HitPoint"); //가까이 있는 HitPoint 오브젝트를 찾습니다
        if (DoubleJump_Attack_Target)                   //타겟이 있다면
        {
            float dist = Vector3.Distance(DoubleJump_Attack_Target.transform.position, transform.position); //타겟까지의 거리를 잽니다
            if (ps_State == PlayerState.DOUBLE_JUMP && DubleJumpAttack)    //플레이어 상태가 더블점프 상태일때
            {
                if (Input.GetKeyDown(KeyCode.Q))         //Q버튼을 눌러서 공격
                {
                    playSound("JumpAttack");             //점프 공격사운드 재생
                    ps_State = PlayerState.Jump_Attack;  //플레이어 상태는 점프 어택 상태
                    JumpEffect.Play();                   //이펙트를 한번 재생한다
                    Vector3 targetDir = DoubleJump_Attack_Target.transform.position - transform.position;    //타겟까지의 거리를 구한다
                    player_rigid.AddForce(targetDir * jumpPower / 2, ForceMode.Impulse);                     //타겟의 방향으로 AddForce를 준다
                }
            }
        }
    }
    void DiePlayer()                            //플레이어가 죽었는지 체크함수
    {
        if (PlayerHP <= 0)                              //플레이어 HP가 0아래로 내려가면
        {
            if (!Death) player_Ani.SetTrigger("Die");   //Die애니메이션을 재생하고
            Death = true;                               //Death를 true로 바꾼다
        }
    }
    void JumpCharactor()                        //캐릭터 점프 함수
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            ps_State != PlayerState.JUMP_START &&
            ps_State != PlayerState.JUMP_END &&
            Jump_Charactor == false)
        {   //위와 같은 상태가 아닐때 space를 누를경우
            if (ps_State == PlayerState.Hang) player_rigid.AddForce(transform.forward * jumpPower * 2, ForceMode.Impulse);  //매달려있는 상태에서는 바로 점프한다
            player_rigid.constraints = RigidbodyConstraints.FreezeRotation;//점프 중에 리지드 바디가 못움직이도록합니다
            Jump_Charactor = true;
            playSound("Jump");                          //점프 사운드 출력
            StartCoroutine("onRand_Check");             //onRand_Check코루틴을 실행한다
            playerColliderBody.enabled = false;         //리짇 바디를 잠시 꺼둔다
            if (ps_State == PlayerState.JUMP)           //이미 점프중일경우
            {
                player_Ani.SetBool("Hit", false);
                player_Ani.SetBool("Attack", false);
                player_Ani.SetBool("DoubleJump", true); //애니메이션 값 초기화
                ps_State = PlayerState.DOUBLE_JUMP;     //상태변경
                playSound("DoubleJump");                //더블점프 사운드 출력
                JumpEffect.Play();                      //점프이펙트 출력
                player_rigid.AddForce(transform.up * jumpPower * 2, ForceMode.Impulse); //점프력으 2배 가해 이단 점프를 한다
            }
            else if (ps_State != PlayerState.JUMP && ps_State != PlayerState.DOUBLE_JUMP)
            {   //점프 상태도 아니고 이단점프 상태도 아닌 상태일때 점프할경우
                ps_State = PlayerState.JUMP;
                player_Ani.SetBool("Jump", true);
                player_rigid.constraints = RigidbodyConstraints.FreezeRotation;//점프 중에 리지드 바디가 못움직이도록합니다
                if (onWall) player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);  //벽에 닿은 상태에서 점프할 경우
                else player_rigid.AddForce(transform.up * jumpPower, ForceMode.Impulse);        //그냥 점프할 경우
            }
        }
        if (player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Double_Jump") & player_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {   //더블 점프 애니메이션 체크 , 일정이상 진행하면 애니메이션을 멈춘다
            player_Ani.SetBool("DoubleJump", false);
        }
        if (ps_State == PlayerState.JUMP ||
        ps_State == PlayerState.DOUBLE_JUMP || ps_State == PlayerState.Jump_Attack)
        {   //점프, 더블점프, 점프 어택이 끝나고
            if (!Jump_Charactor && playerRandCheck.GetComponent<RandCheck>().onRand)
            {   //플레이어가 땅에 충돌하는 경우
                moveSpeed = 5.0f;                       //이동속도는 원래대로
                player_Ani.SetBool("Jump", false);      //애니메이션 초기화
                player_Ani.SetBool("DoubleJump", false);
                if (ps_State == PlayerState.Jump_Attack) RandEffect.Play();  //착지 이펙트 재생
                ps_State = PlayerState.IDLE;            //상태는 IDLE로
                player_Ani.speed = 1.0f;                //애니메이션 스피드 1.0f로
                playerColliderBody.enabled = true;      //콜라이더 활성화
                player_rigid.constraints = RigidbodyConstraints.FreezeRotation;//리지드 바디가 못움직이도록합니다
            }
        }
    }
    void MoveCharactor()                        //캐릭터 움직임 함수
    {
        float xx = Input.GetAxisRaw("Vertical");    
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;    //vertical과 horizontal값을 이용해 방향을 구한다
        if (Input.GetKey(KeyCode.W) && MovePlayerFoward )       //W키를 누르고 플레이어 앞에 벽이 없을 경우
        {
            if (ps_State != PlayerState.JUMP_END && ps_State != PlayerState.Hang && ps_State != PlayerState.ITem_GET)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                //플레이어는 lookdir방향으로 이동한다
            }
            player_Ani.SetBool("Run", true);        //Run 애니메이션 재생
            playSounding("footWalk");               //발소리 재생
            Run_Charator = true;                    //Run_charactor 에 true값을 준다
            if (DustSmoke && DustSmoke.isPlaying == false) DustSmoke.Play(true);    //파티클재생
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            //플레이어가 뒤로 움직인다
            player_Ani.SetBool("Run", true);
            Run_Charator = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {   // W혹은 S를 뗏을 때
            moveSpeed = 5.0f;
            player_Ani.SetBool("Run", false);       //애니메이션을 종료한다
            player_Ani.SetFloat("Move", 0f);        //좌우 블렌더 트리의 값을 초기화 한다   
            Run_Charator = false;
            if (DustSmoke)DustSmoke.Stop();         //파티클을 멈춘다
        }
        if (!player_Ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))DustSmoke.Stop();  //Run애니메이션이 재생중이 아니면 dustSmoke파티클을 멈춘다
        if (Input.GetKey(KeyCode.A))    
        {
            player_Ani.SetFloat("Move", -1f);       //좌우 달리기 값을 판정하는 블렌더 트리 값에 -1을 준다 (좌측 달리기로 변경)
            if (ps_State == PlayerState.Hang)       //플레이어가 매달려 있는 상태라면
            {
                moveSpeed = 2.0f;              
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //2의 속도로 움직인다
                player_Ani.SetFloat("Move", -1f);
            }
            else transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f); //매달려있지않다면 플레이어를 좌측으로 회전시킨다
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_Ani.SetFloat("Move", 1f);        //좌우 판정을 하는 블렌더 트리값에 1을 줘서 오른쪽달리기를 한다
            if (ps_State == PlayerState.Hang)       //매달려있다면
            {
                moveSpeed = 2.0f;
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);    //2의속도로 움직인다
                player_Ani.SetFloat("Move", 1f);
            }
            else transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f); //매달려있지않다면 오른쪽으로 회전한다
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {   // D 또는 A키를 떼면 블렌더 트리의 move값을 초기화한다
            player_Ani.SetFloat("Move", 0f);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {   //시프트키를 통해 기어가는 애니메이션을 재생
            moveSpeed = 1.0f;
            player_Ani.SetBool("Crawl", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {   //시프트 키를 떼면 기어가는 애니메이션이 끝난다
            moveSpeed = 5.0f;
            player_Ani.SetBool("Crawl", false);
        }
    }
    void BossMoveCharactor()                    //마지막 보스 캐릭터 움직임 함수
    {   //마지막 보스 씬에서 플레이어 움직임입니다 일반적인 부분은 똑같지만 이 맵에서는
        // 좌우로만 움직일 수 있도록 만들었습니다
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;
        if (Input.GetKey(KeyCode.A) && MovePlayerFoward)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);  //왼쪽을 바라보도록 한다
            if (ps_State != PlayerState.JUMP_END && ps_State != PlayerState.Hang && ps_State != PlayerState.ITem_GET) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            player_Ani.SetBool("Run", true);
            player_Ani.SetFloat("Direction", 1f);
            Run_Charator = true;
            if (DustSmoke && DustSmoke.isPlaying == false) DustSmoke.Play(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);    //오른쪽을 바라보게 한다
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
    IEnumerator onRand_Check()                  //착지할 떄에 사용하는함수
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);  //착지후 0.1f초 후부터 jump_charactor를 false로 바꾸어준다
            Jump_Charactor = false;
        }
    }
    void playSound(string snd)                  //플레이어 사운드 재생(한번재생)
    {
        GameObject.Find(snd).GetComponent<AudioSource>().Play();//오디오 소스 Play()함수를 사용
    }
    void playSounding(string snd)               //플레이어 사운드 재생(반복재생)
    {
        if (!GameObject.Find(snd).GetComponent<AudioSource>().isPlaying) GameObject.Find(snd).GetComponent<AudioSource>().Play();    //오디오 소스를 체크하여 false상태이며 true로 바꾸어 반복재생한다
    }
    float GetAngle(Vector3 vec1, Vector3 vec2)  //두 벡터의 사이각을 구하는 함수
    {
        float theta = Vector3.Dot(vec1, vec2) / (vec1.magnitude * vec2.magnitude); //벡터의 내적을 두 벡터의 크기의 곱으로 나눕니다
        Vector3 dirAngle = Vector3.Cross(vec1, vec2);       //벡터의 외적을 구합니다
        float angle = Mathf.Acos(theta) * Mathf.Rad2Deg;    //라디안 값을 변화시켜 의 역함수 acos(theta)를 곱합니다
        if (dirAngle.z < 0.0f) angle = 360 - angle;
        if (dirAngle.z > 360f) angle = angle - 360;         //최대 최소값을 넘지않도록 보정해줍니다
        return angle;
    }
}
