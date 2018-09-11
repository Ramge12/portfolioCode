using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GodzillaStatus : MonoBehaviour {

    [Header("GozillaScripts")]
    [SerializeField] private EnemyStatus godzillaStatus;
    [SerializeField] private Transform[] attackParts;
    [SerializeField] private SimpleHealthBar healthBar;
    [SerializeField] private CharactorStatistics godzillaStatistics;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private ParticleSystem hitEffect;

    void Start()
    {
        godzillaStatistics.hpMax = godzillaStatistics.HP;
        healthBar.transform.GetChild(0).GetComponent<Text>().text = godzillaStatistics.HP.ToString() + "/" + godzillaStatistics.hpMax.ToString();
        for (int i = 0; i < attackParts.Length; i++)attackParts[i].GetComponent<DamagePlayer>().damage = godzillaStatistics.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            hitEffect.Play();
            hitSound.Play();
            godzillaStatistics.AddCharactorHealthPoint(-other.transform.GetComponent<WeaponValue>().damage);
            healthBar.UpdateBar(godzillaStatistics.HP, godzillaStatistics.hpMax);
            healthBar.transform.GetChild(0).GetComponent<Text>().text = godzillaStatistics.HP.ToString()+"/"+godzillaStatistics.hpMax.ToString();
            if (godzillaStatistics.HP <= 0)
            {
                this.transform.GetComponent<GodzillaController>().godzillaDeath = true;
            }
        }
    }

    public int geDamage()
    {
        return godzillaStatistics.Damage;
    }
}
