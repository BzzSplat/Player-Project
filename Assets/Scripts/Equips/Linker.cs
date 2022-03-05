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
            if (machine)
            {
                makeLine(machine.gameObject, 0.3f);
                silo.connections.Add(machine);
            }
            else if (relay1)
            {
                makeLine(relay1.gameObject, 0.3f);
                relay1.silo = silo;
            }
        }
        else if (relay1)
        {
            if (machine)
            {
                makeLine(machine.gameObject, 0.3f);
                relay1.silo.connections.Add(machine);
            }
            else if (relay2)
            {
                makeLine(relay2.gameObject, 0.3f);
                if (relay1.silo)
                    relay2.silo = relay1.silo;
                else
                    relay1.silo = relay2.silo;
            }
        }


    }

    void makeLine(GameObject lineHolder, float size)
    {
        GameObject lineObject = new GameObject();
        lineObject.transform.parent = lineHolder.transform;
        lineObject.transform.localPosition = Vector3.zero;

        LineRenderer pipe = lineObject.AddComponent<LineRenderer>();
        pipe.startWidth = size; pipe.endWidth = size;
        pipe.material = pipeMat;
        pipe.material.color = Color.black;

        if(silo) //add the line to the line list in the
            silo.lines.Add(pipe);
        else if (relay1)
            relay1.lines.Add(pipe);
    }
}
