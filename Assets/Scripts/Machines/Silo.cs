using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : InteractableObject
{
    public List<Machine> connections;
    public float[] materials, workMaterials;
    public bool onOff = true, canWork;
    public List<LineRenderer> lines;
    [SerializeField]
    GameObject popup, owner;

    private void Start()
    {
        StartCoroutine("work");
    }

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
        canWork = true;
        for (int i = 0; i < materials.Length; i++) //check to see if any values will be negative
        {
            if (materials[i] + workMaterials[i] < 0)
                canWork = false;
        }
    }

    public override void Interaction()
    {
        Transform spawn = Player.transform.GetChild(0).GetChild(2);
        Player.transform.GetChild(0).GetComponent<CamControl>().freeMouse();
        
        
        GameObject Pops = Instantiate(popup, spawn);
        Pops.GetComponent<SiloPopup>().cam = Player.transform.GetChild(0).GetComponent<CamControl>();
        Pops.GetComponent<SiloPopup>().silo = this;
    }

    public virtual void Update()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetPosition(0, lines[i].transform.position);
            lines[i].SetPosition(1, transform.position);

            if (Vector3.Distance(lines[i].transform.position, transform.position) > 5)
            {
                //check if object is connected by relay then measure that distance        !make sure to check for the relay's distance too, and or if that relay links to another!
                if (connections[i].relay && Vector3.Distance(connections[i].transform.position, connections[i].relay.position) > 5)
                {
                    if (connections[i].onOff)
                    {
                        connections[i].Interaction();
                    }

                    connections[i].silo = null;
                    connections.RemoveAt(i);
                }
                else
                {
                    Destroy(lines[i].gameObject);
                    lines.RemoveAt(i);

                    if (connections[i].onOff)
                    {
                        connections[i].Interaction();
                    }

                    connections[i].silo = null;
                    connections.RemoveAt(i);
                }
            }
        }
    }

}
