using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMinor_ProximityTrigger : MonoBehaviour
{
    private GlobalVolumeController _globalVolumeController;

    void Start()
    {
        _globalVolumeController = GameObject.Find("Global Volume").GetComponent<GlobalVolumeController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _globalVolumeController.ChangeGlobalVolumeProfile(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _globalVolumeController.ChangeGlobalVolumeProfile(false);
        }
    }
}
