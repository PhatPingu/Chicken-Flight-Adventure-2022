using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[System.Serializable]
public struct ArrayTool
{
    public float xPosMin, xPosMax;
    public float yPos, yPosOffset;
    public float zPosMin, zPosMax;
}

[System.Serializable]
public struct Variation
{
    public float xVariation;
    public float zVariation;
}

[ExecuteInEditMode]
public class ArrayCreatorTool : MonoBehaviour
{
    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }
    
    [Header("### Variation ###")]
    [SerializeField] private Variation[] variation_line_01;
    [SerializeField] private Variation[] variation_line_02;
    [SerializeField] private Variation[] variation_line_03;
    [SerializeField] private Variation[] variation_line_04;


    [Header("### Coords ###")]
    [SerializeField] private ArrayTool[] line_01;
    [SerializeField] private ArrayTool[] line_02;
    [SerializeField] private ArrayTool[] line_03;
    [SerializeField] private ArrayTool[] line_04;


    [Header("### Objects ###")]
    [SerializeField] private GameObject[] object_line_01;
    [SerializeField] private GameObject[] object_line_02;
    [SerializeField] private GameObject[] object_line_03;
    [SerializeField] private GameObject[] object_line_04;

    void Update()
    {
        UpdateObjectList();
        UpdateCoordinates();
    }

    void UpdateObjectList()
    {
        for (int i = 0; i < object_line_01.Length; i++)
        {
            object_line_01[i] = transform.GetChild(0).GetChild(i).gameObject;
        }

        for (int i = 0; i < object_line_02.Length; i++)
        {
            object_line_02[i] = transform.GetChild(1).GetChild(i).gameObject;
        }

        for (int i = 0; i < object_line_03.Length; i++)
        {
            object_line_03[i] = transform.GetChild(2).GetChild(i).gameObject;
        }

        for (int i = 0; i < object_line_04.Length; i++)
        {
            object_line_04[i] = transform.GetChild(3).GetChild(i).gameObject;
        }
    }

    void UpdateCoordinates()
    {
        for (int i = 0; i < line_01.Length; i++)
        {
            line_01[i].xPosMin = object_line_01[i].transform.position.x - variation_line_01[i].xVariation;
            line_01[i].xPosMax = object_line_01[i].transform.position.x + variation_line_01[i].xVariation;
            line_01[i].yPos = object_line_01[i].transform.position.y;
            line_01[i].yPosOffset = 0f;
            line_01[i].zPosMin = object_line_01[i].transform.position.z - variation_line_01[i].zVariation;
            line_01[i].zPosMax = object_line_01[i].transform.position.z + variation_line_01[i].zVariation;
        }

        for (int i = 0; i < line_02.Length; i++)
        {
            line_02[i].xPosMin = object_line_02[i].transform.position.x - variation_line_02[i].xVariation;
            line_02[i].xPosMax = object_line_02[i].transform.position.x + variation_line_02[i].xVariation;
            line_02[i].yPos = object_line_02[i].transform.position.y;
            line_02[i].yPosOffset = 0f;
            line_02[i].zPosMin = object_line_02[i].transform.position.z - variation_line_02[i].zVariation;
            line_02[i].zPosMax = object_line_02[i].transform.position.z + variation_line_02[i].zVariation;
        }

        for (int i = 0; i < line_03.Length; i++)
        {
            line_03[i].xPosMin = object_line_03[i].transform.position.x - variation_line_03[i].xVariation;
            line_03[i].xPosMax = object_line_03[i].transform.position.x + variation_line_03[i].xVariation;
            line_03[i].yPos = object_line_03[i].transform.position.y;
            line_03[i].yPosOffset = 0f;
            line_03[i].zPosMin = object_line_03[i].transform.position.z - variation_line_03[i].zVariation;
            line_03[i].zPosMax = object_line_03[i].transform.position.z + variation_line_03[i].zVariation;
        }

        for (int i = 0; i < line_04.Length; i++)
        {
            line_04[i].xPosMin = object_line_04[i].transform.position.x - variation_line_04[i].xVariation;
            line_04[i].xPosMax = object_line_04[i].transform.position.x + variation_line_04[i].xVariation;
            line_04[i].yPos = object_line_04[i].transform.position.y;
            line_04[i].yPosOffset = 0f;
            line_04[i].zPosMin = object_line_04[i].transform.position.z - variation_line_04[i].zVariation;
            line_04[i].zPosMax = object_line_04[i].transform.position.z + variation_line_04[i].zVariation;
        }

    }

    /*[ContextMenu("Create Spawner")]
    void CreateSpawner()
    {
        SpawnDataStruct Spawner_new = EditorApplication.ExecuteMenuItem()
        
        for (int i = 0; i < line_01.Length; i++)
        {
            Spawner_new.line_01[i].xPosMin      = line_01[i].xPosMin;
            Spawner_new.line_01[i].xPosMax      = line_01[i].xPosMax;
            Spawner_new.line_01[i].yPos         = line_01[i].yPos;
            Spawner_new.line_01[i].yPosOffset   = line_01[i].yPosOffset;
            Spawner_new.line_01[i].zPosMin      = line_01[i].zPosMin;
            Spawner_new.line_01[i].zPosMax      = line_01[i].zPosMax;
        }

        for (int i = 0; i < line_02.Length; i++)
        {
            Spawner_new.line_02[i].xPosMin      = line_02[i].xPosMin;
            Spawner_new.line_02[i].xPosMax      = line_02[i].xPosMax;
            Spawner_new.line_02[i].yPos         = line_02[i].yPos;
            Spawner_new.line_02[i].yPosOffset   = line_02[i].yPosOffset;
            Spawner_new.line_02[i].zPosMin      = line_02[i].zPosMin;
            Spawner_new.line_02[i].zPosMax      = line_02[i].zPosMax;
        }

        for (int i = 0; i < line_03.Length; i++)
        {
            Spawner_new.line_03[i].xPosMin      = line_03[i].xPosMin;
            Spawner_new.line_03[i].xPosMax      = line_03[i].xPosMax;
            Spawner_new.line_03[i].yPos         = line_03[i].yPos;
            Spawner_new.line_03[i].yPosOffset   = line_03[i].yPosOffset;
            Spawner_new.line_03[i].zPosMin      = line_03[i].zPosMin;
            Spawner_new.line_03[i].zPosMax      = line_03[i].zPosMax;
        }

        for (int i = 0; i < line_04.Length; i++)
        {
            Spawner_new.line_04[i].xPosMin      = line_04[i].xPosMin;
            Spawner_new.line_04[i].xPosMax      = line_04[i].xPosMax;
            Spawner_new.line_04[i].yPos         = line_04[i].yPos;
            Spawner_new.line_04[i].yPosOffset   = line_04[i].yPosOffset;
            Spawner_new.line_04[i].zPosMin      = line_04[i].zPosMin;
            Spawner_new.line_04[i].zPosMax      = line_04[i].zPosMax;
        }
    }*/

}
