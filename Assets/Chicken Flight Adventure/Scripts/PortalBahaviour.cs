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

    }

    void Update()
    {
        FreezePlayer_OnTimer(waitOnTimer);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = targetLocationTransform.position;
            waitOnTimer = true;
            mainCamera.GetComponent<CameraBehaviour>().SetZoomDistance();
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
