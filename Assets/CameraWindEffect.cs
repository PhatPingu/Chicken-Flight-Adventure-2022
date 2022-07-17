using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWindEffect : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject topRightCorner;
    [SerializeField] GameObject topLeftCorner;
    [SerializeField] GameObject bottomRightCorner;
    [SerializeField] GameObject bottomLeftCorner;
    [SerializeField] PlayerBehaviour _playerBehaviour;

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
