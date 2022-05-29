using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBahaviour : MonoBehaviour
{
    [SerializeField] private Transform targetLocationTransform;
    [SerializeField] private GameObject player;
    [SerializeField] private bool waitOnTimer;
    [SerializeField] private float alarm;
    [SerializeField] private float alarm_reset;

    void Update()
    {
        FreezePlayer_OnTimer(waitOnTimer);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = targetLocationTransform.position;
            waitOnTimer = true;
        }
    }
    
    void FreezePlayer_OnTimer(bool check)
    {
        if(check)
        {
            alarm -= Time.deltaTime;
            if(alarm <= 0)
            {
                waitOnTimer = false;
                alarm = alarm_reset;
            }

            player.transform.position = targetLocationTransform.position;
        }
    }
}
