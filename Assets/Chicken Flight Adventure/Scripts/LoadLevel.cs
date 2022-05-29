using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private GameObject _player;
    private PlayerAnimationControl _playerAnimationControl;
    private LevelSpawnsManager _levelSpawnsManager;
    
    [SerializeField] private MeshRenderer starMesh;
    [SerializeField] private SphereCollider starCollider;

    [Tooltip("Make sure that all Spawners that were added are also added in Management.GameController")]

    [SerializeField] private SpawnDataStruct[] enableLevel;
    [SerializeField] private SpawnDataStruct[] disableLevel;

    [SerializeField] private GameObject[] nextStars;
    [SerializeField] private GameObject blueStars;
    
    private Transform[] blueStarChildren;

    private float InstantiationTimer;

    void Start()
    {
        _player = GameObject.Find("Player_Group");
        _playerAnimationControl = _player.GetComponent<PlayerAnimationControl>();
        _levelSpawnsManager = GameObject.Find("Game Controller").GetComponent<LevelSpawnsManager>();
        blueStarChildren = blueStars.GetComponentsInChildren<Transform>();
        DeactivateBlueStars();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerAnimationControl.CelebrateAir_Animation();
            EnableLevels();
            DisableLevels();
            DeactivateThisStar();
            ActivateLevelStars_NextLevel();
            ActivateBlueStars();
        }
    }

    void EnableLevels()
    {
        for (int i = 0; i < enableLevel.Length; i++)
        {
            if (enableLevel[i] == null)
            {
                return;
            }
            else
            {
                enableLevel[i].spawner_enabled = true;
                _levelSpawnsManager.InstantiationTimer = 0f;
            }
        }
    }

    void DisableLevels()
    {
        for (int i = 0; i < disableLevel.Length; i++)
        {
            if (disableLevel[i] == null)
            {
                return;
            }
            else
            {
                disableLevel[i].spawner_enabled = false;
            }
        }
    }

    void DeactivateThisStar()
    {
        starCollider.enabled = false;
        starMesh.enabled = false;
    }

    void ActivateLevelStars_NextLevel()
    {
        for (int i = 0; i < nextStars.Length; i++)
        {
            nextStars[i].SetActive(true);
        }
    }
    
    void ActivateBlueStars()
    {
        if(blueStarChildren == null)
        { return; }
        
        for (int i = 0; i < blueStarChildren.Length; i++)
        {
            blueStarChildren[i].gameObject.SetActive(true);
        }
    }

    void DeactivateBlueStars()
    {
        if(blueStarChildren == null)
        { return; }

        for (int i = 0; i < blueStarChildren.Length; i++)
        {
            blueStarChildren[i].gameObject.SetActive(false);
        }
    }
}
