using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : InteractableObject
{
    public bool onOff;
    public Silo silo;
    public Transform relay;
    [SerializeField]
    Renderer indicator;

    private void Start()
    {
        indicator.material.color = Color.black;
        indicator.material.EnableKeyword("_EMISSION");
        indicator.material.SetColor("_EmissionColor", Color.red);
    }

    public override void Interaction()
    {
        if (onOff && silo)
        {
            onOff = false;
            if (silo)
                removeMaterials();
            indicator.material.SetColor("_EmissionColor", Color.red);
        }
        else if (!onOff && silo)
        {
            onOff = true;
            if (silo)
                addMaterials();
            indicator.material.SetColor("_EmissionColor", Color.green);
        }

    }

    public virtual void addMaterials()
    {
        //silo.workMaterials[index] += number;
    }
    public virtual void removeMaterials()
    {
        //silo.workMaterials[index] -= number;
    }

    /*public virtual void Update()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            lines[i].SetPosition(1, connections[i].transform.position);
            lines[i].SetPosition(0, transform.position);

            if (Vector3.Distance(connections[i].transform.position, transform.position) > 5)
            {
                Destroy(lines[i].gameObject);
                lines.RemoveAt(i);
                connections.RemoveAt(i);
            }
        }
    }*/
}
