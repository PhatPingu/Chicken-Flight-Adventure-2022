using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[System.Serializable]
public struct BlockLocationCoord
{
    public float xPosMin, xPosMax;
    public float yPos, yPosOffset;
    public float zPosMin, zPosMax;
}*/

[CreateAssetMenu(fileName = "Spawner", menuName ="Create Level")]

public class SpawnType_02 : ScriptableObject
{
    [SerializeField] private GameObject platformObject;

    private float chanceParameter_01 = 0.66f;
    private float chanceParameter_02 = 0.33f;

    [SerializeField] private BlockLocationCoord[] line_01;
    [SerializeField] private BlockLocationCoord[] line_02;
    [SerializeField] private BlockLocationCoord[] line_03;
    [SerializeField] private BlockLocationCoord[] line_04;

    public void CallBlockLines()
    {
        MakeBlockLine(line_01); 
        MakeBlockLine(line_02); 
        MakeBlockLine(line_03); 
        MakeBlockLine(line_04); 
    }
    
    void MakeBlockLine(BlockLocationCoord[] whatLine)
    {
        float CreateChanceControl = Random.Range(0.0f, 1.0f);
        if (CreateChanceControl > chanceParameter_01) // if more than 0.66f, then top 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(1, whatLine);
            InstantiateBlock(2, whatLine);
        }
        else if (CreateChanceControl > chanceParameter_02) // if more than 0.33f, then mid 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(1, whatLine);
        }
        else // else it's bottom 1/3
        {
            InstantiateBlock(0, whatLine);
            InstantiateBlock(2, whatLine);
        }    

        void InstantiateBlock(int whatBlock, BlockLocationCoord[] whatLine) 
        {
            float xPosMin =     whatLine[whatBlock].xPosMin;
            float xPosMax =     whatLine[whatBlock].xPosMax;
            float yPos =        whatLine[whatBlock].yPos;
            float yPosOffset =  whatLine[whatBlock].yPosOffset;
            float zPosMin =     whatLine[whatBlock].zPosMin;
            float zPosMax =     whatLine[whatBlock].zPosMax;

            float x = Random.Range(xPosMin, xPosMax);
            float y = yPos + yPosOffset;
            float z = Random.Range(zPosMin, zPosMax);
            Vector3 thisLocation = new Vector3(x,y,z);            
            
            Instantiate(platformObject, thisLocation, Quaternion.identity);
        }

    }
}
