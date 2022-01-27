using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConverterMenu : MonoBehaviour
{
    public Text inputCounter, outputCounter, objectName;
    public ResourceManager playerInv;
    public int inputID, outputID;
    public Smelter source;
    public CamControl cam;
    [SerializeField]
    GameObject powerSwitch;

    public void setUpMenu(string name, Smelter converter, int inID, int outID , ResourceManager inv)
    {
        objectName.text = name;
        source = converter;
        inputID = inID;
        outputID = outID;
        playerInv = inv;
    }

    void Update()
    {
        inputCounter.text = source.rawMetals.ToString();
        outputCounter.text = source.metalsT1.ToString();
    }

    public void collectResources()
    {
        switch (outputID)
        {
            case 0:
                playerInv.rawMetal += source.rawMetals;
                source.rawMetals = 0;
                break;
            case 1:
                playerInv.metalTier1 += source.metalsT1;
                source.metalsT1 = 0;
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


    public void deposit(int amount)
    {
        if (playerInv.rawMetal < amount)
            return;

        source.rawMetals += amount;
        playerInv.rawMetal -= amount;
    }
}
