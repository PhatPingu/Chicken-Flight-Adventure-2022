using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBoost_Circle : MonoBehaviour
{
    [SerializeField] private GameObject boostTarget;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerAnimationControl _playerAnimationControl;
    [SerializeField] private Vector3 boostDirection;
    [SerializeField] private float boostForce;

    void Start()
    {
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
    }

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
        boostDirection = new Vector3(boostDirection.normalized.x, 0, boostDirection.normalized.z);
        
        _rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
        _playerAnimationControl.CallJump_Animation("GoodJump");
    }
}
