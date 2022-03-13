using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Machine
{
    public override void addMaterials()
    {
        silo.workMaterials[2] += 5;
    }
    public override void removeMaterials()
    {
        silo.workMaterials[2] -= 5;
    }
}
