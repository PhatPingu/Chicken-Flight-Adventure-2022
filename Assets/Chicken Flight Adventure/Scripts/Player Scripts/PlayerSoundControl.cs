using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundControl : MonoBehaviour
{
    [Header("--= Sound References =--")]
    [SerializeField] PlayerFootCollision _playerFootCollision;

    [SerializeField] AudioSource audioSource;

    [Header("--= Audio Sources =--")]
    [SerializeField] AudioSource audioSource_walking;
    [SerializeField] AudioSource audioSource_flying;

    [Header("--= Sound Clips =--")]
    [SerializeField] AudioClip SFX_CatchingStarMinor;
    
    [SerializeField] AudioClip[] SFX_walking;
    [SerializeField] AudioClip[] SFX_jumping;
    [SerializeField] AudioClip[] SFX_flying;
    [SerializeField] AudioClip[] SFX_headBang;

    private bool canPlayWalking;
    private bool canPlayFlying;
    [Header("--= Sound Settings =--")]
    [SerializeField] private float currentClipLength_Walking;
    [SerializeField] private float currentClipLength_Flying;

    void Start()
    {
        currentClipLength_Walking = 0f;
        currentClipLength_Flying = 0f;
    }

    void Update()
    {
        currentClipLength_Walking -= Time.deltaTime;
        currentClipLength_Flying -= Time.deltaTime;
    }
    
    ///////////// -= CatchingStarMinor =- /////////////
    public void PlaySFX_CatchingStarMinor()
    {
        audioSource.PlayOneShot(SFX_CatchingStarMinor);    
    }

    public void Stop_CatchingStarMinor()
    {
        audioSource.Stop();
    }

    ///////////// -= Walking =- /////////////
    public void PlaySFX_Walking()
    {
        if(currentClipLength_Walking <= 0)
        {
            canPlayWalking = true;
        }

        if(canPlayWalking && _playerFootCollision.playerIsGrounded)
        {
            int randomClip = Random.Range(0, SFX_walking.Length);
            audioSource_walking.PlayOneShot(SFX_walking[randomClip]);    
            currentClipLength_Walking = SFX_walking[randomClip].length;
            canPlayWalking = false;
        }
    }

    public void StopSFX_Walking()
    {
        audioSource_walking.Stop();
        canPlayWalking = true;
    }

    ///////////// -= Flying =- /////////////
    public void PlaySFX_Flying()
    {
        if(currentClipLength_Flying <= 0)
        {
            canPlayFlying = true;
        }

        if(canPlayFlying)
        {
            int randomClip = Random.Range(0, SFX_flying.Length);
            audioSource_flying.PlayOneShot(SFX_flying[randomClip]);    
            currentClipLength_Flying = SFX_flying[randomClip].length;
            canPlayFlying = false;

        }

        if(canPlayFlying && _playerFootCollision.playerIsGrounded)
        {
            int randomClip = Random.Range(0, SFX_jumping.Length);
            audioSource_flying.PlayOneShot(SFX_jumping[randomClip]);  
        }        
    }

    public void StopSFX_Flying()
    {
        audioSource_flying.Stop();
        canPlayFlying = true;
    }

    ///////////// -= HeadBang =- /////////////
    public void PlaySFX_HeadBang()
    {
        int randomClip = Random.Range(0, SFX_headBang.Length);
        audioSource.PlayOneShot(SFX_headBang[randomClip]);    
        currentClipLength_Flying = SFX_headBang[randomClip].length;
    }

    public void StopSFX_HeadBang()
    {
        audioSource.Stop();
    }
}
