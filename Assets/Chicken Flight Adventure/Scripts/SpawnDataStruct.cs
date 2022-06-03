using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BlockLocationCoord
{
    public float xPosMin, xPosMax;
    public float yPos, yPosOffset;
    public float zPosMin, zPosMax;
}

[CreateAssetMenu(fileName = "Spawner", menuName ="Create Level")]

public class SpawnDataStruct : ScriptableObject
{
    [SerializeField] private GameObject[] _platformObject;

    [SerializeField] public bool spawner_enabled;

    [SerializeField] public bool type_01;
    [SerializeField] public bool type_02;

    [SerializeField] public Vector3 offsetStartLocation;

    [SerializeField] public BlockLocationCoord[] line_01;
    [SerializeField] public BlockLocationCoord[] line_02;
    [SerializeField] public BlockLocationCoord[] line_03;
    [SerializeField] public BlockLocationCoord[] line_04;
    public BlockLocationCoord[][]  lines_Array;

    void Start() 
    {
        lines_Array = new[] {line_01, line_02, line_03, line_04};
    }

    public void Initialization()
    {
        _platformObject = GameObject.Find("Game Controller").GetComponent<LevelSpawnsManager>().platformObject;
        
    }

    public void CallBlockLines()
    {
        
        if(spawner_enabled)
        {
            if(type_01)
            {
                if(line_01.Length > 0)
                    MakeBlockLine(line_01); 
                if(line_02.Length > 0)
                    MakeBlockLine(line_02); 
                if(line_03.Length > 0)
                    MakeBlockLine(line_03); 
                if(line_04.Length > 0)
                    MakeBlockLine(line_04); 
            }
            else if(type_02)
            {
                if(line_01.Length > 0)
                    MakeBlockLine(line_01); 
                if(line_02.Length > 0)
                    MakeBlockLine(line_02);
                if(line_03.Length > 0)
                    MakeBlockLine(line_03); 
                if(line_04.Length > 0)
                    MakeBlockLine(line_04);
            }
        }
    }
    
    void MakeBlockLine(BlockLocationCoord[] whatLine)
    {
        if(type_01)
        {
            MakeBlockCluster_Type01(whatLine);
        }
        else if(type_02)
        {
            MakeBlockCluster_Type02(whatLine);
        }
    }

    void MakeBlockCluster_Type01(BlockLocationCoord[] whatLine)
    {
        float CreateChanceControl = Random.Range(0.0f, 1.0f);
        if (CreateChanceControl > 0.66f) // if more than 0.66f, then top 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(1, whatLine);
            InstantiateBlock(2, whatLine);
        }
        else if (CreateChanceControl > 0.33f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(1, whatLine);
        }
        else // else it's bottom 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(2, whatLine);
        }    
    }

    void MakeBlockCluster_Type02(BlockLocationCoord[] whatLine)
    {
        float CreateChanceControl = Random.Range(0.0f, 1.0f);
        if (CreateChanceControl > 0.875f) // if more than 0.66f, then top 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(2, whatLine);
            InstantiateBlock(4, whatLine);
            InstantiateBlock(6, whatLine);
            InstantiateBlock(7, whatLine);
        }
        else if (CreateChanceControl > 0.75f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(1, whatLine);
            InstantiateBlock(3, whatLine);
            InstantiateBlock(4, whatLine);
            InstantiateBlock(5, whatLine);
            InstantiateBlock(7, whatLine);
        }
        else if (CreateChanceControl > 0.625f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(1, whatLine);
            InstantiateBlock(2, whatLine);
            InstantiateBlock(3, whatLine);
            InstantiateBlock(6, whatLine);
        }
        else if (CreateChanceControl > 0.50f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(2, whatLine);
            InstantiateBlock(4, whatLine);
            InstantiateBlock(5, whatLine);
            InstantiateBlock(6, whatLine);
        }
        else if (CreateChanceControl > 0.375f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(2, whatLine);
            InstantiateBlock(3, whatLine);
            InstantiateBlock(6, whatLine);
        }
        else if (CreateChanceControl > 0.25f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(2, whatLine);
            InstantiateBlock(3, whatLine);
            InstantiateBlock(4, whatLine);
            InstantiateBlock(6, whatLine);
        }                        
        else if (CreateChanceControl > 0.125f) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(1, whatLine);
            InstantiateBlock(3, whatLine);
            InstantiateBlock(5, whatLine);
            InstantiateBlock(7, whatLine);
        }            
        else // else it's bottom 1/3
        {
            InstantiateBlock(2, whatLine);
            InstantiateBlock(4, whatLine);
            InstantiateBlock(5, whatLine);
        } 
    }

    void InstantiateBlock(int whatBlock, BlockLocationCoord[] whatLine) 
    {
        float xPosMin =     whatLine[whatBlock].xPosMin;
        float xPosMax =     whatLine[whatBlock].xPosMax;
        float yPos =        whatLine[whatBlock].yPos;
        float yPosOffset =  whatLine[whatBlock].yPosOffset;
        float zPosMin =     whatLine[whatBlock].zPosMin;
        float zPosMax =     whatLine[whatBlock].zPosMax;

        float x = Random.Range(xPosMin, xPosMax)    + offsetStartLocation.x;
        float y = yPos + yPosOffset                 + offsetStartLocation.y;
        float z = Random.Range(zPosMin, zPosMax)    + offsetStartLocation.z;
        Vector3 thisLocation = new Vector3(x,y,z);            
        
        //GameObject[] i_platformObject = GameObject.Find("Game Controller").GetComponent<LevelSpawnsManager>().platformObject;
        int random = Random.Range(0 , _platformObject.Length);
        GameObject newPlatform = Instantiate(_platformObject[random], thisLocation, Quaternion.identity);

        SetPlataform_Type(newPlatform); //Type determines if platform destroysOnTimer   
    }   

    void SetPlataform_Type(GameObject whatPlatform)
    {
        if(type_01 )
        {
            whatPlatform.GetComponent<ObjectBehaviour>().doDestroyOnTimer = false;
        }
        else if (type_02)
        {
            whatPlatform.GetComponent<ObjectBehaviour>().doDestroyOnTimer = true;
        }
    }
}
