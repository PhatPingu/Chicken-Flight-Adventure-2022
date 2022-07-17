using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeController : MonoBehaviour
{
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private VolumeProfile[] _volumeProfiles;

    void Start()
    {
        ChangeGlobalVolumeProfile(0);
    }

    public void ChangeGlobalVolumeProfile(int volumeIndex)
    {
        if (enabled)
        {
            _globalVolume.profile = _volumeProfiles[volumeIndex];
        }
        else
        {
            _globalVolume.profile = _volumeProfiles[volumeIndex];
        }
    }
}