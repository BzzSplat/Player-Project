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
                makeLine(inTo.gameObject, Color.white, 0.5f);
                inTo.connections.Add(outFrom);
            }
        }
        else
        {

            if (inTo.GetType() == typeof(Generator))
            {
                makeLine(inTo.gameObject, Color.black, 0.2f);
                inTo.connections.Add(outFrom);
            }
            if (outFrom.GetType() == typeof(Generator))
            {
                makeLine(outFrom.gameObject, Color.black, 0.2f);
                outFrom.connections.Add(outFrom);
            }
        }


        inTo = null;
        outFrom = null;
        doingElec = false;
    }

    void makeLine(GameObject lineHolder, Color color, float size)
    {
        GameObject lineObject = new GameObject();
        lineObject.transform.parent = lineHolder.transform;
        lineObject.transform.localPosition = Vector3.zero;

        LineRenderer pipe = lineObject.AddComponent<LineRenderer>();
        pipe.startWidth = size; pipe.endWidth = size;
        pipe.material = pipeMat;
        pipe.material.color = color;

        lineHolder.GetComponent<Machine>().lines.Add(pipe);
    }
}
