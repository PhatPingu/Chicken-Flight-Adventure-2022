using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class InputDeviceSelector : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private GameObject _camFreeLook_GamePad;
    [SerializeField] private GameObject _camFreeLook_Keyboard;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        //SwitchPlayerInput();
        SwitchCameraAxisInput();
    }

    /*void SwitchPlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.F1) && playerInput.currentControlScheme == "Keyboard")
        {
            playerInput.SwitchCurrentControlScheme("Gamepad");
        }
        
        if(Input.GetKeyDown(KeyCode.F1) && playerInput.currentControlScheme == "Gamepad")
        {
            playerInput.SwitchCurrentControlScheme("Keyboard");
        }
    }*/

    
    // Switch from Vector2 to float action for XY movement in the CinemachineInputProvider
    void SwitchCameraAxisInput()
    {
        if(playerInput.currentControlScheme == "Keyboard")
        {
            _camFreeLook_GamePad.SetActive(false);
            _camFreeLook_Keyboard.SetActive(true);
        }
        else
        {
            _camFreeLook_GamePad.SetActive(true);
            _camFreeLook_Keyboard.SetActive(false);
        }
    }
}
