using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationControl : MonoBehaviour
{
    public Rigidbody _rigidbody;

    [Header("Component References")]
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    
    [Header("VFX References")]
    [SerializeField] private ParticleSystem starPuff_FX;
    [SerializeField] private ParticleSystem jump_FX;
    [SerializeField] private ParticleSystem diveLong_FX;
    [SerializeField] private ParticleSystem diveShort_FX01;
    [SerializeField] private ParticleSystem diveShort_FX02;
    [SerializeField] private ParticleSystem headCollision_FX;

    [SerializeField] private ParticleSystem jumpSmoke_Good;
    [SerializeField] private ParticleSystem jumpSmoke_Average;
    [SerializeField] private ParticleSystem jumpSmoke_Weak;

    [Header("Attributes")]
    [SerializeField] private float timeTo_AFK = 15f;
    [SerializeField] private float velocityTo_FallingAnimation;

    private float xInput;
    private float yInput;
    private bool isFlying;

    private bool haltAFK;
        
    private float noInputCheck;
    private PlayerInput playerInput;
    private InputAction jumpAction;
    private InputAction flyAction;
    private InputAction movementAction;
    

    void Awake()
    {
        playerInput =       GetComponent<PlayerInput>();
        jumpAction =        playerInput.actions["Jump"];      //not used
        flyAction =         playerInput.actions["Fly"];       //not used
        movementAction =    playerInput.actions["Movement"];
    }

    void Start()
    {
        noInputCheck = timeTo_AFK; //Default is 15f
    }

    void Update()
    {
        xInput = movementAction.ReadValue<Vector2>().x;
        yInput = movementAction.ReadValue<Vector2>().y;
        isFlying = _playerBehaviour.inputHover;
        
        AFK_Controller();
        FallingDown_Controller();
        MovementAnimationController();

        // Anim Teste:
        if(Input.GetKeyDown(KeyCode.Q))animator.SetTrigger("Celebrating"); 
    }

    public void CallFlightBoost_Animation() 
    {
        if( animator.GetBool("CelebratingAir") == false 
        &&   animator.GetBool("Dive") == false) 
        {
            animator.SetTrigger("AirBoost");
            starPuff_FX.Play();
        }
    }

    public void CallJump_Animation(string typeJump)
    {
        animator.SetTrigger("Jump");
        
        if (typeJump == "GoodJump"      )
        {   jumpSmoke_Good.Play();      }

        if (typeJump == "AverageJump"   )
        {   jumpSmoke_Average.Play();   }
        
        if (typeJump == "WeakJump"      )
        {   jumpSmoke_Weak.Play();      }
    }

    public void CallHeadCollision_Animation() 
    {
        animator.Play("HeadImpact");
        headCollision_FX.Play();
    }

    public void FallingDown_Controller() 
    {
        if(_rigidbody.velocity.y < velocityTo_FallingAnimation)
        {animator.SetBool("FallingDown", true);}
        else 
        {animator.SetBool("FallingDown", false);}
    }
    
    public void Dive_Animation(bool choice)
    {
        animator.SetBool("Dive", choice);
        _cameraBehaviour.ChangeZoom(choice, 35f, 20f, 30f);
        
        if(choice == true) 
        {
            diveShort_FX01.Play();
            diveShort_FX02.Play();
        }
        else
        {
            diveShort_FX01.Stop();
            diveShort_FX02.Stop();
        }

        if(choice == true && _playerBehaviour.do_EndDiveBoost)
        {
            diveLong_FX.Play();
        }
        else
        {
            diveLong_FX.Stop();
        }
    }

    public void CelebrateAir_Animation() // This can be imprved
    {
        animator.Play("CelebratingAir");
        Invoke("ResetBool", 2f);
    }
    void ResetBool() {animator.SetBool("CelebratingAir", false);}
    
    void AFK_Controller()
    {   
        if(!playerInput.inputIsActive)
        {   haltAFK = false;    }
        else if (playerInput.inputIsActive)
        {   haltAFK = true;     }
    
        if(haltAFK) 
        {   noInputCheck = timeTo_AFK;}
        noInputCheck -= Time.deltaTime;

        if(noInputCheck < 0) 
        {   animator.SetBool("Turn Head", true);}
        else
        {   animator.SetBool("Turn Head", false);}
    }

    void CallAnimation(bool test, string animation) => animator.SetBool(animation, test);

    void MovementAnimationController() 
    {        
        bool forwardInput   =   yInput > 0;
        bool backwardInput  =   yInput < 0;
        bool leftInput      =   xInput < 0;
        bool rightInput     =   xInput > 0;
        bool xInputNull     =   xInput == 0;
        bool yInputNull     =   yInput == 0;  

        // Flight Detection ------------------------------------------------------start
        CallAnimation(isFlying, "Fly");
        CallAnimation(isFlying && forwardInput  && xInputNull, "Fly Forward");
        CallAnimation(isFlying && backwardInput && xInputNull, "Fly Back");
        CallAnimation(isFlying && rightInput    && yInputNull, "Fly Right");
        CallAnimation(isFlying && leftInput     && yInputNull, "Fly Left");
        //          Diagonal Flight ----------------------------------------------start
        CallAnimation(isFlying && forwardInput  && rightInput, "Fly_DiagRightForward");
        CallAnimation(isFlying && forwardInput  && leftInput,  "Fly_DiagLeftForward");
        CallAnimation(isFlying && backwardInput && rightInput, "Fly_DiagRightBack");
        CallAnimation(isFlying && backwardInput && leftInput,  "Fly_DiagLeftBack");
        //          Diagonal Flight ------------------------------------------------end
        // Flight Detection --------------------------------------------------------end

        // Walk Detection --------------------------------------------------------start
        CallAnimation(!isFlying && forwardInput  && xInputNull, "Walk Forward");
        CallAnimation(!isFlying && backwardInput && xInputNull, "Walk Back");
        CallAnimation(!isFlying && rightInput    && yInputNull, "Walk Right");
        CallAnimation(!isFlying && leftInput     && yInputNull, "Walk Left");
        //          Diagona Walk -------------------------------------------------start
        CallAnimation(!isFlying && forwardInput  && rightInput, "Walk_DiagRightForward");
        CallAnimation(!isFlying && forwardInput  && leftInput,  "Walk_DiagLeftForward");
        CallAnimation(!isFlying && backwardInput && rightInput, "Walk_DiagRightBack");
        CallAnimation(!isFlying && backwardInput && leftInput,  "Walk_DiagLeftBack");
        //          Diagona Walk ---------------------------------------------------end






        /*
        if(isFlying)
        {   animator.SetBool("Fly", true);  } 
        else
        {   animator.SetBool("Fly", false); }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x == 0)
        {   animator.SetBool("Fly Forward", true);  } 
        else
        {   animator.SetBool("Fly Forward", false); }
        
        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x == 0)
        {   animator.SetBool("Fly Back", true);     } 
        else
        {   animator.SetBool("Fly Back", false);    }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().x > 0
        && movementAction.ReadValue<Vector2>().y == 0)
        {   animator.SetBool("Fly Right", true);    } 
        else
        {   animator.SetBool("Fly Right", false);   }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().x < 0
        && movementAction.ReadValue<Vector2>().y == 0)
        {   animator.SetBool("Fly Left", true);     } 
        else
        {   animator.SetBool("Fly Left", false);    }

        //          Diagona Flight -----------------------------------------------start
        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x > 0) 
        {   animator.SetBool("Fly_DiagRightForward", true);  } 
        else
        {   animator.SetBool("Fly_DiagRightForward", false); }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x < 0) 
        {   animator.SetBool("Fly_DiagLeftForward", true);  } 
        else
        {   animator.SetBool("Fly_DiagLeftForward", false); }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x > 0) 
        {   animator.SetBool("Fly_DiagRightBack", true);  } 
        else
        {   animator.SetBool("Fly_DiagRightBack", false); }

        if(_playerBehaviour.inputHover 
        && movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x < 0) 
        {   animator.SetBool("Fly_DiagLeftBack", true);  } 
        else
        {   animator.SetBool("Fly_DiagLeftBack", false); }*/
        //          Diagona Flight -------------------------------------------------end
        // Flight Detection --------------------------------------------------------end


        // Walk Detection ----------------------------------------------------------end
/*
        if(movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x == 0)
        {   animator.SetBool("Fly", true);  }

        if(movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x == 0)
        {   animator.SetBool("Walk Back", true);
        }   else    {animator.SetBool("Walk Back", false);}

        if(movementAction.ReadValue<Vector2>().x > 0
        && movementAction.ReadValue<Vector2>().y == 0)
        {   animator.SetBool("Walk Right", true);
        }   else    {animator.SetBool("Walk Right", false);}

        if(movementAction.ReadValue<Vector2>().x < 0
        && movementAction.ReadValue<Vector2>().y == 0)
        {   animator.SetBool("Walk Left", true); 
        }   else    {animator.SetBool("Walk Left", false);}

        //          Diagona Walk -------------------------------------------------start
        if(movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x > 0) 
        {   animator.SetBool("Walk_DiagRightForward", true);  } 
        else
        {   animator.SetBool("Walk_DiagRightForward", false); }

        if(movementAction.ReadValue<Vector2>().y > 0
        && movementAction.ReadValue<Vector2>().x < 0) 
        {   animator.SetBool("Walk_DiagLeftForward", true);  } 
        else
        {   animator.SetBool("Walk_DiagLeftForward", false); }

        if(movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x > 0) 
        {   animator.SetBool("Walk_DiagRightBack", true);  } 
        else
        {   animator.SetBool("Walk_DiagRightBack", false); }

        if(movementAction.ReadValue<Vector2>().y < 0
        && movementAction.ReadValue<Vector2>().x < 0) 
        {   animator.SetBool("Walk_DiagLeftBack", true);  } 
        else
        {   animator.SetBool("Walk_DiagLeftBack", false); }*/
        //          Diagona Walk ---------------------------------------------------end
        // Walk Detection ----------------------------------------------------------end
    }
}