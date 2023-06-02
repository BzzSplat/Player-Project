using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : InteractableObject
{
    GameObject seated;
    Vector3 oldRotation;

    private void Start()
    {
        needsPlayer = true;
    }

    private void FixedUpdate()
    {
        //obj1.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        if (seated)
        {
            Quaternion qr = Quaternion.identity;
            Vector3 p = new Vector3(0, seated.transform.localRotation.eulerAngles.y, 0);
            qr.eulerAngles = p;

            seated.transform.localRotation = qr;
        }

    }

    public override void Interaction()
    {
        if (!seated)
        {
            seated = Player;
            seated.GetComponent<Rigidbody>().isKinematic = true;
            seated.GetComponent<Collider>().enabled = false;
            seated.transform.parent = transform;
            seated.transform.position = transform.position;
            seated.transform.rotation = transform.rotation;
        }
        else
        {
            if (Player != seated)
                return;

            seated.GetComponent<Collider>().enabled = true;
            seated.transform.parent = null;
            seated.transform.rotation = Quaternion.identity;
            seated.GetComponent<Rigidbody>().isKinematic = false;

            seated = null;
        }
    }
}
