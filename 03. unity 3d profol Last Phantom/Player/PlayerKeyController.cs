using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKeyController : MonoBehaviour {

    [Header("PlayerValue")]
    [SerializeField] private float climmingPower = 0f;
    [SerializeField] private CharactorMoveValue charactorMoveValue = new CharactorMoveValue();

    [Header("PlayerScripts")]
    [SerializeField] private BikeSystem bikeSystem;
    [SerializeField] private PlayerUI playerUI;

    [System.NonSerialized] public Vector3 moveVector;         
    [System.NonSerialized] public bool stopPlayer = false;
    [System.NonSerialized] public float unGravity = 0.0f;
    [System.NonSerialized] public float magnification = 1.0f;

    private PlayerStats playerStats;
    private CharacterController charactorController;

    private bool onColliding = false;
    private bool wallClimming = false;
    private float hangHeight = 0;
    private float gravityMax = -20;
    private Transform playerTransform;
    private PlayerCharactor changeCharactor = PlayerCharactor.Azaha_mode;

    [SerializeField] private ParticleSystem jumpEffect;
   void Start()
    {
        moveVector = Vector3.zero;
        playerStats = GetComponent<PlayerStats>();
        playerTransform = GetComponent<Transform>();       
        charactorController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!onColliding) wallClimming = false;
        if (!CheckStatus(PlayerState.Player_Swing) && !playerStats.playerDeath)
        {
            PlayerMoveVectorSet();
            PlayerKeyControll();
        }
        PlayerMoveAniamtion();

        Vector3 playerCenter = playerTransform.position + charactorController.center;
        Debug.DrawLine( playerCenter, playerCenter + -playerTransform.right, Color.red);
        Debug.DrawLine( playerCenter, playerCenter + playerTransform.right, Color.red);
        Debug.DrawLine( playerCenter, playerCenter + playerTransform.forward, Color.red);
    }

    //fix->oncollide->update
    private void FixedUpdate()
    {
        onColliding = false;
        if (!CheckStatus(PlayerState.Player_Swing) && !playerStats.playerDeath) PlayerMoveRotation();
    }

    void PlayerKeyControll()
    {
        if (Input.GetKeyDown(KeyCode.Q)) BattleMode();
        if (Input.GetKeyDown(KeyCode.Tab)) ChangeCharactor();

        if (Input.GetKeyDown(KeyCode.Space))PlayerJump();
        if (Input.GetKeyDown(KeyCode.LeftShift)) PlayerSlide();

        if (Input.GetKeyDown(KeyCode.Z)) playerStats.PlayerSkill(1);
        if (Input.GetKeyDown(KeyCode.X)) playerStats.PlayerSkill(2);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAttack();
            MouseClick();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerStats.playerCharactor == PlayerCharactor.Azaha_mode) PlayerAttack();

        if (Input.GetKeyDown(KeyCode.R)) PlayerSpecialAttack();
        if (Input.GetKeyDown(KeyCode.I)) playerUI.InventoryOnOff();
        if (Input.GetKeyDown(KeyCode.T)) playerUI.DroneViewOnOff();

        if (Input.GetKeyDown(KeyCode.Alpha1)) playerStats.UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) playerStats.UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) playerStats.UseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) playerStats.UseItem(3);
    }

    void PlayerMoveVectorSet()
    {
        if (stopPlayer)//player가 벽에 매달려서 이동할 경우
        {
            moveVector = charactorMoveValue.InputVector();
            moveVector = new Vector3(moveVector.x, moveVector.y,0);
            playerTransform.position = new Vector3(playerTransform.position.x, hangHeight, playerTransform.position.z);
        }
        else
        {
            moveVector = charactorMoveValue.InputVector();
            if (CheckStatus(PlayerState.Player_SlideStep)) magnification = 2f;
            else magnification = 1.0f;
        }
    }

    void PlayerMoveAniamtion()
    {
        if (CheckStatus(PlayerState.Player_Idle) || CheckStatus(PlayerState.Player_Run))
        {
            if (charactorMoveValue.InputPressCheck())
            {
                PlayerSound.playSoundManagerCall().PlayAudio("footSound",false,0);
                playerStats.CurPlayerStatus(PlayerState.Player_Run);
            }
            else playerStats.CurPlayerStatus(PlayerState.Player_Idle);
        }
        else if (stopPlayer)
        {
            playerStats.CurPlayerStatus(PlayerState.Player_Hang);
        }
    }

    void PlayerMoveRotation()
    {
        if (unGravity != 0) charactorMoveValue.charactorHeight += unGravity;
        moveVector.y = charactorMoveValue.charactorHeight;

        //----------------------------gravity---------------------------------------------------

        if (!charactorController.isGrounded && !stopPlayer)
        {
            charactorMoveValue.charactorHeight += charactorMoveValue.gravity;
            if (charactorMoveValue.charactorHeight < gravityMax) charactorMoveValue.charactorHeight = gravityMax;
        }
        else if (charactorController.isGrounded && charactorMoveValue.charactorHeight < 0.3)   
        {
            charactorMoveValue.charactorHeight = 0.0f;

            if (CheckStatus(PlayerState.Player_Jump) || CheckStatus(PlayerState.Player_DoubleJump) || CheckStatus(PlayerState.Player_Fall) ||
                CheckStatus(PlayerState.Player_JumpAttack) || CheckStatus(PlayerState.Player_Hurt))
            {
                playerStats.CurPlayerStatus(PlayerState.Player_Idle);
            }
        }

        //----------------------------MoveRotation-----------------------------------------------------------

        if (!CheckStatus(PlayerState.Player_Attack) && !CheckStatus(PlayerState.Player_SpecialAttack))
        {
            charactorController.Move(playerTransform.rotation * moveVector * charactorMoveValue.moveSpeed * magnification * Time.deltaTime);
            if (!stopPlayer)playerTransform.Rotate(0f, charactorMoveValue.InputRotationY(), 0f);
        }
        else if(CheckStatus(PlayerState.Player_Attack))
        {
            float attackDelay = 0.3f;
            playerTransform.Rotate(0f, charactorMoveValue.InputRotationY() * attackDelay, 0f);
        }
    }

    void PlayerAttack()
    {
        if(playerStats.playerBattle)
        {
            if (CheckStatus(PlayerState.Player_Idle) || CheckStatus(PlayerState.Player_Run) || CheckStatus(PlayerState.Player_Attack))
            {
                playerStats.CurPlayerStatus(PlayerState.Player_Attack);
            }
            else if(CheckStatus(PlayerState.Player_Jump)|| CheckStatus(PlayerState.Player_DoubleJump)&&playerStats.playerCharactor==PlayerCharactor.Kohaku_mode)
            {
                playerStats.CurPlayerStatus(PlayerState.Player_JumpAttack);
            }
        }
    }

    void PlayerSlide()
    {
        if (CheckStatus(PlayerState.Player_Run))
        {
            PlayerSound.playSoundManagerCall().PlayAudio("playerSlide",false,0.4f);
            playerStats.CurPlayerStatus(PlayerState.Player_SlideStep);
        }
    }

    void PlayerSpecialAttack()
    {
        if (playerStats.playerBattle && CheckStatus(PlayerState.Player_Idle))
        {
            playerStats.CurPlayerStatus(PlayerState.Player_SpecialAttack);
            playerStats.SpecialAttack();
        }
    }
    
    void PlayerJump()
    {
        if (!CheckStatus(PlayerState.Player_Jump) && !CheckStatus(PlayerState.Player_DoubleJump))
        {
            if (CheckStatus(PlayerState.Player_Idle) || CheckStatus(PlayerState.Player_Run))
            {
                PlayerSound.playSoundManagerCall().PlayAudio("jumpSound",false,0);
                charactorMoveValue.charactorHeight = charactorMoveValue.jumpPower * magnification;
                playerStats.CurPlayerStatus(PlayerState.Player_Jump);
            }
        }
        else if (!CheckStatus(PlayerState.Player_DoubleJump) && CheckStatus(PlayerState.Player_Jump))
        {
            PlayerSound.playSoundManagerCall().PlayAudio("doubleJumpSound",true,0);
            charactorMoveValue.charactorHeight = charactorMoveValue.jumpPower * magnification;
            playerStats.CurPlayerStatus(PlayerState.Player_DoubleJump);
            jumpEffect.Play();
        }

        if(stopPlayer)  //만약 플레이어가 매달려있는(hang)상태면 jump상태로 바꾸고 hang상태를 해제한다
        {
            charactorMoveValue.charactorHeight = charactorMoveValue.jumpPower * magnification;
            playerStats.CurPlayerStatus(PlayerState.Player_Jump);
            stopPlayer = false;
        }
    }

    void ChangeCharactor()
    {
        if (!playerStats.playerBattle)
        {
            PlayerCharactor preCharactorNum = playerStats.playerCharactor;
            playerStats.playerCharactor = changeCharactor;
            playerStats.ChractorChange();
            changeCharactor = preCharactorNum;
        }
    }

    void BattleMode()
    {
        if (playerStats.playerBattle) playerStats.playerBattle = false;
        else playerStats.playerBattle = true;
        playerStats.BattleWeaponCheck();
    }
    //-------------------------------------------------------------
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Wall") && !playerStats.playerBattle)
        {
            onColliding = true;
            if (!charactorController.isGrounded && hit.normal.y < 0.1f  &&
                CheckStatus(PlayerState.Player_Jump) && !CheckStatus(PlayerState.Player_DoubleJump)|| CheckStatus(PlayerState.Player_Run))
            {
                RaycastHit rayHit;
                Vector3 playerCenter = playerTransform.position + charactorController.center;

                Vector3 incomingVec = playerTransform.forward.normalized;
                Vector3 nomalVec = hit.normal;
                Vector3 reflectVec = Vector3.Reflect(incomingVec, nomalVec);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    charactorMoveValue.charactorHeight = charactorMoveValue.jumpPower * magnification;
                    StartCoroutine(WallJumpTurn(reflectVec, 0.5f));
                }
                else if (Physics.Raycast(playerCenter, playerTransform.right, out rayHit, 0.5f))
                {
                    Debug.DrawLine(playerCenter, playerTransform.right, Color.red, 0.5f);
                    if (charactorMoveValue.charactorHeight < 0)
                    {
                        charactorMoveValue.charactorHeight = 0;
                        playerStats.CurPlayerStatus(PlayerState.Player_Run);
                    }
                }
                else if (Physics.Raycast(playerCenter, -playerTransform.right, out rayHit, 0.5f))
                {
                    Debug.DrawLine(playerCenter, -playerTransform.right, Color.red, 0.5f);
                    if (charactorMoveValue.charactorHeight < 0)
                    {
                        charactorMoveValue.charactorHeight = 0;
                        playerStats.CurPlayerStatus(PlayerState.Player_Run);
                    }
                }
                else if (Physics.Raycast(playerCenter, playerTransform.forward, out rayHit, 0.5f))
                {
                    Debug.DrawLine(playerCenter, playerTransform.forward, Color.red, 0.5f);
                    if (Input.GetKey(KeyCode.W) && !wallClimming)StartCoroutine(ClimingWall(0.5f, reflectVec));
                }
            }
        }

        if (hit.transform.gameObject.CompareTag("Hang"))
        {
            stopPlayer = true;
            onColliding = true;
            magnification = 0.3f;
            charactorMoveValue.charactorHeight = 0.0f;
            StartCoroutine(WallJumpTurn(-hit.normal, 0.5f));
            playerStats.CurPlayerStatus(PlayerState.Player_Hang);
            hangHeight = hit.transform.gameObject.transform.position.y-1.5f;
        }

        if (hit.transform.gameObject.CompareTag("Bike"))//bike
        {
            if (Input.GetKeyDown(KeyCode.R))bikeSystem.RideOnPlayer();
        }
    }

    IEnumerator ClimingWall(float time, Vector3 reflectVec)
    {
        float timer = 0.0f;

        wallClimming = true;
        playerStats.CurPlayerStatus(PlayerState.Player_Climing);

        while (time > timer)
        {
            timer += Time.deltaTime;

            if (onColliding && !stopPlayer)
            {
                charactorMoveValue.charactorHeight = climmingPower;
            }

            if (Input.GetKeyDown(KeyCode.Space))    //벽타기 도중 벽점프를 하는 경우
            {
                charactorMoveValue.charactorHeight = charactorMoveValue.jumpPower * magnification;
                StartCoroutine(WallJumpTurn(reflectVec, 0.5f));
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        unGravity = 0.0f;

        if (playerStats.playerState == PlayerState.Player_Climing && !charactorController.isGrounded)
        {
            playerStats.CurPlayerStatus(PlayerState.Player_Fall);
        }
        else if(charactorController.isGrounded)
        {
            playerStats.CurPlayerStatus(PlayerState.Player_Idle);
        }
        yield return null;
    }

    IEnumerator WallJumpTurn(Vector3 reflectVec, float time)
    {
        var targetRotation = Quaternion.LookRotation(reflectVec, Vector3.up);
        float timer = 0.0f;

        while (time > timer)
        {
            timer += Time.deltaTime;
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 15 * Time.deltaTime);
            playerTransform.localEulerAngles = new Vector3(0, playerTransform.localEulerAngles.y, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    bool CheckStatus(PlayerState playerStatus)
    {
        if (playerStats.playerState == playerStatus) return true;
        else return false;
    }

    void MouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.CompareTag("NPC"))
            {
                hit.transform.GetComponent<ShopNPC>().OpenShopUI();
            }
        }
    }
}
