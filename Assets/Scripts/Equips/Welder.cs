using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Welder : MonoBehaviour
{
    bool SecondClick = false;

    GameObject obj1, obj2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 endPoint;

            if (Physics.Raycast(ray, out hit))
                endPoint = hit.point;
            else
                endPoint = ray.GetPoint(1000);

            if (SecondClick == false && hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                obj1 = hit.transform.gameObject;
                SecondClick = true;
            }
            else
            {
                obj2 = hit.transform.gameObject;
                Weld(obj1, obj2);
                SecondClick = false;
            }
        }
    }

    void Weld(GameObject obj1, GameObject obj2)
    {
        
        obj1.AddComponent<FixedJoint>();
        obj1.GetComponent<FixedJoint>().connectedBody = obj2.GetComponent<Rigidbody>();

    }
}
