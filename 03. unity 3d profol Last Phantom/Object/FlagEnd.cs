using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagEnd : MonoBehaviour {

    [SerializeField] private Stage2Quest stage2Quest;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bike"))
        {
            stage2Quest.NextStage();
        }
    }
}
