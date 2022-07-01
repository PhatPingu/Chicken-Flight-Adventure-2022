using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource baseLayer;
    [SerializeField] AudioSource layer01;
    [SerializeField] AudioSource layer02;

    public void StartAllMusicLayers()
    {
        intro.Play();
        baseLayer.PlayDelayed(intro.clip.length);
        layer01.PlayDelayed(intro.clip.length);
        layer02.PlayDelayed(intro.clip.length);
        
        SetAllMusic_DefaultVolume();
    }

    public void SetAllMusic_DefaultVolume()
    {
        intro.volume        = 0.1f;
        baseLayer.volume    = 0.1f;
        layer01.volume      = 0f;
        layer02.volume      = 0f;
    }

    public void SetMusicLayers_NewVolume(float vol_baselayer, float vol_layer01, float vol_layer02)
    {
        baseLayer.volume    = vol_baselayer;
        layer01.volume      = vol_layer01;
        layer02.volume      = vol_layer02;
    }
}
