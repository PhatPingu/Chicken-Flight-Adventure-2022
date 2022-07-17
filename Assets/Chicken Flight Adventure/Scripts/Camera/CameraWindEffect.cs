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
    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject right;
    [SerializeField] GameObject left;

    
    [Header("Camera References")]
    [SerializeField] CameraBehaviour _cameraBehaviour;
    [SerializeField] float cameraMoveUpSpeed;
    
    private LineRenderer lineRenderer_01;
    private LineRenderer lineRenderer_02;
    private LineRenderer lineRenderer_03;
    private LineRenderer lineRenderer_04;
    private LineRenderer lineRenderer_05;
    private LineRenderer lineRenderer_06;
    private LineRenderer lineRenderer_07;
    private LineRenderer lineRenderer_08;

    void Start()
    {
        lineRenderer_01 = topRightCorner.GetComponent<LineRenderer>();
        lineRenderer_02 = topLeftCorner.GetComponent<LineRenderer>();
        lineRenderer_03 = bottomRightCorner.GetComponent<LineRenderer>();
        lineRenderer_04 = bottomLeftCorner.GetComponent<LineRenderer>();
        lineRenderer_05 = top.GetComponent<LineRenderer>();
        lineRenderer_06 = bottom.GetComponent<LineRenderer>();
        lineRenderer_07 = right.GetComponent<LineRenderer>();
        lineRenderer_08 = left.GetComponent<LineRenderer>();
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
        lineRenderer_05.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;
        lineRenderer_06.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;       
        lineRenderer_07.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;
        lineRenderer_08.enabled = _playerBehaviour.inputDive && _playerBehaviour.EndDiveBoost_enabled;

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

            lineRenderer_05.SetPosition(0, player.transform.position);
            lineRenderer_05.SetPosition(1, top.transform.position);
        
            lineRenderer_06.SetPosition(0, player.transform.position);
            lineRenderer_06.SetPosition(1, bottom.transform.position);

            lineRenderer_07.SetPosition(0, player.transform.position);
            lineRenderer_07.SetPosition(1, right.transform.position);

            lineRenderer_08.SetPosition(0, player.transform.position);
            lineRenderer_08.SetPosition(1, left.transform.position);      
        }
    }
}
