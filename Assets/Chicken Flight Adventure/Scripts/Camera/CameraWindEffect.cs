using UnityEngine;
using UnityEngine.InputSystem;


public class CameraWindEffect : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] PlayerBehaviour _playerBehaviour;
    [SerializeField] GameObject player;

    [Header("Screen Points References")]
    [SerializeField] GameObject topRightCorner;
    [SerializeField] GameObject topLeftCorner;
    [SerializeField] GameObject bottomRightCorner;
    [SerializeField] GameObject bottomLeftCorner;

    
    [Header("Camera References")]
    [SerializeField] CameraBehaviour _cameraBehaviour;
    [SerializeField] float cameraMoveUpSpeed;
    
    private LineRenderer lineRenderer_01;
    private LineRenderer lineRenderer_02;
    private LineRenderer lineRenderer_03;
    private LineRenderer lineRenderer_04;

    void Start()
    {
        lineRenderer_01 = topRightCorner.GetComponent<LineRenderer>();
        lineRenderer_02 = topLeftCorner.GetComponent<LineRenderer>();
        lineRenderer_03 = bottomRightCorner.GetComponent<LineRenderer>();
        lineRenderer_04 = bottomLeftCorner.GetComponent<LineRenderer>();
    }

    void Update()
    {
        MakeWindLines();
    }
    
    public void MakeWindLines()
    {
        lineRenderer_01.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;
        lineRenderer_02.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;       
        lineRenderer_03.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;
        lineRenderer_04.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;
        _cameraBehaviour.MoveCameraUp(_playerBehaviour.inputDive, cameraMoveUpSpeed);

        if(_playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled)
        {
            lineRenderer_01.SetPosition(0, player.transform.position);
            lineRenderer_01.SetPosition(1, topRightCorner.transform.position);
        
            lineRenderer_02.SetPosition(0, player.transform.position);
            lineRenderer_02.SetPosition(1, topLeftCorner.transform.position);

            lineRenderer_03.SetPosition(0, player.transform.position);
            lineRenderer_03.SetPosition(1, bottomRightCorner.transform.position);

            lineRenderer_04.SetPosition(0, player.transform.position);
            lineRenderer_04.SetPosition(1, bottomLeftCorner.transform.position);       
        }
    }
}
