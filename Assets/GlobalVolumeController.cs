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
        ChangeGlobalVolumeProfile(false);
    }

    public void ChangeGlobalVolumeProfile(bool enabled)
    {
        if (enabled)
        {
            _globalVolume.profile = _volumeProfiles[1];
        }
        else
        {
            _globalVolume.profile = _volumeProfiles[0];
        }
    }
}