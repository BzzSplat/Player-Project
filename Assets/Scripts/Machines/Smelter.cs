using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smelter : InteractableObject
{
    public float[] materials;
    public int inputID, outputID;
    new public Miner Inports; //change for different machines

    public bool OnOff = false;

    [SerializeField]
    Text counter1, counter2;
    [SerializeField]
    GameObject menu, popup;

    IEnumerator convCoro;

    private void Start()
    {
        counter1.text = materials[inputID].ToString(); ;
        counter2.text = materials[outputID].ToString();
        convCoro = Convert();
    }

    IEnumerator Convert()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);

            if(Inports && Inports.materials[inputID] > 0)
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

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ConverterMenu>().setUpMenu(gameObject.name, this, 0, 1, Player.GetComponent<ResourceManager>());
    }

    public void toggleProducing()
    {
        if (OnOff)
        {
            OnOff = false;
            StopCoroutine(convCoro);
            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();
        }
        else
        {
            OnOff = true;
            StartCoroutine(convCoro);
            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();
        }
    }

    public void updateDisplay()
    {
        if (OnOff)
        {
            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();
        }
        else
        {
            counter1.text = materials[inputID].ToString();
            counter2.text = materials[outputID].ToString();
        }
    }

    private void Update()
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

    public override int link()
    {
        return inputID;
    }

}

