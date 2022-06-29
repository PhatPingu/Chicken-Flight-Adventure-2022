using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCollision : MonoBehaviour
{
    [SerializeField] PlayerAnimationControl _playerAnimationControl;
    [SerializeField] PlayerSoundControl _playerSoundControl;

    void OnTriggerEnter(Collider other)
    {
        _playerAnimationControl.CallHeadCollision_Animation();
        _playerSoundControl.PlaySFX_HeadBang();
        if(other.tag == "Platform")
        {
            Debug.Log("HeadButt");
            _playerAnimationControl.CallHeadCollision_Animation();
            _playerSoundControl.PlaySFX_HeadBang();
        }
    }
}
