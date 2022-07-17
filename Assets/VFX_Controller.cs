using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Controller : MonoBehaviour
{
    [Header("VFX References")]
    public ParticleSystem starPuff_FX;
    
    public ParticleSystem diveLong_FX;
    public ParticleSystem diveShort_FX01;
    public ParticleSystem diveShort_FX02;
    
    public ParticleSystem headCollision_FX;

    public ParticleSystem jumpSmoke_Good;
    public ParticleSystem jumpSmoke_Average;
    public ParticleSystem jumpSmoke_Weak;

    public ParticleSystem waterSplash;
}
