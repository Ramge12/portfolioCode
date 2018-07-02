using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ENEMY_STATE
{
    IDLE,
    WALK,
    Hit,
    ATTACK
}
public class Enemy : MonoBehaviour
{
    public ENEMY_STATE currState = ENEMY_STATE.IDLE;    //현재 애니메이션의 상태
    public ENEMY_STATE nextState = ENEMY_STATE.IDLE;    //다음으로 불러올 애니메이션의 상태
    public ParticleSystem DeathEffect;                  //죽는 파티클 효과

    private Animator anim;                              //애니메이터를 받아온다
    private Vector3  targetPos;                         //목표물의 위치
    private Camera   eye;                               //에너미의 시야(실제로 에너미가 보고있다)

    GameObject player;                                  //플레이어
    float AttackRange = 2.0f;                           //공격범위
    float rotationSpeed = 1.0f;                         //회전속도
    float walkSpeed = 1.0f;                             //이동속도
    bool AwayEnemy = false;                             //도망는 불값             
    //--------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();                    //애니메이터를 찾아 넣어줍니다
        eye = transform.GetComponentInChildren<Camera>();   //카메라를 이용해서 시아를 파악합니다
        AwayEnemy = false;                                  //시작할때 도망가지 않으므로 false
        ChangeCoroutine(currState);                         //코루틴 함수에 현재 값을 넣어줍니다
        DeathEffect.Stop();                                 //죽음 효과를 정지시켜둡니다
    }
    void Update()
    {
        if (!AwayEnemy)                 //캐리거가 도망가는 상태가 아닐떄
        {
            switch (currState)          //현재 상태일 때 할 행동을 업데이트합니다
            {
                case ENEMY_STATE.IDLE:
                    UpdateIdle();
                    break;
                case ENEMY_STATE.WALK:
                    UpdateWalk();
                    break;
                case ENEMY_STATE.ATTACK:
                    UpdateAttack();
                    break;
                case ENEMY_STATE.Hit:
                    UpdateHit();
                    break;

            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") & anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {   //막약 Death애니메이션이 재생중일 경우 죽음 이펙트를 활성화시키고 코루틴에 onDeath값을 넣어줍니다
                if (!DeathEffect.isPlaying) DeathEffect.Play();
                StartCoroutine("onDeath");
            }
        }
        else //도망치는 상태일 경우
        {
            transform.position += (transform.forward * -walkSpeed * Time.deltaTime);    //캐릭터는 뒷걸음질 치며
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Running") & anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {   //달리기 에니메이션이 끝날때쯤 이펙트를 재생하고 코루틴에 onDeath값을 넘겨줍니다
                if (!DeathEffect.isPlaying) DeathEffect.Play();
                StartCoroutine("onDeath");
            }
        }
    }

    public void RunAway()                       //도망가는 함수
    {
        Vector3 targetDir = targetPos - transform.position;                     //자신과 대상의 방향을 파악합니다
        targetDir.y = 0;                                                        //높이값을 0을 줍니다
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);    //Up축을 기준으로 대상을 회전시킵니다
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);//적을 회전시킵니다
        AwayEnemy = true;
        anim.SetBool("Runaway", true);                                          //도망가는 애니메이션을 실행시킵니다
    }

    void UpdateHit()                            //Hit상태일때의 업테이트 함수
    {
        if(!anim.GetBool("Death"))anim.SetBool("Death", true);  //게임의 빠른 진행을 위해 1대만 맞아도 죽도록 만들었습니다
        anim.SetBool("Hit", true);  //Hit애니메이션이 재생됩니다
    }
    void MovePatrol()                           //찾은 대상을 향해 이동합니다
    {
        Vector3 targetDir = targetPos - transform.position;             //자신과 대상의 방향을 파악합니다
        targetDir.y = 0;                                                //높이값을 0으로 둡니다
        float dist = Vector3.Distance(targetPos, transform.position);   //dist는 자신과 대상의 거리를 나타냅니다
        if (dist < AttackRange)                                         //플레이어와의 거리가 공격범위보다 낮을때
        {
            ChangeCoroutine(ENEMY_STATE.ATTACK);                        //코루틴값을 공격우로 바꾸어줍니다
            return;
        }
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);                                        //Up축을 기준으로 대상을 회전시킵니다
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  //적을 회전시킵니다
        transform.position += (transform.forward * walkSpeed * Time.deltaTime);                                     //적이 대상의 방향으로 향해 이동합니다
    }
    void UpdateIdle()                           //idle상태일때의 업데이트함수
    {
        IsFindEnemy();  //Idle상태일때는 적을 찾습니다
    }
    void UpdateWalk()                           //walk상태일때의 업데이트 함수
    {
        if (IsFindEnemy()) MovePatrol(); //대상을 찾고,이동을 시작합니다
        else MovePatrol();              //무작위로 이동합니다
    }
    bool IsFindEnemy()                          //에너미가 플레이어를 찾습니다
    {
        bool isFind = false;                                            //반환값을 설정해줍니다
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(eye);   //프로스텀 검사를 합니다...
        player = GameObject.Find("Player");                             //플레이어를 검색합니다. 없으면 false반환
        if (player == null) return false;                               //플레이어가 없다면 false를 반환하고 함수를 끝냅니다
        Bounds bounds = player.GetComponentInChildren<SkinnedMeshRenderer>().bounds;    //플레이어의 스킨드 메쉬안에있는bound를 찾습니다 bound는 collider와 같은 역할로 충돌을 검사합니다
        isFind = GeometryUtility.TestPlanesAABB(planes, bounds);        //AABB충돌을 이용하여 bouns(충돌박스)가 시야 안에 들어오는지 파악합니다
        if (isFind)                                                     //찾았다면
        {
            float dist = Vector3.Distance(targetPos, transform.position);//dist는 자신과 대상의 거리를 나타냅니다
            if (dist > 1.0f) ChangeCoroutine(ENEMY_STATE.WALK);          //거리가 1보다 멀다면 걸어갑도록 코루틴값을 바꾸어줍니다
            targetPos = player.transform.position;                      //타켓의 위치는 플레이어의 위치
        }
        return isFind;                                                  //불값을 반환합니다
    }
    void UpdateAttack()                         //Attack상태일때으 업데이트 함수
    {
        targetPos = player.transform.position;              //타켓의 포지션은 플레이어 포지션이다
        Vector3 targetDir = targetPos - transform.position; //자신과 대상의 방향을 파악합니다
        targetDir.y = 0;                                    //높이값을 0으로 둡니다
        float dist = Vector3.Distance(targetPos, transform.position);   //dist는 자신과 대상의 거리를 나타냅니다
        anim.SetBool("Attack", true);                       //공격 애니메이션을 실행합니다
        if (dist > AttackRange)                             //플레이어와의 거리가 공격범위를 벗어나는 경우
        {
            ChangeCoroutine(ENEMY_STATE.IDLE);              //Idle상태로 돌아옵니다
            return;
        }
    }
    void ChangeCoroutine(ENEMY_STATE nextState) //코루틴 값을 바꾸는 함수
    {
        StopAllCoroutines();            //현재 실행중인 모든 코루틴을 멈춥니다
        currState = nextState;          //다음 코루틴을 받아 옵니다
        anim.SetBool("Idle", false);    //애니메이션 불값을 모두 초기화시킵니다
        anim.SetBool("Walk", false);
        anim.SetBool("Damaged", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hit", false);
        switch (currState)              //현재 상태에 맞는 코루틴을 실행합니다
        {
            case ENEMY_STATE.IDLE: StartCoroutine("CoroutineIdle"); break;  
            case ENEMY_STATE.WALK: StartCoroutine("CoroutineWalk"); break;
        }
    }
    IEnumerator onDeath()                       //캐릭터가 죽거나 사라질떄
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);  //0.5초 뒤에 이 게임오브젝트를 파괴합니다
            Destroy(this.gameObject);
        }
    }
    IEnumerator CoroutineIdle()                 //idle 코루틴
    {
        anim.SetBool("Idle", true);     //idle애니메이션을 실행합니다
        nextState = ENEMY_STATE.IDLE;   //다음 상태로 Idle을 둡니다
        while (true)
        {
            yield return new WaitForSeconds(1.0f);  
            ChangeCoroutine(nextState); //1초뒤 ChangeCoroutine함수에 Idle을 넣고 실행합니다
        }
    }
    IEnumerator CoroutineWalk()                 //walk 코루틴
    {
        anim.SetBool("Walk", true);       //Walk애니메이션을 실행한다
        targetPos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
        int rand = Random.Range(0, 2);     
        switch (rand)                     //0~1둘중 무작위값을 받아 다음 행동을 결정합니다
        {
            case 0: nextState = ENEMY_STATE.IDLE; break;
            case 1: nextState = ENEMY_STATE.WALK; break;
        }
        while (true)
        {
            yield return new WaitForSeconds(1.0f);  //1초뒤 위에서 결정한 행동으로 코루틴을 바꿉니다
            ChangeCoroutine(nextState);
        }
    }
}