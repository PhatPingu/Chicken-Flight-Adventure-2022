using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerLevelStatus.StarColor colorGroup;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(colorGroup == PlayerLevelStatus.StarColor.Blue)
            {
                PlayerLevelStatus.blueActive = true;
            }
            if(colorGroup == PlayerLevelStatus.StarColor.Red)
            {
                PlayerLevelStatus.redActive = true;
            }
        }

        gameObject.SetActive(false);
    }
}
