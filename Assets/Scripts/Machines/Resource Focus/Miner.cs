using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    public override void addMaterials()
    {
        silo.workMaterials[0] += 1;
        silo.workMaterials[2] -= 1;
    }
    public override void removeMaterials()
    {
        silo.workMaterials[0] -= 1;
        silo.workMaterials[2] += 1;
    }

    public override int[,] getWorkInfo()
    {
        int[,] info = new int[2, 2] {
            { 0, 1 },
            { 2, -1 }
        };

        return info;
    }
}