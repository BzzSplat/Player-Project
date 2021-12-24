using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAtmosphere : MonoBehaviour
{
    public GameObject storage;

    private void Start()
    {
        storage = transform.parent.GetChild(2).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
            return;

        if(other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().useGravity = true;

        if(other.gameObject.transform.parent = null)
            other.gameObject.transform.SetParent(storage.transform, true);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().AddForce (GetComponentInParent<Rigidbody>().velocity);
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Planet")
            return;

        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().useGravity = false;

        if(other.transform.parent = storage.transform)
            other.gameObject.transform.parent = null;
    }
}//use an update (which coems after fixed updates) to add planet velocity to objects in atmosphere, remember to Time.deltaTime
