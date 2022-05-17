using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    [SerializeField] private LevelSpawnsManager _levelSpawnsManager;
    [SerializeField] private Renderer material_topPlatform;
    [SerializeField] private Renderer material_bottomPlatform;


    [SerializeField] public bool doDestroyOnTimer;
    [SerializeField] public float destroyTimer = 35f;

    [SerializeField] private float minBlockSize = 1.0f;
    [SerializeField] private float maxBlockSize = 7.0f;
    
    private bool destroyAlert;

    private float timer = 1.0f;
    private float timerLimit = 0.7f;
    private float timerReset = 0.4f;

    void Start()
    {
        _levelSpawnsManager = GameObject.Find("Game Controller").GetComponent<LevelSpawnsManager>();

        ResizeBlock();
        DestroyOnTimer();
        SetRandomMaterial();
    }

    void Update()
    {
        if(doDestroyOnTimer)
        {
            destroyTimer -= Time.deltaTime;
            if(destroyTimer <= 5f)
            {
                destroyAlert = true;
            }
        }

        if(destroyAlert == true)
        {
            DestroyAlert();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor" && !doDestroyOnTimer)
        {
            destroyAlert = true;
            Destroy(gameObject, 1f);
        }
        if (other.gameObject.tag == "Platform" && !doDestroyOnTimer)
        {
            destroyAlert = true;
            Destroy(gameObject, 5f);
        }
    }

    void ResizeBlock()
    {
        float x = Random.Range(minBlockSize,maxBlockSize);
        float y = Random.Range(minBlockSize,maxBlockSize);
        float z = Random.Range(minBlockSize,maxBlockSize);
        transform.localScale = new Vector3(x,y,z);
    }

    void DestroyOnTimer()
    {
        if(doDestroyOnTimer)
        {
            Destroy(gameObject,destroyTimer);
        }
    }

    void SetRandomMaterial()
    {
        int random_top = Random.Range(0, _levelSpawnsManager.material_topPlatform.Length);
        material_topPlatform.material = _levelSpawnsManager.material_topPlatform[random_top];
        
        int random_bottom = Random.Range(0, _levelSpawnsManager.material_bottomPlatform.Length);
        material_bottomPlatform.material = _levelSpawnsManager.material_bottomPlatform[random_bottom];
    }

    void DestroyAlert()
    {
        timer -= Time.deltaTime;
        if(timer <= timerReset)
        {
            timer = 1.0f;
        }

        if(timer >= timerLimit)
        {
            Color newColor = new Color(1, 0, 0, 1); //change color to red
            material_topPlatform.material.color = newColor;

        }
        else
        {
            Color newColor = new Color(1, 1, 1, 1); //change color to white
            material_topPlatform.material.color = newColor;
        }
    }
}
