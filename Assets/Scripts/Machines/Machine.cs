using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : InteractableObject
{
    public float electricityCost, maxElectricity;

    public List<float> materials = new List<float>();
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
            if (OnOff)
                materials[outputID]++;
            counter1.text = materials[outputID].ToString();
        }
    }

    public IEnumerator Convert()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);

            if (Inports && Inports.materials[inputID] > 0)
            {
                materials[inputID] += Inports.materials[inputID];
                Inports.materials[inputID] = 0;
            }

            if (OnOff && materials[inputID] > 0)
            {
                materials[0]--;
                materials[1]++;
            }
            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();
        }
    }

    public virtual void Update()
    {
        if (Inports && GetComponent<LineRenderer>())
        {
            GetComponent<LineRenderer>().SetPosition(1, Inports.transform.position);
            GetComponent<LineRenderer>().SetPosition(0, transform.position);

            if (Vector3.Distance(Inports.transform.position, transform.position) > 5)
            {
                Destroy(GetComponent<LineRenderer>());
                Inports = null;
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
