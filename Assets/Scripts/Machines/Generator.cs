using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Machine
{
    public float produceAmount;

    private void Start()
    {
        counter1.text = materials[outputID].ToString(); ;
        workCoro = Generate();
    }

    IEnumerator Generate()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(1);
            if (OnOff && materials[2] < maxElectricity)
                materials[outputID] += produceAmount;
            counter1.text = materials[outputID].ToString();

            if (connections.Count > 0)
            {
                Machine needElec = connections[Random.Range(0, connections.Count-1)];
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
            }
        }
    }

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ProducerMenu>().setUpMenu(gameObject.name, GetComponent<Generator>(), outputID, Player.GetComponent<ResourceManager>());
    }
}
