using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Uses of resources, spawning/assembeling things, hand crafting things, firing certain weapons
public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    public float rawMetal, metalTier1;
    [SerializeField]
    GameObject RMC, MT1C;

    private void Start()
    {
        rawMetal = 11;
    }

    void Update()
    {
        RMC.GetComponent<Text>().text = rawMetal.ToString();
        MT1C.GetComponent<Text>().text = metalTier1.ToString();
    }
}
