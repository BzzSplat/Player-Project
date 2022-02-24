using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : InteractableObject
{
    public float electricityCost, maxElectricity;

    public List<Machine> connections = new List<Machine>(); //use this instead of inports
    public List<LineRenderer> lines = new List<LineRenderer>();

    public float[] materials = new float[3];
    public int inputID, outputID;
    public Machine Inports; //change for different machines

    public bool OnOff = false;
    public IEnumerator workCoro;

    public Text counter1, counter2;
    public GameObject menu, popup;

    public IEnumerator Produce()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);
            if (OnOff && materials[2] >= electricityCost)
            {
                materials[outputID]++;
                materials[2] -= electricityCost;
            }

            counter1.text = materials[outputID].ToString();

            if (materials[2] < electricityCost)
                toggleProducing();
        }
    }

    public IEnumerator Convert()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);

            if (Inports && Inports.materials[inputID] > 0 && materials[2] >= electricityCost)
            {
                materials[inputID] += Inports.materials[inputID];
                Inports.materials[inputID] = 0;
            }

            if (OnOff && materials[inputID] > 0 && materials[2] >= electricityCost)
            {
                materials[0]--;
                materials[1]++;
            }

            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();

            if (materials[2] < electricityCost)
                toggleProducing();
        }
    }

    public virtual void Update()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            lines[i].SetPosition(1, connections[i].transform.position);
            lines[i].SetPosition(0, transform.position);

            if (Vector3.Distance(connections[i].transform.position, transform.position) > 5)
            {
                Destroy(lines[i].gameObject);
                lines.RemoveAt(i);
                connections.RemoveAt(i);
            }
        }
    }

    public virtual void toggleProducing()
    {
        if (OnOff)
        {
            OnOff = false;
            StopCoroutine(workCoro);

            counter1.text = materials[inputID].ToString();
            if(counter2)
                counter2.text = materials[outputID].ToString();
        }
        else
        {
            OnOff = true;
            StartCoroutine(workCoro);

            counter1.text = materials[inputID].ToString();
            if (counter2)
                counter2.text = materials[outputID].ToString();
        }
    }

    public void updateDisplay()
    {
        if (OnOff)
        {
            counter1.text = materials[inputID].ToString();
            if(counter2)
                counter2.text = materials[outputID].ToString();
        }
        else
        {
            counter1.text = materials[inputID].ToString();
            if(counter2)
                counter2.text = materials[outputID].ToString();
        }
    }
}
