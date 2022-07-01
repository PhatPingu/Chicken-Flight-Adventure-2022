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
        
        StartAllMusicVolume();
    }

    void StartAllMusicVolume()
    {
        intro.volume = 0.3f;
        baseLayer.volume = 0.3f;
        layer01.volume = 0f;
        layer02.volume = 0f;
    }
}
