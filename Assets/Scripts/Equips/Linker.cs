using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    bool SecondClick = false;

    InteractableObject inTo, outFrom;

    [SerializeField]
    Material pipeMat;

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
                if (!SecondClick && hit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    inTo = hit.transform.gameObject.GetComponent<InteractableObject>();
                    SecondClick = true;
                }

                else if(SecondClick && hit.transform.gameObject.GetComponent<InteractableObject>())
                {
                    outFrom = hit.transform.gameObject.GetComponent<InteractableObject>();
                    SecondClick = false;
                    Link();
                    
                }
            }
        }
    }

    void Link()
    {
        if(inTo.link() == outFrom.link())
        {
            inTo.Inports = outFrom; //if input matches output connect

            LineRenderer pipe = inTo.gameObject.AddComponent<LineRenderer>();
            pipe.startWidth = 0.5f; pipe.endWidth = 0.5f;
            pipe.material = pipeMat;
        }

        inTo = null;
        outFrom = null;
    }
}
