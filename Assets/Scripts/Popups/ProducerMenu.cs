using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMenu : MonoBehaviour //if producers ever generate multiple types, use lists for text and whatnot
{
    public Text resourceCounter, objectName;
    public ResourceManager playerInv;
    public int outputID;
    public Machine source;
    public CamControl cam;
    [SerializeField]
    GameObject powerSwitch;
    [SerializeField]
    Sprite[] icons;

    public void setUpMenu(string name, Machine producer, int ID, ResourceManager inv)
    {
        objectName.text = name;
        source = producer;
        outputID = ID;
        playerInv = inv;

        resourceCounter.GetComponentInParent<Image>().sprite = icons[ID];

        if (source.OnOff)
            powerSwitch.GetComponent<Image>().color = Color.green;
        else
            powerSwitch.GetComponent<Image>().color = Color.red;
    }

    void Update()
    {
        resourceCounter.text = source.materials[outputID].ToString();
    }

    public void collectResources()
    {
        playerInv.materials[outputID] += source.materials[outputID];
        source.materials[outputID] = 0;
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
