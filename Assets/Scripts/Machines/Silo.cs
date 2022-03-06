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

    public virtual void Update()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetPosition(0, lines[i].transform.position);
            lines[i].SetPosition(1, transform.position);

            if (Vector3.Distance(lines[i].transform.position, transform.position) > 5)
            {
                if (connections[i].relay && Vector3.Distance(connections[i].transform.position, connections[i].relay.position) > 5)
                {
                    connections.RemoveAt(i);
                }
                Destroy(lines[i].gameObject);
                lines.RemoveAt(i);
                connections.RemoveAt(i);
            }
        }
    }

}
