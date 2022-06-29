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
    
    bool CameraFX_activated; //

    void Start()
    {
        _cameraBehaviour = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>(); //

        _player = GameObject.Find("Player_Group");
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        //This is broken
        //ChangeZoom(CameraFX_activated);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Boost_ToTarget();
            Invoke("EndDash", boost_Timer);
            CameraFX_activated = true;
        }
    }

    void Boost_ToTarget()
    {
        Rigidbody _rb = _player.GetComponent<PlayerBehaviour>().rb;
        boostDirection = boostTarget.transform.position - transform.position;
        boostDirection = new Vector3(boostDirection.x, yForce, boostDirection.z);
        
        _rb.AddForce(boostDirection.normalized * boostForce, ForceMode.Impulse);
        _playerAnimationControl.CallJump_Animation("GoodJump");
        _playerBehaviour.i_frameActive = true;
    }

    void EndDash()
    {
        _playerBehaviour.EndDash();
        CameraFX_activated = false;
        _playerBehaviour.i_frameActive = false;
    }

    void ChangeZoom(bool choice)  //THIS CAUSES CONFLICT  --- Not being called atm
    {
        if(choice == true)  
            _cameraBehaviour.ChangeZoom(choice, 120f, -40f, -20f);
    }
}
