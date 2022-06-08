using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] private PlayerAnimationControl _playerAnimationControl;
    [SerializeField] private PlayerSoundControl _playerSoundControl;
    [SerializeField] private PlayerFootCollision _playerFootCollision;

    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float hoverForce = 10f;
    [SerializeField] private float pushDownForce = -700f;
    
    [Header("Dive Attributes")]
    [SerializeField] private float diveForce = 10f;
    [SerializeField] private float endDiveBoostForce = 100f;
    [SerializeField] private float diveTimer;
    [SerializeField] private float diveTimer_Reset = 2f;

    [Header("Jump Attributes")]
    [SerializeField] private float goodJumpTimer;
    [SerializeField] private float goodJumpTimer_Reset = 2f;
    [SerializeField] private float averageJumpTimer;
    [SerializeField] private float averageJumpTimer_Reset = 1.5f;
    [SerializeField] private float weakJumpTimer;
    [SerializeField] private float weakJumpTimer_Reset = 1f;
    
    [Header("Dash Attributes")]
    [SerializeField] private float forwardDashForce;
    [SerializeField] private float sideDashForce;
    [SerializeField] private float dashDuration;
    
    private Vector3 startDashVelocity;

    public float walkSpeed = 0.15f;

    public float bestJumpForce = 800f;
    public float averagebestJumpForce = 600f;
    public float weakbestJumpForce = 400f;
    
    //check if variables under shoudl be public?
    public bool canGoodJump = true;
    public bool canAverageJump = true;
    public bool canWeakJump = true;
    
    public bool inputHover;

    public bool do_EndDiveBoost;
    private bool inputDive;
    private bool isDiving;

    private PlayerInput playerInput;
    private InputAction jumpAction;                               
    private InputAction flyAction;
    private InputAction movementAction;
    private InputAction cameraAction;
    private InputAction diveAction;
    private InputAction forwardDashAction;
    private InputAction leftDashAction;
    private InputAction rightDashAction;

    void Awake()
    {
        playerInput =       GetComponent<PlayerInput>();
        jumpAction =        playerInput.actions["Jump"];          
        flyAction =         playerInput.actions["Fly"];
        movementAction =    playerInput.actions["Movement"];
        cameraAction =      playerInput.actions["RotateCamera"];
        diveAction =        playerInput.actions["Dive"];
        forwardDashAction = playerInput.actions["ForwardDash"];
        leftDashAction =    playerInput.actions["LeftDash"];
        rightDashAction =   playerInput.actions["RightDash"];
    }

    void Start()
    {
        movementAction.performed += _           => PlayerMovement();
        cameraAction.performed += _             => PlayerRotate();
        jumpAction.performed += context         => PerformJump(context);  
        flyAction.performed += context          => PerformFlight(context);
        diveAction.performed += context         => PerformDive(context);
        forwardDashAction.performed += context  => PerformFowardDash(context);
        leftDashAction.performed += context     => PerformLeftDash(context);
        rightDashAction.performed += context    => PerformRightDash(context);

        Physics.gravity = new Vector3(0, -10F, 0);
        goodJumpTimer = goodJumpTimer_Reset;
        averageJumpTimer = averageJumpTimer_Reset;
        weakJumpTimer = weakJumpTimer_Reset;
    }

    public void Boost()
    {
        rb.AddForce(0, bestJumpForce, 0);
    }

    void FixedUpdate()
    {
        PlayerMovement();       
        PlayerRotate();
        FlightController();
        VelocityContorol();
    }

    void Update()
    {
        CanFlyController();
        CanDiveController();
        DiveController();
        SoundController();
    }
    // Updates ------------------------------------------------------start
    void PlayerMovement()
    {
        rb.MovePosition(transform.position 
        + (transform.forward * movementAction.ReadValue<Vector2>().y * walkSpeed) 
        + (transform.right   * movementAction.ReadValue<Vector2>().x * walkSpeed));
    }

    void PlayerRotate()
    {
        if(playerInput.currentControlScheme == "Keyboard")
        {
        rb.MoveRotation(rb.rotation *
        Quaternion.Euler(new Vector3
            (0, Mouse.current.delta.x.ReadValue() * mouseSensitivity * 0.1f, 0)));
        } 
        else if(playerInput.currentControlScheme == "Gamepad")
        {
        rb.MoveRotation(rb.rotation *
        Quaternion.Euler(new Vector3
            (0, cameraAction.ReadValue<Vector2>().x * mouseSensitivity * 2f, 0)));
        }
    }
    
    void CanFlyController()
    {    
        goodJumpTimer -= Time.deltaTime;
        if (goodJumpTimer <= 0)
        {
            canGoodJump = true;
        }

        averageJumpTimer -= Time.deltaTime;
        if (averageJumpTimer <= 0)
        {
            canAverageJump = true;
        }

        weakJumpTimer -= Time.deltaTime;
        if (weakJumpTimer <= 0)
        {
            canWeakJump = true;
        }
    }

    void FlightController()
    {
        if (inputHover) {rb.AddForce(0, hoverForce    * Time.deltaTime, 0);}
        else            {rb.AddForce(0, pushDownForce * Time.deltaTime, 0);}
    }

    void VelocityContorol()
    {
        if(rb.velocity.y > 15f)  {rb.velocity = new Vector3(0,14.9f,0);}
        if(rb.velocity.y < -50f) {rb.velocity = new Vector3(0, -50f,0);}
    }
    
    void CanDiveController()  
    {    
        diveTimer -= Time.deltaTime;
        if (diveTimer <= 0)
        {
            do_EndDiveBoost = true;
        }
    }

    void DiveController()
    {
        if(inputDive)
        {
            _playerAnimationControl.Dive_Animation(true);
            rb.velocity = new Vector3(0f, -diveForce, 0f);        
            isDiving = true;
        } 
        else
        {
            _playerAnimationControl.Dive_Animation(false);
        }

        if(!inputDive && isDiving)
        {
            if(do_EndDiveBoost) 
            {
                rb.velocity = new Vector3(0f, y_StartVelocity + endDiveBoostForce, 0f); 
                do_EndDiveBoost = false;
            }
            isDiving = false;
        }
        else if(!inputDive && isDiving)
        {
            if(do_EndDiveBoost) 
            {
                do_EndDiveBoost = false;
            }
            isDiving = false;
        }
    }
    
    void SoundController()
    {
        // Walking
        if(movementAction.ReadValue<Vector2>().y != 0F
        || movementAction.ReadValue<Vector2>().x != 0F)
        {   _playerSoundControl.PlaySFX_Walking();  }
        else
        {   _playerSoundControl.StopSFX_Walking();  }

        // Flying
        if(inputHover)
        {   _playerSoundControl.PlaySFX_Flying();   }
        else
        {   _playerSoundControl.StopSFX_Flying();   }
    }
    // Updates -------------------------------------------------------end
    
    void PerformFlight(InputAction.CallbackContext context)
    {
        if(!inputHover)     {inputHover = true;   } //Turn on Flight
        else                {inputHover = false;  } //Turn off Flight
    }

    void PerformJump(InputAction.CallbackContext context) 
    {
        if(canGoodJump)
        {
            ResetJumpTimer();
            rb.AddForce(0, bestJumpForce , 0);
            _playerAnimationControl.CallJump_Animation("GoodJump");
        }
        else if(canAverageJump)
        {
            ResetJumpTimer();
            rb.AddForce(0, averagebestJumpForce , 0);
            _playerAnimationControl.CallJump_Animation("AverageJump");
        }
        else if(canWeakJump)
        {
            ResetJumpTimer();
            rb.AddForce(0, weakbestJumpForce , 0);
            _playerAnimationControl.CallJump_Animation("WeakJump");
        }
    }

    void ResetJumpTimer()
    {
        goodJumpTimer = goodJumpTimer_Reset;
        averageJumpTimer = averageJumpTimer_Reset;
        weakJumpTimer = weakJumpTimer_Reset;
        canWeakJump = false;
        canAverageJump = false;
        canGoodJump = false;
    }

    void PerformFowardDash(InputAction.CallbackContext context) 
    {
        if(canWeakJump)
        {
            ResetJumpTimer();
            startDashVelocity = rb.velocity;
            rb.AddRelativeForce(0, 0, forwardDashForce, ForceMode.VelocityChange);
            _playerAnimationControl.CallJump_Animation("GoodJump");
            Invoke("EndDash", dashDuration);
        }
    }

    void PerformLeftDash(InputAction.CallbackContext context) 
    {
        if(canWeakJump)
        {
            ResetJumpTimer();
            startDashVelocity = rb.velocity;
            rb.AddRelativeForce(-sideDashForce , 0, 0, ForceMode.VelocityChange);
            _playerAnimationControl.CallJump_Animation("GoodJump");
            Invoke("EndDash", dashDuration);
        }
    }

    void PerformRightDash(InputAction.CallbackContext context) 
    {
        if(canWeakJump)
        {
            ResetJumpTimer();
            startDashVelocity = rb.velocity;
            rb.AddRelativeForce(sideDashForce, 0, 0, ForceMode.VelocityChange);
            _playerAnimationControl.CallJump_Animation("GoodJump");
            Invoke("EndDash", dashDuration);
        }
    }

    public void EndDash()
    {
        rb.velocity = startDashVelocity;
    }

    float y_StartVelocity;
    void PerformDive(InputAction.CallbackContext context)
    {
        if(!inputDive && !_playerFootCollision.playerIsGrounded) //Turn on Dive
        {
            Reset_EndDiveBoost();
            inputDive = true;    
            y_StartVelocity = rb.velocity.y;
        }
        else if(inputDive) // Turning off Dive
        {
            inputDive = false;
        }

        void Reset_EndDiveBoost()
        {
            diveTimer = diveTimer_Reset;
            do_EndDiveBoost = false;
        }
    }

}