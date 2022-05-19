using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArrayTool
{
    public float xPosMin, xPosMax;
    public float yPos, yPosOffset;
    public float zPosMin, zPosMax;
}



public class ArrayCreatorTool : MonoBehaviour
{

    [Header("Coords")]
    [SerializeField] private ArrayTool[] line_01;
    [SerializeField] private ArrayTool[] line_02;
    [SerializeField] private ArrayTool[] line_03;
    [SerializeField] private ArrayTool[] line_04;


    [Header("Objects")]
    [SerializeField] private GameObject[] object_line_01;
    [SerializeField] private GameObject[] object_line_02;
    [SerializeField] private GameObject[] object_line_03;
    [SerializeField] private GameObject[] object_line_04;

    void Start()
    {
        UpdateObjectList();

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
            line_01[i].xPosMin = object_line_01[i].transform.position.x;
            line_01[i].xPosMax = object_line_01[i].transform.position.x;
            line_01[i].yPos = object_line_01[i].transform.position.y;
            line_01[i].yPosOffset = object_line_01[i].transform.position.y;
            line_01[i].zPosMin = object_line_01[i].transform.position.z;
            line_01[i].zPosMax = object_line_01[i].transform.position.z;
        }
    }


}
