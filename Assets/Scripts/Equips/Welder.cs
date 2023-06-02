using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welder : MonoBehaviour // if ray hit nothing just stop, so no errors
{
    bool SecondClick = false, smartWeld = false;
    GameObject obj1, obj2;

    void Update()
    {
        if (!GetComponent<Weapon>().Holder)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0)) //left click
        {
            Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 endPoint;

            if (Physics.Raycast(ray, out hit, 5f))
                endPoint = hit.point;
            else
                endPoint = ray.GetPoint(1000);

            if (hit.transform)
            {
                if (SecondClick == false && hit.transform.gameObject.GetComponent<Rigidbody>())
                {
                    obj1 = hit.transform.gameObject;
                    SecondClick = true;
                }
                else
                {
                    obj2 = hit.transform.gameObject;
                    SecondClick = false;

                    if (Input.GetKey(KeyCode.Mouse1))
                        smartWeld = true;

                    Weld(hit);
                    smartWeld = false;
                    //Debug.Log("Weld created between " + obj1 + " and " + obj2);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2)) //left click
        {
            Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 endPoint;

            if (Physics.Raycast(ray, out hit, 5f))
                endPoint = hit.point;
            else
                endPoint = ray.GetPoint(1000);

            if (hit.transform.GetComponent<FixedJoint>())
            {
                Destroy(hit.transform.GetComponent<FixedJoint>());
            }
        }
    }

    void Weld(RaycastHit hit)
    {
        if(smartWeld)
        {
            obj1.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            obj1.transform.position = hit.point + hit.normal * obj1.transform.localScale.y / 2; // + hit.normal for attempt at correct positioning
        }
        
        FixedJoint newJoint = obj1.AddComponent<FixedJoint>();
        newJoint.connectedBody = obj2.GetComponent<Rigidbody>();
    }
}
