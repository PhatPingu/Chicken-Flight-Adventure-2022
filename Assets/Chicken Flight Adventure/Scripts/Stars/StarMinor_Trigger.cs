using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMinor_Trigger : MonoBehaviour
{
    [SerializeField] private BehaviourStarMinor _behaviourStarMinor;
    [SerializeField] private LineRenderer lineRenderer;

    private bool lineOn;

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
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            lineRenderer.enabled = false;
            lineOn = false;
        }
    }
}
