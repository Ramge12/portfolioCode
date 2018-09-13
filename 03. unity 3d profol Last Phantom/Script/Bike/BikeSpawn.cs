using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSpawn : MonoBehaviour {

    [SerializeField]private GameObject[] bikeParts = new GameObject[22];

    void Start()
    {
        for (int i = 0; i < bikeParts.Length; i++)  bikeParts[i] = this.transform.GetChild(i).gameObject;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int bikeCount = 0;
        while (true)
        {
            if (bikeCount == bikeParts.Length)
            {
                break;
            }
            else
            {
                bikeParts[bikeCount].SetActive(true);
                bikeCount++;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
