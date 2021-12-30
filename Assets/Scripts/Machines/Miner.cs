using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Miner : InteractableObject
{
    public float rawMetals = 0;
    public bool mining = false;
    [SerializeField]
    Text counter;
    IEnumerator mineCoro;
    [SerializeField]
    GameObject menu, popup;
    public GameObject player;

    private void Start()
    {
        counter.text = "Stopped\n" + rawMetals.ToString(); ;
        mineCoro = Mine();
    }

    IEnumerator Mine()
    {
        while (mining)
        {
            yield return new WaitForSeconds(5);
            if(mining)
                rawMetals++;
            counter.text = "Mining\n" + rawMetals.ToString();
        }
        
    }

    public override void Interaction()
    {
        popup = popupMenuProducer(menu, player.transform.GetChild(0).GetChild(2).gameObject, 0);
    }

    /*public override void Interaction()
    {
        if (mining) {
            mining = false;
            StopCoroutine(mineCoro);
            counter.text = "Stopped\n" + rawMetals.ToString();
        } else {
            mining = true;
            StartCoroutine(mineCoro);
            counter.text = "Mining\n" + rawMetals.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
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
