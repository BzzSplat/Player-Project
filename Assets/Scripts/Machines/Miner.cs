using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    public override void addMaterials()
    {
        silo.workMaterials[0] += 1;
    }
    public override void removeMaterials()
    {
        silo.workMaterials[0] -= 1;
    }
}
