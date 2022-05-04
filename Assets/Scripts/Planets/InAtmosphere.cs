using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAtmosphere : MonoBehaviour
{
    public GameObject storage;
    Vector3 oldPosition;

    private void Start()
    {
        //storage = transform.parent.GetChild(2).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet" || other.tag == "Ground")
            return;

        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().useGravity = true;
        }


        if(other.transform.parent == null)
            other.transform.SetParent(storage.transform, true);

        if (other.CompareTag("Player"))
            other.GetComponent<Health>().canBreathe = true;

        if (other.CompareTag("Living"))
            other.GetComponent<Dummy>().canBreathe = true;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < storage.transform.childCount; i++)
        {
            Vector3 newPos = transform.position - oldPosition;
            storage.transform.GetChild(i).position += newPos;
        }
        oldPosition = transform.position;
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.tag == "Planet" || other.tag == "Ground")
            return;

        Vector3 newPos = new Vector3(0.06f,0,0);
        other.transform.position += newPos;
        Debug.Log(transform.position - oldPosition);
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Planet" || other.tag == "Ground")
            return;

        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().useGravity = false;

        if(other.transform.parent == storage.transform)
            other.gameObject.transform.parent = null;



        if (other.CompareTag("Player"))
            other.GetComponent<Health>().canBreathe = false;

        if (other.CompareTag("Living"))
            other.GetComponent<Dummy>().canBreathe = false;
    }
}//use an update (which comes after fixed updates) to add planet velocity to objects in atmosphere, remember to Time.deltaTime
