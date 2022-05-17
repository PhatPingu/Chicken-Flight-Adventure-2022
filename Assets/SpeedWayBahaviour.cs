using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWayBahaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float walkSpeedMultiplier;

    private PlayerBehaviour _playerBehaviour;

    void Start()
    {
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if(other.gameObject == _player)
        {
            _playerBehaviour.walkSpeed = walkSpeedMultiplier * _playerBehaviour.walkSpeed;
        }
    }

}
