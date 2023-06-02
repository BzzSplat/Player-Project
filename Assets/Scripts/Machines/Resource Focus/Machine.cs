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

    public virtual void addMaterials() //consider turning this into a short function like getWorkInfo with a bool to decide weather each element should be turned negative for use
    {
        //silo.workMaterials[index] += number;
    }
    public virtual void removeMaterials()
    {
        //silo.workMaterials[index] -= number;
    }

    public virtual int[,] getWorkInfo() //for silo menu
    {
        int[,] info = new int[1, 2] {
            { 2, -1 }, //type, amount
        };

        return info;
    }
}
