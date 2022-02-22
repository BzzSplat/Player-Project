using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConverterMenu : MonoBehaviour
{
    public Text inputCounter, outputCounter, objectName;
    public ResourceManager playerInv;
    public int inputID, outputID;
    public Machine source;
    public CamControl cam;
    [SerializeField]
    GameObject powerSwitch;
    [SerializeField]
    Sprite[] icons;

    public void setUpMenu(string name, Machine converter, int inID, int outID , ResourceManager inv)
    {
        objectName.text = name;
        source = converter;
        inputID = inID;
        outputID = outID;
        playerInv = inv;

        inputCounter.GetComponentInParent<Image>().sprite = icons[inID];
        outputCounter.GetComponentInParent<Image>().sprite = icons[outID];

        if (source.OnOff)
            powerSwitch.GetComponent<Image>().color = Color.green;
        else
            powerSwitch.GetComponent<Image>().color = Color.red;
    }

    void Update()
    {
        inputCounter.text = source.materials[inputID].ToString();
        outputCounter.text = source.materials[outputID].ToString();
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


    public void deposit(int amount)
    {
        if (playerInv.materials[inputID] < amount)
            return;

        source.materials[inputID] += amount;
        playerInv.materials[inputID] -= amount;
    }
}
