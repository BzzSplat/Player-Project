using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMenu : MonoBehaviour //if producers ever generate multiple types use lists for text and whatnot
{
    public Text resourceCounter, objectName;
    public ResourceManager playerInv;
    public int outputID;
    GameObject source;

    void Update()
    {
        
    }

    public void collectResources()
    {
        switch (outputID)
        {
            case 0:
                playerInv.rawMetal
        }
    }

    public void closePopup()
    {
        Destroy(gameObject);
    }
}
