using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMenu : MonoBehaviour //if producers ever generate multiple types, use lists for text and whatnot
{
    public Text resourceCounter, objectName;
    public ResourceManager playerInv;
    public int outputID;
    public Miner source;
    public CamControl cam;
    [SerializeField]
    GameObject powerSwitch;

    public void setUpMenu(string name, Miner producer, int ID, ResourceManager inv)
    {
        objectName.text = name;
        source = producer;
        outputID = ID;
        playerInv = inv;
    }

    void Update()
    {
        resourceCounter.text = source.rawMetals.ToString();
    }

    public void collectResources()
    {
        switch (outputID)
        {
            case 0:
                playerInv.rawMetal += source.rawMetals;
                source.rawMetals = 0;
                break;
        }
        source.updateDisplay();
    }

    public void closePopup()
    {
        cam.trapMouse();
        Destroy(gameObject);
    }

    public void togglePower()
    {
        if (source.OnOff)
            powerSwitch.GetComponent<Image>().color = Color.red;
        else
            powerSwitch.GetComponent<Image>().color = Color.green;
        source.toggleProducing();
    }
}
