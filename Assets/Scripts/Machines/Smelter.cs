using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smelter : InteractableObject
{
    public float rawMetals = 0, metalsT1 = 0;
    public bool OnOff = false;
    [SerializeField]
    Text counter1, counter2;
    IEnumerator convCoro;
    [SerializeField]
    GameObject menu, popup;

    private void Start()
    {
        counter1.text = rawMetals.ToString(); ;
        counter2.text = metalsT1.ToString();
        convCoro = Convert();
    }

    IEnumerator Convert()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);
            if (OnOff && rawMetals > 0)
            {
                rawMetals--;
                metalsT1++;
            }
            counter1.text = rawMetals.ToString();
            counter2.text = metalsT1.ToString();
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
            counter1.text = rawMetals.ToString();
            counter2.text = metalsT1.ToString();
        }
        else
        {
            OnOff = true;
            StartCoroutine(convCoro);
            counter1.text = rawMetals.ToString();
            counter2.text = metalsT1.ToString();
        }
    }

    public void updateDisplay()
    {
        if (OnOff)
        {
            counter1.text = rawMetals.ToString();
            counter2.text = metalsT1.ToString();
        }
        else
        {
            counter1.text = rawMetals.ToString();
            counter2.text = metalsT1.ToString();
        }
    }

}

