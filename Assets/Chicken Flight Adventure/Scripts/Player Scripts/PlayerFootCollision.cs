using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootCollision : MonoBehaviour
{
    public bool playerIsGrounded;

    [SerializeField] private PlayerBehaviour _playerBehaviour;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor" || other.gameObject.tag == "Platform")
        {
            _playerBehaviour.canGoodJump = true;
            _playerBehaviour.canAverageJump = true;
            _playerBehaviour.canWeakJump = true;
            playerIsGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerIsGrounded = false;
    }
}
