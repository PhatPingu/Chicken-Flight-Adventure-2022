using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoGenerator : MonoBehaviour
{
    [SerializeField] LevelSpawnsManager _levelSpawnsManager;
    [SerializeField] Vector3 position;
    [SerializeField] Vector3 size;

    void OnDrawGizmos()
    {
        var _spawner = _levelSpawnsManager.spawner;
        size = new Vector3(4,1,4);

        for (int i = 0; i < _levelSpawnsManager.spawner.Length; i++)
        {
            for (int w =0; w < _spawner[i].line_01.Length; w++)
            {
            float x = (_spawner[i].line_01[w].xPosMin + _spawner[i].line_01[w].xPosMax)     * 0.5f;
            float y = (_spawner[i].line_01[w].yPos    + _spawner[i].line_01[w].yPosOffset)  ;
            float z = (_spawner[i].line_01[w].zPosMin + _spawner[i].line_01[w].zPosMax)     * 0.5f;                
            position = new Vector3(x, y, z) + _spawner[i].offsetStartLocation;

            Gizmos.color = Color.green;
            Gizmos.DrawCube(position, size);
            }

            for (int w =0; w < _spawner[i].line_02.Length; w++)
            {
            float x = (_spawner[i].line_02[w].xPosMin + _spawner[i].line_02[w].xPosMax)     * 0.5f;
            float y = (_spawner[i].line_02[w].yPos    + _spawner[i].line_02[w].yPosOffset)  ;
            float z = (_spawner[i].line_02[w].zPosMin + _spawner[i].line_02[w].zPosMax)     * 0.5f;                
            position = new Vector3(x, y, z) + _spawner[i].offsetStartLocation;

            Gizmos.color = Color.red;
            Gizmos.DrawCube(position, size);
            } 

            for (int w =0; w < _spawner[i].line_03.Length; w++)
            {
            float x = (_spawner[i].line_03[w].xPosMin + _spawner[i].line_03[w].xPosMax)     * 0.5f;
            float y = (_spawner[i].line_03[w].yPos    + _spawner[i].line_03[w].yPosOffset)  ;
            float z = (_spawner[i].line_03[w].zPosMin + _spawner[i].line_03[w].zPosMax)     * 0.5f;                
            position = new Vector3(x, y, z) + _spawner[i].offsetStartLocation;

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(position, size);
            } 

            for (int w =0; w < _spawner[i].line_04.Length; w++)
            {
            float x = (_spawner[i].line_04[w].xPosMin + _spawner[i].line_04[w].xPosMax)     * 0.5f;
            float y = (_spawner[i].line_04[w].yPos    + _spawner[i].line_04[w].yPosOffset)  ;
            float z = (_spawner[i].line_04[w].zPosMin + _spawner[i].line_04[w].zPosMax)     * 0.5f;                
            position = new Vector3(x, y, z) + _spawner[i].offsetStartLocation;

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(position, size);
            }         
        }
        
    }    
}

