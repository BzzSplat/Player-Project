using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    bool SecondClick = false, doingElec = false;

    Machine inTo, outFrom;

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
                if (!SecondClick && hit.transform.gameObject.GetComponent<Machine>()) //first click
                {
                    inTo = hit.transform.gameObject.GetComponent<Machine>();
                    SecondClick = true;

                    if (hit.transform.gameObject.GetComponent<Generator>())
                        doingElec = true;
                }

                else if(SecondClick && hit.transform.gameObject.GetComponent<Machine>()) //second click
                {
                    outFrom = hit.transform.gameObject.GetComponent<Machine>();
                    SecondClick = false;

                    if (hit.transform.gameObject.GetComponent<Generator>())
                        doingElec = true;

                    Link();
                }
            }
        }
    }

    void Link()
    {
        if (!doingElec)
        {
            if(inTo.link() == outFrom.link())
            {
               inTo.Inports = outFrom; //if input matches output connect

                LineRenderer pipe = inTo.gameObject.AddComponent<LineRenderer>();
                pipe.startWidth = 0.5f; pipe.endWidth = 0.5f;
                pipe.material = pipeMat;
            }
        }
        else
        {
            if (inTo.GetType() == typeof(Generator))
            {
                inTo.GetComponent<Generator>().connections.Add(outFrom);
            }
            if (outFrom.GetType() == typeof(Generator))
            {
                outFrom.GetComponent<Generator>().connections.Add(inTo);
            }

            LineRenderer pipe = inTo.gameObject.AddComponent<LineRenderer>();
            pipe.startWidth = 0.2f; pipe.endWidth = 0.2f;
            pipe.material = pipeMat;
        }


        inTo = null;
        outFrom = null;
        doingElec = false;
    }
}
