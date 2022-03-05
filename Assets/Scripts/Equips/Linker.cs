using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    bool SecondClick = false;

    Machine machine;
    Relay relay1, relay2;
    Silo silo;

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
                if (!SecondClick) //first click
                {
                    if (hit.transform.gameObject.GetComponent<Machine>())
                        machine = hit.transform.gameObject.GetComponent<Machine>();

                    else if (hit.transform.gameObject.GetComponent<Relay>())
                        relay1 = hit.transform.gameObject.GetComponent<Relay>();

                    else if (hit.transform.gameObject.GetComponent<Silo>())
                        silo = hit.transform.gameObject.GetComponent<Silo>();

                    SecondClick = true;
                }

                else if(SecondClick && hit.transform.gameObject.GetComponent<Machine>()) //second click
                {
                    if (hit.transform.gameObject.GetComponent<Machine>())
                        machine = hit.transform.gameObject.GetComponent<Machine>();

                    else if (hit.transform.gameObject.GetComponent<Relay>())
                        if(!relay1)
                            relay1 = hit.transform.gameObject.GetComponent<Relay>();
                        else
                            relay2 = hit.transform.gameObject.GetComponent<Relay>();

                    else if (hit.transform.gameObject.GetComponent<Silo>())
                        silo = hit.transform.gameObject.GetComponent<Silo>();

                    SecondClick = false;
                    Link();
                }
            }
        }
    }

    void Link()
    {
        if (silo)
        {
            silo.connections.Add(machine);
            
        }


    }

    //makeLine(inTo.gameObject, Color.black, 0.3f);
    void makeLine(GameObject lineHolder, Color color, float size)
    {
        GameObject lineObject = new GameObject();
        lineObject.transform.parent = lineHolder.transform;
        lineObject.transform.localPosition = Vector3.zero;

        LineRenderer pipe = lineObject.AddComponent<LineRenderer>();
        pipe.startWidth = size; pipe.endWidth = size;
        pipe.material = pipeMat;
        pipe.material.color = color;

        if(lineHolder.GetComponent<Silo>()) //add the line to the line list in the
            lineHolder.GetComponent<Silo>().lines.Add(pipe);
        else if (lineHolder.GetComponent<Relay>())
            lineHolder.GetComponent<Relay>().lines.Add(pipe);
    }
}
