using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Miner : InteractableObject // instead make this a parent class for all producing machines
{
    public float rawMetals = 0;
    public bool OnOff = false;
    [SerializeField]
    Text counter;
    IEnumerator prodCoro;
    [SerializeField]
    GameObject menu, popup;

    private void Start()
    {
        counter.text = "Stopped\n" + rawMetals.ToString(); ;
        prodCoro = Produce();
    }

    IEnumerator Produce()
    {
        while (OnOff)
        {
            yield return new WaitForSeconds(5);
            if(OnOff)
                rawMetals++;
            counter.text = "Mining\n" + rawMetals.ToString();
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
            counter.text = "Stopped\n" + rawMetals.ToString();
        } else {
            OnOff = true;
            StartCoroutine(prodCoro);
            counter.text = "Mining\n" + rawMetals.ToString();
        }
    }

    public void updateDisplay()
    {
        if (OnOff)
            counter.text = "Mining\n" + rawMetals.ToString();
        else
            counter.text = "Stopped\n" + rawMetals.ToString();
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ResourceManager>().rawMetal += rawMetals;
            rawMetals = 0;
            if(mining)
                counter.text = "Mining\n" + rawMetals.ToString();
            else
                counter.text = "Stopped\n" + rawMetals.ToString();
        }
    }*/
}
