using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundriesCollision : MonoBehaviour
{
    private Vector3 lastGoodPosition;
    private Vector3 thisFramePosition;

    [SerializeField] float UpdatePosition_timer;

    void Update()
    {
        Invoke("NewPostion", UpdatePosition_timer);
    }

    void NewPostion() // Captures the position from the frame before:
    {   
        lastGoodPosition = transform.parent.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.parent.position = lastGoodPosition;
        }
    }

}
