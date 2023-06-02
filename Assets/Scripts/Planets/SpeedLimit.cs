using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speedThreshold, speedLimit, dragMax;

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude > speedLimit)
        {
            rb.velocity *= speedLimit / rb.velocity.magnitude; //set the speed to the max if it is over the max
        }
        else if (rb.velocity.magnitude > speedThreshold) //check if speed is over the threshhold to begin adding drag
        {
            rb.drag = ((rb.velocity.magnitude - speedThreshold) / (speedLimit - speedThreshold)) * dragMax; //increase the drag from 0 at the threshold to dragMax at the speedlimit
        }
    }
}
