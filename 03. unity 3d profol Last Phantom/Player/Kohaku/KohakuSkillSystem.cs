using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KohakuSkillSystem : MonoBehaviour {

    public enum State { Swinging, Falling, Walking };

    [Header("-Battle Value")]
    [SerializeField] private GameObject kohakuWeapon;
    [SerializeField] private GameObject weaponStartParticle;
    [SerializeField] private GameObject weaponEndParticle;
    [SerializeField] private Transform camTransform;

    [Header("-WebSkill Value")]
    [SerializeField] private Web webSkill;
    [SerializeField] private WebManager webManager;

    private State state;
    private bool spiderSkillOn = false;
    private bool kohakuSmall = false;
    private Vector3 previousPosition;
    private Transform playerTransform;
    private CharacterController controller;

    private bool skill_1_available = true;
    [SerializeField] private float skill_1_CoolTime = 0;

    public bool skill_3_available = true;
    [SerializeField] private float skill_3_CoolTime = 0;

    [SerializeField] Text skill_1_Text;
    [SerializeField] Text skill_3_Text;
    [SerializeField] private PlayerUI playerUI;

    void Start()
    {
        state = State.Walking;
        playerTransform = this.transform.parent;
        previousPosition = playerTransform.localPosition;
        controller = playerTransform.GetComponent<CharacterController>();
        webManager.playerTransform.parent = webManager.wepPointTransform;
    }

    public void WeaponCheck(bool m_Battle)
    {
        if (m_Battle) //전투시작
        {
            PlayerSound.playSoundManagerCall().PlayAudio("thunderSound",false,0);
            kohakuWeapon.SetActive(true);
            kohakuWeapon.transform.GetComponent<WeaponValue>().damage = transform.parent.GetComponent<PlayerStats>().PlayerAttackDamage();
            weaponStartParticle.SetActive(true);
            weaponEndParticle.SetActive(false);
        }
        else //전투종료
        {
            kohakuWeapon.SetActive(false);
            weaponStartParticle.SetActive(false);
            weaponEndParticle.SetActive(true);
        }
    }

    public void KohakuSize(CameraController cameraController)
    {
        if (kohakuSmall) kohakuSmall = false;
        else kohakuSmall = true;

        StartCoroutine(PlayerSizeControll(cameraController));
    }

    IEnumerator PlayerSizeControll(CameraController cameraController)
    {
        if (skill_1_available)
        {
            float time = Time.deltaTime * 5;
            while (true)
            {
                if (kohakuSmall)
                {
                    Time.timeScale = 0.5f;
                    playerTransform.tag = "Untagged";
                    if (playerTransform.localScale.x > 0.1f)
                    {
                        playerTransform.localScale = playerTransform.localScale - new Vector3(time, time, time);
                        if (cameraController.magnification > 0.2f) cameraController.magnification -= time;
                    }
                    else
                    {
                        playerTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        cameraController.magnification = 0.15f;
                        playerTransform.GetComponent<PlayerKeyController>().magnification = 0.5f;
                        break;
                    }
                }
                else
                {
                    Time.timeScale = 1f;
                    playerTransform.tag = "Player";
                    if (playerTransform.localScale.x < 1.0f)
                    {
                        playerTransform.localScale = playerTransform.localScale + new Vector3(time, time, time);
                        if (cameraController.magnification < 1.0f) cameraController.magnification += time;
                    }
                    else
                    {
                        playerTransform.localScale = new Vector3(1f, 1f, 1f);
                        cameraController.magnification = 1f;
                        playerTransform.GetComponent<PlayerKeyController>().magnification = 1.0f;
                        skill_1_available = false;
                        StartCoroutine(Skill_1_DelayTime());
                        break;
                    }
                }
                yield return new WaitForSeconds(time * 0.01f);
            }
        }
        yield return null;
    }

    IEnumerator Skill_1_DelayTime()
    {
        string preText = skill_1_Text.text;
        float curTime = skill_1_CoolTime;
        while(true)
        {
            skill_1_Text.text = skill_1_CoolTime.ToString("N2");
            skill_1_CoolTime -= Time.deltaTime;
            if (skill_1_CoolTime <= 0) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        skill_1_Text.text = preText;
        skill_1_CoolTime = curTime;
        skill_1_available = true;
        yield return null;
    }

    public void WebShootSystemOnOff()
    {
        if (spiderSkillOn)
        {
            webSkill.EndSkill();
            webSkill.enabled = false;
            spiderSkillOn = false;
            playerUI.SkillButton_2_OnOff();
        }
        else
        {
            webSkill.StartSkill();
            webSkill.enabled = true;
            spiderSkillOn = true;
            StartCoroutine(KohakuSpiderMode());
            playerUI.SkillButton_2_OnOff();
        }
    }

    IEnumerator KohakuSpiderMode()
    {
        while (spiderSkillOn)
        {
            DetermineState();
            switch (state)
            {
                case State.Swinging:
                    DoSwingAction();
                    break;
                case State.Falling:
                    DoFallingAction();
                    break;
                case State.Walking:
                    break;
            }
            previousPosition = playerTransform.localPosition;
            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
        yield return null;
    }

    void DetermineState()
    {
        if (controller.isGrounded)
        {
            if (CheckStatus(PlayerState.Player_Swing) || CheckStatus(PlayerState.Player_Fall))
            {
                webManager.playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Idle);
            }
            state = State.Walking;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (state == State.Walking)
                {
                    webManager.webController.velocity = webManager.playerTransform.GetComponent<PlayerKeyController>().moveVector;
                }
                webManager.playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Swing);
                webManager.SwitchTether(hit.point);
                state = State.Swinging;
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (state == State.Swinging)state = State.Falling;
        }
    }

    void DoSwingAction()
    {
        if (Input.GetKey(KeyCode.W)) webManager.webController.velocity += camTransform.forward * 0.6f;
        if (Input.GetKey(KeyCode.A)) webManager.webController.velocity -= camTransform.right * 0.6f;
        if (Input.GetKey(KeyCode.D)) webManager.webController.velocity += camTransform.right * 0.6f;
        if (Input.GetKey(KeyCode.S)) webManager.webController.velocity -= camTransform.forward * 0.6f;

        controller.Move(webManager.MoveBob(playerTransform.localPosition, previousPosition, Time.deltaTime));
        previousPosition = playerTransform.localPosition;
    }

    void DoFallingAction()
    {
        if (CheckStatus(PlayerState.Player_Swing))
        {
            webManager.playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Fall);
        }
        webManager.webLength = Mathf.Infinity;
        controller.Move(webManager.Fall(transform.localPosition, Time.deltaTime));
        previousPosition = transform.localPosition;
    }

    bool CheckStatus(PlayerState playerStatus)
    {
        if (webManager.playerTransform.GetComponent<PlayerStats>().playerState == playerStatus) return true;
        else return false;
    }

    public IEnumerator WeaponSpecialSkill()
    {
        if (skill_3_available)
        {
            skill_3_available = false;
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                kohakuWeapon.transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, Time.deltaTime * 3);
                if (timer > 1.0f) break;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return WeaponBack();
        }
    }

    IEnumerator WeaponBack()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            kohakuWeapon.transform.localScale -= new Vector3(timer, timer, timer);
            if (kohakuWeapon.transform.localScale.x < 1.0f)
            {
                kohakuWeapon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return Skill_3_DelayTime();
    }

    IEnumerator Skill_3_DelayTime()
    {
        string preText = skill_3_Text.text;
        float curTime = skill_3_CoolTime;
        while (true)
        {
            skill_3_Text.text = skill_3_CoolTime.ToString("N2");
            skill_3_CoolTime -= Time.deltaTime;
            if (skill_3_CoolTime <= 0) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        skill_3_Text.text = preText;
        skill_3_CoolTime = curTime;
        skill_3_available = true;
        yield return null;
    }
}
