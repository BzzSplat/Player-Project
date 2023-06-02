using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : InteractableObject
{
    [SerializeField]
    float force;
    [SerializeField]
    bool isActive;

    [SerializeField]
    ParticleSystem flames;
    [SerializeField]
    AudioSource sounds;
    [SerializeField]
    Rigidbody rb;

    public void Activate()
    {
        flames.Play();
        sounds.Play();
    }

    public void Disable()
    {
        flames.Stop();
        sounds.Stop();
    }

    private void FixedUpdate() //is there a non cororutine way to make this only check when it is on?
    {
        if(isActive)
            rb.AddForce(-transform.up * force);
    }

    public override void Interaction()
    {
        if (isActive)
        {
            Disable();
            isActive = false;
        }
        else
        {
            Activate();
            isActive = true;
        }
    }
}
