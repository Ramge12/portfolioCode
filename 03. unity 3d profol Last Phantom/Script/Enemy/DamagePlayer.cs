using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public bool battle;

    [System.NonSerialized] public int damage;
    [System.NonSerialized] public bool oneDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (battle && oneDamage)
            {
                other.transform.GetComponent<PlayerStats>().AddPlayerHealthPoint(damage,0);
                oneDamage = false;
            }
        }
    }
}
