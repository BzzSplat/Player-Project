using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Miner : InteractableObject
{
    public float rawMetals = 0;
    public bool mining = true;
    [SerializeField]
    Text counter;

    private void Start()
    {
        StartCoroutine(Mine());
        if(mining)
            counter.text = "Online";
        else
            counter.text = "Offline";
    }

    public override void Interaction()
    {
        if (mining) {
            mining = false;
            StopCoroutine(Mine());
            counter.text = "Offline";
        } else {
            mining = true;
            StartCoroutine(Mine());
            counter.text = "Online";
        }
    }

    IEnumerator Mine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            rawMetals++;
            counter.text = rawMetals.ToString();
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ResourceManager>().rawMetal += rawMetals;
            rawMetals = 0;
            counter.text = rawMetals.ToString();
        }
    }
}
