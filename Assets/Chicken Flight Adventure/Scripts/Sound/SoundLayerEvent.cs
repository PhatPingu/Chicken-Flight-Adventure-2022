using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLayerEvent : MonoBehaviour
{
    [SerializeField] SoundController _soundController;

    [SerializeField] float baseLayer;
    [SerializeField] float layer01;
    [SerializeField] float layer02;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _soundController.SetMusicLayers_NewVolume(baseLayer, layer01, layer02);      
        }
    }

    // This is stopping the OnTrigger from playing.
    // Could maybe solver by spacing out the Collision Zones
    /*void OnTriggerExit(Collider other)  
    {
        if(other.tag == "Player")
        {
            _soundController.SetAllMusic_DefaultVolume();
        }
    }*/
}
