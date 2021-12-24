using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welder : MonoBehaviour // if ray hti nothing just stop, so no errors
{
    bool SecondClick = false;

    GameObject obj1, obj2;

    void Update()
    {
        if (!GetComponent<Weapon>().Holder)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 endPoint;

            if (Physics.Raycast(ray, out hit))
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
                    Weld(obj1, obj2);
                    Debug.Log("Weld created between " + obj1 + " and " + obj2);
                }
            }
        }
    }

    void Weld(GameObject obj1, GameObject obj2)
    {
        
        FixedJoint newJoint = obj1.AddComponent<FixedJoint>();
        newJoint.connectedBody = obj2.GetComponent<Rigidbody>();
    }
}
