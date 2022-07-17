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
        // This was used to change the boundrie of the camera from Cameramachine 
        // (How much it can orbit in the y-axis)
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
    
    public void SetZoomDistance()
    {
        camera_01.m_Lens.FieldOfView = cameraNormalFieldView * 0.5f;
        camera_02.m_Lens.FieldOfView = cameraNormalFieldView * 0.5f; 
    }

    public void ChangeZoom(bool choice, float ChangeZoomDistance, float ChangeZoomSpeed, float ReturnZoomSpeed)
    {
        if(choice)
        {
            if( cameraNormalFieldView > ChangeZoomDistance &&  camera_01.m_Lens.FieldOfView <= ChangeZoomDistance
            ||  cameraNormalFieldView < ChangeZoomDistance &&  camera_01.m_Lens.FieldOfView >= ChangeZoomDistance)
            {   
                camera_01.m_Lens.FieldOfView = ChangeZoomDistance;
                camera_02.m_Lens.FieldOfView = ChangeZoomDistance; 
            }
            else if (camera_01.m_Lens.FieldOfView >= ChangeZoomDistance)
            {   
                camera_01.m_Lens.FieldOfView -= ChangeZoomSpeed * Time.deltaTime; 
                camera_02.m_Lens.FieldOfView -= ChangeZoomSpeed * Time.deltaTime;
            }
            else if (camera_01.m_Lens.FieldOfView <= ChangeZoomDistance)
            {   
                if(camera_01.m_Lens.FieldOfView >= ChangeZoomDistance) return;

                camera_01.m_Lens.FieldOfView += ChangeZoomSpeed * Time.deltaTime; 
                camera_02.m_Lens.FieldOfView += ChangeZoomSpeed * Time.deltaTime;
            }
        }
        else if (choice == false)
        {
            if( cameraNormalFieldView > ChangeZoomDistance &&  camera_01.m_Lens.FieldOfView >= cameraNormalFieldView
            ||  cameraNormalFieldView < ChangeZoomDistance &&  camera_01.m_Lens.FieldOfView <= cameraNormalFieldView)
            {   
                camera_01.m_Lens.FieldOfView = cameraNormalFieldView;
                camera_02.m_Lens.FieldOfView = cameraNormalFieldView;
            }
            else if (camera_01.m_Lens.FieldOfView >= cameraNormalFieldView)
            {   
                camera_01.m_Lens.FieldOfView -= ReturnZoomSpeed * Time.deltaTime;
                camera_02.m_Lens.FieldOfView -= ReturnZoomSpeed * Time.deltaTime;
            }
            else if (camera_01.m_Lens.FieldOfView <= cameraNormalFieldView)
            {   
                camera_01.m_Lens.FieldOfView += ReturnZoomSpeed * Time.deltaTime;
                camera_02.m_Lens.FieldOfView += ReturnZoomSpeed * Time.deltaTime;
            }
        }
    }

    public void MoveCameraUp(bool choice, float moveSpeed)
    {
        if(choice)
        {
          camera_01.m_YAxis.Value += moveSpeed;
          camera_02.m_YAxis.Value += moveSpeed;  
        } 
    }

}
