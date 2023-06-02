using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float radius;
    public float power;
    public int[] affectedLayers;

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            bool correctLayer = true;
            foreach(int l in affectedLayers)
            {
                correctLayer = hit.gameObject.layer == l;
            }

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius);

            if (hit.GetComponent<Health>())
                hit.GetComponent<Health>().health -= (int) ((power/100) / Vector3.Distance(hit.transform.position, transform.position));

            if (hit.GetComponent<Dummy>())
                hit.GetComponent<Dummy>().health -= (int) ((power / 100) / Vector3.Distance(hit.transform.position, transform.position));
        }
        Destroy(this.gameObject, 1);
    }
}
