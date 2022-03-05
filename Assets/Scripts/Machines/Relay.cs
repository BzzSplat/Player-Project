using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
    public Silo silo;
    public List<Machine> machines;
    public List<LineRenderer> lines;

    public virtual void Update()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetPosition(0, lines[i].transform.position);
            lines[i].SetPosition(1, transform.position);

            if (Vector3.Distance(lines[i].transform.position, transform.position) > 5)
            {
                Destroy(lines[i].gameObject);
                lines.RemoveAt(i);
            }
        }
    }
}
