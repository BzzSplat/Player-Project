using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAtmosphere : MonoBehaviour
{
    public List<GameObject> storage = new List<GameObject>();
    Vector3 oldPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet" || other.tag == "Ground")
            return;

        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().velocity += other.GetComponent<Rigidbody>().velocity - transform.GetComponentInParent<Rigidbody>().velocity;

        }


        if(other.transform.parent == null)
            storage.Add(other.gameObject);

        if (other.CompareTag("Player"))
            other.GetComponent<Health>().canBreathe = true;

        if (other.CompareTag("Living"))
            other.GetComponent<Dummy>().canBreathe = true;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = transform.position - oldPosition;
        for (int i = 0; i < storage.Count; i++)
        {
            if (storage[i] == null)
                storage.RemoveAt(i);
            else
                storage[i].transform.position += newPos;
            //Debug.Log($"Moving {storage.transform.GetChild(i).name}, {newPos}");
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
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().velocity += transform.GetComponentInParent<Rigidbody>().velocity; //make the object keep with the planet
        }

        if (storage.Contains(other.gameObject))
            storage.Remove(other.gameObject);

        if (other.CompareTag("Player"))
            other.GetComponent<Health>().canBreathe = false;

        if (other.CompareTag("Living"))
            other.GetComponent<Dummy>().canBreathe = false;
    }
}//use an update (which comes after fixed updates) to add planet velocity to objects in atmosphere, remember to Time.deltaTime
