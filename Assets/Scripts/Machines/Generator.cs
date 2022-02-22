using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Machine
{
    float produceAmount;
    public List<Machine> connections = new List<Machine>();
    public List<LineRenderer> lines = new List<LineRenderer>();

    private void Start()
    {
        counter1.text = "Stopped\n" + materials[outputID].ToString(); ;
        workCoro = Generate();
    }

    IEnumerator Generate()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(1);
            if (OnOff && materials[2] < maxElectricity)
                materials[outputID] += produceAmount;
            counter1.text = "Working\n" + materials[outputID].ToString();

            /*if (connections.Count > 0)
            {
                Machine needElec = connections[Random.Range(0, connections.Count)];
                bool needsToGive = true;
                while (needsToGive)
                {
                    if (needElec.materials[2] < maxElectricity)
                    {
                        needElec.materials[2]++;
                        needsToGive = false;
                    }
                    else
                        needElec = connections[Random.Range(0, connections.Count)]; 

                }
            }*/
        }
    }

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ProducerMenu>().setUpMenu(gameObject.name, GetComponent<Generator>(), outputID, Player.GetComponent<ResourceManager>());
    }

    public override void Update()
    {
        LineRenderer[] lines = GetComponents<LineRenderer>(); //causes the out of bounds error, because there can only be 1 line renderer

        for (int i = 0; i < connections.Count-1; i++)
        {
            lines[i].SetPosition(1, connections[i].transform.position);
            lines[i].SetPosition(0, transform.position);

            if (Vector3.Distance(connections[i].transform.position, transform.position) > 5)
            {
                Destroy(lines[i]);
            }
        }
    }
}
