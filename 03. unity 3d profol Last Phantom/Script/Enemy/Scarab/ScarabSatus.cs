using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabSatus : MonoBehaviour {

    public GameObject hitText;
    [SerializeField] private float nockForce;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ParticleSystem hitEffect;

    [System.NonSerialized] public Transform bodyTransform;
    [System.NonSerialized] public CharactorStatistics chractorStat;
    bool scarabDeath = false;

    private void Start()
    {
        chractorStat.hpMax = chractorStat.HP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            playerTransform = other.transform.GetComponent<WeaponValue>().playerTransform;
            Vector3 knockBack = bodyTransform.position - other.transform.position;
            knockBack = knockBack.normalized;
            bodyTransform.GetComponent<Rigidbody>().AddForce(knockBack * nockForce);

            hitEffect.Play();
            
            if (chractorStat.HP > 0)
            {
                hitText.SetActive(true);
                hitText.transform.position = this.transform.position;
                hitText.GetComponent<Rigidbody>().Sleep();
                hitText.GetComponent<Rigidbody>().AddForce(0, 300f, 0);
                hitText.transform.GetChild(0).GetComponent<TextMesh>().text = (-playerTransform.GetComponent<PlayerStats>().PlayerAttackDamage()).ToString();
                if (chractorStat.HP < chractorStat.hpMax * 0.5f) hitText.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0);
                chractorStat.AddCharactorHealthPoint(-playerTransform.GetComponent<PlayerStats>().PlayerAttackDamage());
            }
            else
            {
                playerTransform.GetComponent<PlayerInventory>().SetGold(chractorStat.Exp);
                bodyTransform.GetComponent<ScarabController>().SetAnimation(EnemyStatus.enemy_Death);
                if (!scarabDeath)
                {
                    scarabDeath = true;
                    bodyTransform.GetComponent<ScarabController>().sacrabSpawnPoint.CountUI();
                }
            }
        }
        if (other.CompareTag("CarSkill"))
        {
            hitEffect.Play();
            if (chractorStat.HP > 0)
            {
                hitText.SetActive(true);
                hitText.transform.position = this.transform.position;
                hitText.GetComponent<Rigidbody>().Sleep();
                hitText.GetComponent<Rigidbody>().AddForce(0, 300f, 0);
                hitText.transform.GetChild(0).GetComponent<TextMesh>().text = (-other.GetComponent<WeaponValue>().damage).ToString();
                if (chractorStat.HP < chractorStat.hpMax * 0.5f) hitText.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0);
                chractorStat.AddCharactorHealthPoint(-other.GetComponent<WeaponValue>().damage);
            }
            else
            {
                //playerTransform.GetComponent<PlayerInventory>().SetGold(chractorStat.Exp);
                bodyTransform.GetComponent<ScarabController>().SetAnimation(EnemyStatus.enemy_Death);
                if (!scarabDeath)
                {
                    scarabDeath = true;
                    bodyTransform.GetComponent<ScarabController>().sacrabSpawnPoint.CountUI();
                }
            }
        }
    }
}
