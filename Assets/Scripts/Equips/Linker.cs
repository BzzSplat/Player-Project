using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    bool SecondClick = false;

    Smelter Exports;
    Miner Inports;

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
                if (!SecondClick && hit.transform.gameObject.GetComponent<Smelter>())
                {
                    Exports = hit.transform.gameObject.GetComponent<Smelter>();
                    SecondClick = true;
                }
                else if(SecondClick && hit.transform.gameObject.GetComponent<Miner>())
                {
                    Inports = hit.transform.gameObject.GetComponent<Miner>();
                    SecondClick = false;
                    Link();
                    
                }
            }
        }
    }

    void Link()
    {
        Exports.Inports = Inports;
        LineRenderer pipe = Exports.gameObject.AddComponent<LineRenderer>();
        pipe.startWidth = 0.5f; pipe.endWidth = 0.5f;
        pipe.material = pipeMat;
        Exports = null;
        Inports = null;
    }
}
