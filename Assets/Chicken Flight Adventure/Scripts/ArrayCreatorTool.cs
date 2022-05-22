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
    [Header("### Variation ###")]
    [SerializeField] private Variation[] variation_line_01;
    [SerializeField] private Variation[] variation_line_02;
    [SerializeField] private Variation[] variation_line_03;
    [SerializeField] private Variation[] variation_line_04;


    //"### Coords ###"
    private ArrayTool[] line_01;
    private ArrayTool[] line_02;
    private ArrayTool[] line_03;
    private ArrayTool[] line_04;


    //"### Objects ###"
    private GameObject[] object_line_01;
    private GameObject[] object_line_02;
    private GameObject[] object_line_03;
    private GameObject[] object_line_04;

    void Start()
    {
    object_line_01 = new GameObject[8];
    object_line_02 = new GameObject[8];
    object_line_03 = new GameObject[8];
    object_line_04 = new GameObject[8];

    line_01 = new ArrayTool[8];
    line_02 = new ArrayTool[8];
    line_03 = new ArrayTool[8];
    line_04 = new ArrayTool[8];
    }

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

    public void CreateSpawner()
    {
        SpawnDataStruct Spawner_new = ScriptableObject.CreateInstance<SpawnDataStruct>();
        string path = "Assets/Chicken Flight Adventure/Assets/House's Prefabs/Spawners/Spawner_new.asset";
        AssetDatabase.CreateAsset(Spawner_new, path);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = Spawner_new;

        Spawner_new.type_02 = true;

        Spawner_new.line_01 = new BlockLocationCoord[line_01.Length];
        Spawner_new.line_02 = new BlockLocationCoord[line_02.Length];
        Spawner_new.line_03 = new BlockLocationCoord[line_03.Length];
        Spawner_new.line_04 = new BlockLocationCoord[line_04.Length];

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
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
