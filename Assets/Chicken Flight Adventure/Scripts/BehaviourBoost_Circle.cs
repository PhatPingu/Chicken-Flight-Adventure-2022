using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBoost_Circle : MonoBehaviour
{
    [SerializeField] private GameObject boostTarget;
    [SerializeField] private float boostForce;
    [SerializeField] private float yForce;
    [SerializeField] private float boost_Timer;

    private Vector3 boostDirection;
    private CameraBehaviour _cameraBehaviour; //
    private GameObject _player;
    private PlayerBehaviour _playerBehaviour;
    private PlayerAnimationControl _playerAnimationControl;
    
    bool activateCameraFX; //

    void Start()
    {
        _cameraBehaviour = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>(); //

        _player = GameObject.Find("Player_Group");
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Boost_ToTarget();
            Invoke("EndDash", boost_Timer);
        }
    }

    void Boost_ToTarget()
    {
        Rigidbody _rb = _player.GetComponent<PlayerBehaviour>().rb;
        boostDirection = boostTarget.transform.position - _player.transform.position;
        boostDirection = new Vector3(boostDirection.normalized.x, yForce, boostDirection.normalized.z);
        
        _rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
        _playerAnimationControl.CallJump_Animation("GoodJump");
    }

    void EndDash()
    {
        _playerBehaviour.EndDash();
    }
}
