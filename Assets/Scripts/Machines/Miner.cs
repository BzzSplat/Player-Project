using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Miner : InteractableObject // instead make this a parent class for all producing machines
{
    public float[] materials;
    public int outputID;

    public bool OnOff = false;

    [SerializeField]
    Text counter;
    [SerializeField]
    GameObject menu, popup;

    IEnumerator prodCoro;

    private void Start()
    {
        counter.text = "Stopped\n" + materials[outputID].ToString(); ;
        prodCoro = Produce();
    }

    IEnumerator Produce()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);
            if(OnOff)
                materials[0]++;
            counter.text = "Mining\n" + materials[outputID].ToString();
        }
        
    }

    public override void Interaction()
    {
        popup = popupMenu(menu, Player.transform.GetChild(0).GetChild(2).gameObject);
        popup.GetComponent<ProducerMenu>().setUpMenu(gameObject.name, GetComponent<Miner>(), 0, Player.GetComponent<ResourceManager>());
    }

    public void toggleProducing()
    {
        if (OnOff) {
            OnOff = false;
            StopCoroutine(prodCoro);
            counter.text = "Stopped\n" + materials[outputID].ToString();
        } else {
            OnOff = true;
            StartCoroutine(prodCoro);
            counter.text = "Mining\n" + materials[outputID].ToString();
        }
    }

    public void updateDisplay()
    {
        if (OnOff)
            counter.text = "Mining\n" + materials[outputID].ToString();
        else
            counter.text = "Stopped\n" + materials[outputID].ToString();
    }

    public override int link()
    {
        return outputID;
    }
}
