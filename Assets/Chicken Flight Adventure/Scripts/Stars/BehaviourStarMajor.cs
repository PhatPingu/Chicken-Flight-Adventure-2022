using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourStarMajor : MonoBehaviour
{
    private GameObject _gameController;
    private GameObject _player;
    private PlayerBehaviour _playerBehaviour;
    private PlayerSoundControl _playerSoundControl;
    

    void Start()
    {
        _gameController = GameObject.Find("Game Controller");
        _player = GameObject.Find("Player_Group");
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();        
        _playerSoundControl = _player.GetComponentInChildren<PlayerSoundControl>();
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            _gameController.GetComponent<GameController>().score += 10;
            StarBoostPlayerEvent();
        }

        void StarBoostPlayerEvent()
        {
            _playerBehaviour.Boost();
            _playerSoundControl.PlaySFX_CatchingStarMinor();
        }
    }
}
