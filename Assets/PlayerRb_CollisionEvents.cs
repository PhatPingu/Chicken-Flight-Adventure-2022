using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRb_CollisionEvents : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor" || other.gameObject.tag == "Platform")
        {
            _playerBehaviour.canGoodJump = true;
            _playerBehaviour.canAverageJump = true;
            _playerBehaviour.canWeakJump = true;
        }
        
        if(other.gameObject.tag == "Water")
        {
            _playerBehaviour.notOnWater = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Water")
        {
            _playerBehaviour.notOnWater = true;
        }
    }
}