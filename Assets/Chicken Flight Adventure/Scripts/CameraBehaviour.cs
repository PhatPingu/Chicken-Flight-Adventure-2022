using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerFootCollision _playerFootCollision;
    [SerializeField] private CinemachineFreeLook camera_01;
    [SerializeField] private CinemachineFreeLook camera_02;
    [SerializeField] private float cameraNormalFieldView = 80f;

    void Start()
    {
        camera_01.m_Lens.FieldOfView = cameraNormalFieldView;
        camera_02.m_Lens.FieldOfView = cameraNormalFieldView;
    }

    void Update()
    {
        /*if(_playerFootCollision.playerIsGrounded)
        {   //This is changing the values of BottomRig (Height, Radius)
            camera_01.m_Orbits[2] = new CinemachineFreeLook.Orbit(0f,16f);
            camera_02.m_Orbits[2] = new CinemachineFreeLook.Orbit(0f,16f);
        }
        else
        {
            camera_01.m_Orbits[2] = new CinemachineFreeLook.Orbit(-4f,12f);
            camera_02.m_Orbits[2] = new CinemachineFreeLook.Orbit(-4f,12f);
        }*/
    }

    public void ChangeZoom(bool choice, float ChangeZoomDistance, float ChangeZoomSpeed, float zoomOutSpeed) 
    {                                
        if      (choice == true && camera_01.m_Lens.FieldOfView <= ChangeZoomDistance)
        {   
            camera_01.m_Lens.FieldOfView = ChangeZoomDistance; 
            camera_02.m_Lens.FieldOfView = ChangeZoomDistance;
        }
        else if (choice == true && camera_01.m_Lens.FieldOfView >= ChangeZoomDistance)
        {   
            camera_01.m_Lens.FieldOfView -= ChangeZoomSpeed * Time.deltaTime; 
            camera_02.m_Lens.FieldOfView -= ChangeZoomSpeed * Time.deltaTime;
        }
        else if (choice == false && camera_01.m_Lens.FieldOfView >= cameraNormalFieldView)
        {   
            camera_01.m_Lens.FieldOfView = cameraNormalFieldView;
            camera_02.m_Lens.FieldOfView = cameraNormalFieldView; 
        }
        else
        {   camera_01.m_Lens.FieldOfView += zoomOutSpeed * Time.deltaTime;
            camera_02.m_Lens.FieldOfView += zoomOutSpeed * Time.deltaTime; 
        }
    }

    public void ResetCamera()
    {
        if(camera_01.m_Lens.FieldOfView > cameraNormalFieldView)
        {
            camera_01.m_Lens.FieldOfView -= 20f * Time.deltaTime;
            camera_02.m_Lens.FieldOfView -= 20f * Time.deltaTime;
        }
        else if(camera_01.m_Lens.FieldOfView < cameraNormalFieldView)
        {
            camera_01.m_Lens.FieldOfView += 20f * Time.deltaTime;
            camera_02.m_Lens.FieldOfView += 20f * Time.deltaTime;
        }
    }
}
