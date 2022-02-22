using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Converter : Machine
{    
    private void Start()
    {
        counter1.text = materials[inputID].ToString(); ;
        counter2.text = materials[outputID].ToString();
        workCoro = Convert();
    }

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ConverterMenu>().setUpMenu(gameObject.name, this, inputID, outputID, Player.GetComponent<ResourceManager>());
    }

    public override int link()
    {
        return inputID;
    }

}

