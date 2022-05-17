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
            _playerBehaviour.canWeakJump = true;
            playerIsGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerIsGrounded = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor" || other.gameObject.tag == "Platform")
        {
            _playerBehaviour.canGoodJump = true;
            _playerBehaviour.canWeakJump = true;
            playerIsGrounded = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
        playerIsGrounded = false;
    }
}
