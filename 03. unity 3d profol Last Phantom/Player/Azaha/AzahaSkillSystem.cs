using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AzahaSkillSystem : MonoBehaviour {

    [Header("-Battle Set")]
    [SerializeField] private Transform azahaWeapon;
    [SerializeField] private Transform azahaWings;
    [SerializeField] private Transform azaha_AimUI;

    [SerializeField] private GameObject weaponStartParticle;
    [SerializeField] private GameObject weaponEndParticle;
    [SerializeField] private ParticleSystem attackParticle;
    [Header("-Portal Set")]
    [SerializeField] private Transform leftPortal;
    [SerializeField] private Transform rightPortal;

    [Header("-FPS bullet Set")]
    [SerializeField] private Camera skillCamera;

    [SerializeField] private float attackSpeed = 5;
    [SerializeField] private Transform magicShield;

    public bool enableFly = true;        //비행가능상태

    public bool curFlying = false;       //현재 비행중

    public bool flySkillOnOff = false;   //스킬 상태 여부

    public bool potalSkillOnOff = false;

    public bool reLoading = false;

    public bool shootDelay = false;

    [SerializeField] Text skill_1_Text;
    [SerializeField] Text skill_3_Text;
    [SerializeField] float skill_3_CoolTime;

    private int bulletNumber = 0;
    private bool hitObject;
    private float flyingHeight = 0f;
    private float flyTime = 0;

    private Vector3 attackPoint;
    private Transform playerTransform;
    private AzahaWingController azahaWingController;

    private bool specialSkillAvailable = true;

    void Start()
    {
        attackPoint = transform.forward;
        playerTransform = this.transform.parent;
        azahaWingController = transform.GetComponent<AzahaWingController>();
    }

    private void LateUpdate()
    {
        if (!potalSkillOnOff) AzahaWeaponAim();
        else AzahaPotalSystem();
    }

    void AzahaWeaponAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.collider.gameObject && !hit.transform.CompareTag("Bullet") )
            {
                attackPoint = hit.point;
                Vector3 pos = (skillCamera.WorldToScreenPoint(hit.point));
                azaha_AimUI.position = pos;
                hitObject = true;
            }
        }
        else
        {
            hitObject = false;
            azaha_AimUI.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    void AzahaPotalSystem()
    {
        azaha_AimUI.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) leftPortal.localScale = new Vector3(1.0f, 1.0f, leftPortal.localScale.z);
        if (Input.GetMouseButtonDown(1)) rightPortal.localScale = new Vector3(1.0f, 1.0f,rightPortal.localScale.z);

        if (Input.GetMouseButton(0)) ThrowPortal(leftPortal);
        if (Input.GetMouseButton(1)) ThrowPortal(rightPortal);
    }

    void ThrowPortal(Transform portal)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject && !hit.transform.CompareTag("Portal") && !hit.transform.CompareTag("Range"))
            {
                Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
                portal.position = hit.point;
                portal.rotation = hitObjectRotation;
            }
        }
        if (portal.localScale.x < 5.0f)
            portal.localScale = portal.localScale + new Vector3(Time.deltaTime * 5, Time.deltaTime * 5, 0);
    }

    public void WeaponCheck(bool m_Battle)
    {
        if (m_Battle) //전투시작
        {
            PlayerSound.playSoundManagerCall().PlayAudio("suitUp", false, 0);
            azahaWingController.StartWingRoation();
            azahaWeapon.gameObject.SetActive(true);
            azahaWings.gameObject.SetActive(true);
            weaponStartParticle.SetActive(true);
            weaponEndParticle.SetActive(false);
            azaha_AimUI.gameObject.SetActive(true);
        }
        else //전투종료
        {
            azahaWeapon.gameObject.SetActive(false);
            azahaWings.gameObject.SetActive(false);
            weaponStartParticle.SetActive(false);
            weaponEndParticle.SetActive(true);
            azaha_AimUI.gameObject.SetActive(false);
        }
    }

    public IEnumerator ShootSystem()
    {
        Transform bullet= BulletManager.bulletManagerCall().GetObject("Bullet").transform;

        bullet.gameObject.SetActive(true);
        bullet.GetComponent<BulletValue>().playerTransform = playerTransform;
        bullet.GetComponent<WeaponValue>().damage = (int)playerTransform.GetComponent<PlayerStats>().PlayerAttackDamage();
        bullet.GetComponent<WeaponValue>().playerTransform = playerTransform;
        bullet.GetComponent<Rigidbody>().Sleep();
        bullet.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bullet.position = azahaWeapon.position;

        float timer = 0f;

        while (true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
              
                timer += Time.deltaTime;
                bullet.localScale = bullet.localScale + new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, Time.deltaTime * 0.3f);
                bullet.position = azahaWeapon.position;
                if (timer > 1f) bullet.GetComponent<BulletValue>().passShot = true;
                if (timer > 2f) break;
            }
            else break;
            yield return new WaitForSeconds(Time.deltaTime);
            attackParticle.Play();
        }
        PlayerSound.playSoundManagerCall().PlayAudio("shootSound", true,0);
        bullet.GetComponent<BulletValue>().attackSpeed = attackSpeed;

        if (hitObject)
        {
            attackPoint = attackPoint - azahaWeapon.position;
            attackPoint = attackPoint.normalized;
            bullet.GetComponent<BulletValue>().dir = attackPoint.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.GetComponent<BulletValue>().dir * attackSpeed);
        }
        else
        {
            bullet.GetComponent<BulletValue>().dir = transform.forward;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * attackSpeed);
        }

        azahaWingController.SelectWingFold(bulletNumber);

        bulletNumber++;

        if(bulletNumber==6)
        {
            azahaWingController.AllWingFold();
            azahaWingController.StartWingRoation();
            reLoading = true;
            StartCoroutine(WaitForReload());
        }
        yield return null;
    }

    IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(1.0f);
        reLoading = false;
        BulletManager.bulletManagerCall().reload(6);
        bulletNumber = 0;
        yield return null;
    }

    public void StartFlying()
    {
        if (flySkillOnOff)
        {
            curFlying = true;
            StartCoroutine(AzahaFly());
        }
    }

    IEnumerator AzahaFly()
    {
        string skillText = skill_1_Text.text;
        PlayerKeyController playerController = playerTransform.GetComponent<PlayerKeyController>();
        while (enableFly)
        {
            if (Input.GetKey(KeyCode.Space)&& flyTime < 2.0f)
            {
                skill_1_Text.text = (2.0 - flyTime).ToString("N2");
                flyTime += Time.deltaTime;
                flyingHeight +=  Time.deltaTime;
                if (flyingHeight > 0.1f) flyingHeight = 0.1f;
            }
            else 
            {
                flyingHeight = 0;
                enableFly = false;
            }

            if (!enableFly)
            {
                skill_1_Text.text = skillText;
                StartCoroutine(FlyWaitTime());
            }

            playerController.unGravity = flyingHeight;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        curFlying = false;
        yield return null;
    }

    IEnumerator FlyWaitTime()
    {
        string skillText = skill_1_Text.text;
        float timer = 0.0f;
        playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Fall);
        while (3.0f > timer)
        {
            skill_1_Text.text = (3-timer).ToString("N2");
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        enableFly = true;
        flyTime = 0.0f;
        skill_1_Text.text = skillText;
        yield return null;
    }

    public void SpecialSkill()
    {
        if (specialSkillAvailable)
        {
            specialSkillAvailable = false;
            magicShield.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            magicShield.position = azahaWeapon.position;
            magicShield.gameObject.SetActive(true);
            StartCoroutine(SheildOpen());
        }
    }

    IEnumerator SheildOpen()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            magicShield.rotation = playerTransform.rotation;
            magicShield.position = playerTransform.position + playerTransform.forward + new Vector3(0,1,0);
            magicShield.localScale += new Vector3(Time.deltaTime*5, Time.deltaTime *5, Time.deltaTime);
            if (timer > 0.5f) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(Skill_3_Delay());
        yield return null;
    }

    IEnumerator Skill_3_Delay()
    {
        float timer = 0;
        string skillText = skill_3_Text.text;
        while (true)
        {
            timer += Time.deltaTime;
            skill_3_Text.text = (skill_3_CoolTime - timer).ToString("N2");
            if (timer >= skill_3_CoolTime) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        skill_3_Text.text = skillText;
        specialSkillAvailable = true;
        yield return null;
    }
}
