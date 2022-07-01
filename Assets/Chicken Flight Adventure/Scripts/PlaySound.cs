using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] SoundController _soundController;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _soundController.StartAllMusicLayers();
        }
    }
}
