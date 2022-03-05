using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : InteractableObject
{
    public List<Machine> connections;
    public float[] materials, workMaterials;
    public bool onOff, canWork = true;
    public List<LineRenderer> lines;

    IEnumerator work()
    {
        while (onOff)
        {
            yield return new WaitForSeconds(5);

            checkMaterialCounts();

            if(canWork)
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] += workMaterials[i]; //add or subtract materials to simulate machines working
                }
        }
    }

    public void checkMaterialCounts()
    {
        for (int i = 0; i < materials.Length; i++) //check to see if any values will be negative
        {
            if (materials[i] + workMaterials[i] < 0)
                canWork = false;
            else
                canWork = true;
        }
    }

}
