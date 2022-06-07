using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBoost_Circle : MonoBehaviour
{
    [SerializeField] private GameObject boostTarget;
    [SerializeField] private GameObject _player;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    [SerializeField] private float boostForce;
    [SerializeField] private float yForce;
    [SerializeField] private float boost_Timer;

    private Vector3 boostDirection;
    private PlayerAnimationControl _playerAnimationControl;
    
    bool activateCameraFX;
    float currentBoost_Timer;

    void Start()
    {
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
    }
/*
    void Update()
    {
        _cameraBehaviour.ChangeZoom(activateCameraFX, 125f, -60f, -30f);
        
        currentBoost_Timer -= Time.deltaTime;
        if (currentBoost_Timer <= 0)
        {
            activateCameraFX = false;
        }
    }
*/
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Boost_ToTarget();
        }
    }

    void Boost_ToTarget()
    {
        Rigidbody _rb = _player.GetComponent<PlayerBehaviour>().rb;
        boostDirection = boostTarget.transform.position - _player.transform.position;
        boostDirection = new Vector3(boostDirection.normalized.x, yForce, boostDirection.normalized.z);
        
        _rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
        _playerAnimationControl.CallJump_Animation("GoodJump");
        activateCameraFX = true;
        currentBoost_Timer = boost_Timer;
    }
}
