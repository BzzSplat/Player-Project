using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Producer : Machine
{    private void Start()
    {
        counter1.text = "Stopped\n" + materials[outputID].ToString(); ;
        workCoro = Produce();
    }

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ProducerMenu>().setUpMenu(gameObject.name, GetComponent<Producer>(), outputID, Player.GetComponent<ResourceManager>());
    }

    public override int link()
    {
        return outputID;
    }
}
