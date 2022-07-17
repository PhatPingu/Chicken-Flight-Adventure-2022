using UnityEngine;

public class PortalBahaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private Transform targetLocationTransform;
    [SerializeField] private float timeToWait_OnTeleport = 2f;
    [SerializeField] private ParticleSystem teleportCompleted;
    
    private float alarm;
    private bool waitOnTimer;

    void Start()
    {
        player = GameObject.Find("Player_Group");
        mainCamera = GameObject.Find("Main Camera");

    }

    void Update()
    {
        FreezePlayer_OnTimer(waitOnTimer);
    }

    void OnCollisionEnter(Collision other)
    {
        var cameraAction = mainCamera.GetComponent<CameraBehaviour>();

        if(other.gameObject.tag == "Player")
        {
            other.transform.position = targetLocationTransform.position;
            waitOnTimer = true;
            
            cameraAction.SetZoomDistance();
            cameraAction.MoveCameraUp(true, 1f);
        }
    }
    
    void FreezePlayer_OnTimer(bool check)
    {
        if(check)
        {
            alarm -= Time.deltaTime;
            if(alarm <= 0)
            {
                waitOnTimer = false;
                alarm = timeToWait_OnTeleport;
                teleportCompleted.Play();
            }

            player.transform.position = targetLocationTransform.position;
        }
    }
}
