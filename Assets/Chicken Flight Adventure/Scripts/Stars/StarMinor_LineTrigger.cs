using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMinor_LineTrigger : MonoBehaviour
{
    [SerializeField] private BehaviourStarMinor _behaviourStarMinor;
    [SerializeField] private LineRenderer lineRenderer;

    private GlobalVolumeController _globalVolumeController;

    private bool lineOn;

    void Start()
    {
        _globalVolumeController = GameObject.Find("Global Volume").GetComponent<GlobalVolumeController>();
    }

    void Update()
    {
        if(lineOn)
        {
            _behaviourStarMinor.MakeLineToPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lineRenderer.enabled = true;
            lineOn = true;
            _globalVolumeController.ChangeGlobalVolumeProfile(2);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            lineRenderer.enabled = false;
            lineOn = false;
            _globalVolumeController.ChangeGlobalVolumeProfile(0);
        }
    }
}
