using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnsManager : MonoBehaviour
{
    [TextArea][SerializeField] private string ImportantNote;

    public SpawnDataStruct[] spawner;
    public GameObject[] platformObject;
    public Material[] material_topPlatform;
    public Material[] material_bottomPlatform;
    
    public float InstantiationTimer;

    void Start()
    {
        SpawnersInitialization();
    }

    void FixedUpdate()
    {
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)
        {
            InstantiationTimer = Random.Range(8.0f, 14.00f);
            for (int i = 0; i < spawner.Length; i++)
            {
                if(spawner[i] == null)
                {
                    return;
                }
                else
                {
                    spawner[i].CallBlockLines();
                }
            }
        }
    }

    void SpawnersInitialization()
    {
        for (int i = 0; i < spawner.Length; i++)
            {
                if(spawner[i] == null)
                {
                    return;
                }
                else
                {
                    spawner[i].Initialization();
                    spawner[i].spawner_enabled = false;
                }

            }
    }
}
