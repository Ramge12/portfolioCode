using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimmingWall : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerKeyController>().stopPlayer = false;
            other.transform.GetComponent<PlayerKeyController>().magnification = 1.0f;
            other.transform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Fall);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("접촉중");
        }
    }
}
