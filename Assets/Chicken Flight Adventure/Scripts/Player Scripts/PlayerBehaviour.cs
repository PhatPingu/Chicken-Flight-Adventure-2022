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
    [SerializeField] private CapsuleCollider _hoverChikenCollider;

    [SerializeField] private float hoverForce = 10f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float pushDownForce = -700f;
    
    [Header("Dive Attributes")]
    [SerializeField] private float diveForce = 10f;
    [SerializeField] private float forwardDiveForce = 400f;
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
    
    //private Vector3 startDashVelocity;

    [Header("Movement Attributes")]
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float WalkSpeedHovering;
    public float currentWalkSpeed = 0.15f;
    
    public float bestJumpForce = 800f;
    public float averagebestJumpForce = 600f;
    public float weakbestJumpForce = 400f;
    
    //check if variables under should be public?
    public bool canGoodJump = true;
    public bool canAverageJump = true;
    public bool canWeakJump = true;
    public bool notOnWater = true;
    
    public bool inputHover;
    public bool i_frameActive;

    public bool EndDiveBoost_enabled;
    public bool inputDive;
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
    }

    void Start()
    {
        movementAction.performed += _           => PlayerMovementInput();
        cameraAction.performed += _             => PlayerRotateInput();
        jumpAction.performed += context         => PerformJump(context);  
        flyAction.performed += context          => PerformHover(context);
        diveAction.performed += context         => PerformDive(context);

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
        VelocityContorol();
        
        if(!i_frameActive)
        {
            PlayerMovementInput();       
            PlayerRotateInput();
            HoverInput();
        }
    }

    void Update()
    {
        CanFlyController();
        CanDiveController();
        SoundController();

        if(!i_frameActive)
        {
            DiveInput();
        }
    }
    // Updates ------------------------------------------------------start
    void PlayerMovementInput()
    {
        rb.MovePosition(transform.position 
        + (transform.forward * movementAction.ReadValue<Vector2>().y * currentWalkSpeed) 
        + (transform.right   * movementAction.ReadValue<Vector2>().x * currentWalkSpeed));
    }

    void PlayerRotateInput()
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
            (0, cameraAction.ReadValue<Vector2>().x * mouseSensitivity * 3f, 0)));
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

    bool firstPush = true;
    bool needsDelay = true;
    void HoverInput()
    {
        if (inputHover) 
        {
            if(firstPush && needsDelay)
            {
                firstPush = false;
                needsDelay = false;
                rb.AddForce(0, forwardDashForce, 0);    
            }
            else
            {
                rb.AddForce(0, hoverForce    * Time.deltaTime, 0);
            }

            _hoverChikenCollider.center = new Vector3(0,0.1f,0.05f);
            currentWalkSpeed = WalkSpeedHovering;
        }
        else            
        {
            firstPush = true;
            rb.AddForce(0, pushDownForce * Time.deltaTime, 0);
            _hoverChikenCollider.center = new Vector3(0,0.234f,0.05f);
            currentWalkSpeed = WalkSpeed;
        }
        
        if(!needsDelay) Invoke("ResetDelay", 2f);
    }
    void ResetDelay()   { needsDelay = true; }

    void VelocityContorol()
    {
        if (rb.velocity.y > 15f)    { rb.velocity = new Vector3(0,14.9f,0); }
        if (rb.velocity.y < -50f)   { rb.velocity = new Vector3(0, -50f,0); }
    }
    
    void CanDiveController()  
    {    
        diveTimer -= Time.deltaTime;
        if (diveTimer <= 0)         { EndDiveBoost_enabled = true; }
    }

    void DiveInput()
    {
        if(inputDive)
        {
            _playerAnimationControl.Dive_Animation(true);
            rb.velocity = new Vector3(0f, -diveForce, 0f);
            rb.AddRelativeForce(0, 0, forwardDiveForce);  
            isDiving = true;
        } 
        else
        {
            _playerAnimationControl.Dive_Animation(false);
        }

        if(!inputDive && isDiving)
        {
            if(EndDiveBoost_enabled) 
            {
                rb.velocity = new Vector3(0f, y_StartVelocity + endDiveBoostForce, 0f); 
                EndDiveBoost_enabled = false;
            }
            isDiving = false;
        }
        else if(!inputDive && isDiving) // WEIRD!!!! WTF is this? Does nothing? -- !!!!!!!!!!!!!!
        {
            if(EndDiveBoost_enabled) 
            {
                EndDiveBoost_enabled = false;
            }
            isDiving = false;
        }
    }
    
    void SoundController()        // REFACTOR THIS
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
    
    void PerformHover(InputAction.CallbackContext context)
    {
        if(!inputHover)     {inputHover = true;   } //Turn on Flight
        else                {inputHover = false;  } //Turn off Flight
    }

    void PerformJump(InputAction.CallbackContext context) 
    {
        if(canGoodJump && notOnWater)
        {
            ResetJumpTimer();
            rb.AddForce(0, bestJumpForce , 0);
            _playerAnimationControl.CallJump_Animation("GoodJump");
        }
        else if(canAverageJump && notOnWater)
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
            EndDiveBoost_enabled = false;
        }
    }

}