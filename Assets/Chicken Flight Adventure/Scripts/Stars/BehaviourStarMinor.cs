using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourStarMinor : MonoBehaviour
{
    [SerializeField] private Collider starCollider;
    [SerializeField] private Collider lineCollider;
    [SerializeField] private Collider proximityCollider;
    [SerializeField] private MeshRenderer starMesh;
    [SerializeField] private LineRenderer lineRenderer;
    
    private GameObject _gameController;
    private GameObject _player;
    private PlayerBehaviour _playerBehaviour;
    private GameObject _playerCamera;
    private PlayerAnimationControl _playerAnimationControl;
    private PlayerSoundControl _playerSoundControl;
    private GlobalVolumeController _globalVolumeController;

    void Start()
    {
        _gameController = GameObject.Find("Game Controller");
        _player = GameObject.Find("Player_Group");
        _playerBehaviour = _player.GetComponent<PlayerBehaviour>();        
        _playerCamera = GameObject.Find("Main Camera");
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
        _playerSoundControl = _player.GetComponentInChildren<PlayerSoundControl>();
        _globalVolumeController = GameObject.Find("Global Volume").GetComponent<GlobalVolumeController>();
    }

    public void MakeLineToPlayer()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _playerBehaviour.transform.position);
        
        UpdateLineColor();
    }

    void UpdateLineColor()
    {
        float playerCamDistance = Mathf.Abs(_player.transform.position.z - _playerCamera.transform.position.z);
        float StarCamDistance = Mathf.Abs(transform.position.z - _playerCamera.transform.position.z);
        
        Color red = new Color(1, 0.06666666f, 0.1638318f);
        Color blue = new Color(0.06666667f, 0.454902f, 1);

        if(playerCamDistance - 1.5f >= StarCamDistance)
        {
            lineRenderer.startColor = red;
        }else
        {
            lineRenderer.startColor = blue;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _gameController.GetComponent<GameController>().score += 1;
            StarBoostPlayerEvent();
            DisableStar();    
            Invoke("EnableStar", 5f);
        }

        void StarBoostPlayerEvent()
        {
            _playerBehaviour.Boost();
            _playerAnimationControl.CallFlightBoost_Animation();
            _playerSoundControl.PlaySFX_CatchingStarMinor();
        }

        void DisableStar()
        {
            starCollider.enabled = false;
            lineCollider.enabled = false;
            proximityCollider.enabled = false;
            starMesh.enabled = false;
            lineRenderer.enabled = false;
            _globalVolumeController.ChangeGlobalVolumeProfile(0);
        }
    }

    void EnableStar()
    {
        starCollider.enabled = true;
        lineCollider.enabled = true;
        proximityCollider.enabled = true;
        starMesh.enabled = true;
    }
}
