using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : Machine
{
    public override void addMaterials()
    {
        silo.workMaterials[0] -= 1;
        silo.workMaterials[1] += 1;
        silo.workMaterials[2] -= 1;
    }
    public override void removeMaterials()
    {
        silo.workMaterials[0] += 1;
        silo.workMaterials[1] -= 1;
        silo.workMaterials[2] += 1;
    }

    public override int[,] getWorkInfo()
    {
        int[,] info = new int[3, 2] { 
            { 0, -1 },
            { 1, 1 },
            { 2, -1 }
        };

        return info;
    }
}