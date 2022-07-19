using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRb_CollisionEvents : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private VFX_Controller _VFX_Controller;

    void OnCollisionEnter(Collision other)
    {
       
        if(other.gameObject.tag == "Water")
        {
            _playerBehaviour.notOnWater = false;
            _VFX_Controller.waterSplash.Play();
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
