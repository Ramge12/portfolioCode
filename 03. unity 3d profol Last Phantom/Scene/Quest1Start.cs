using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest1Start : MonoBehaviour {

    [SerializeField] private ScarabSpawnPoint[] scarabSpawnPoint = new ScarabSpawnPoint[3];
 

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i=0; i< scarabSpawnPoint.Length; i++)
            {
                scarabSpawnPoint[i].StartCoroutine(scarabSpawnPoint[i].StartSpawn());
            }
        }
    }
}
