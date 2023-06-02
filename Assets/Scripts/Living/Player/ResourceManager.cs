using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Uses of resources, spawning/assembeling things, hand crafting things, firing certain weapons
public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    public List<float> materials = new List<float>();
    [SerializeField]
    Text[] materialDisplay = new Text[2];

    private void Start()
    {
        materials[0] = 10;
        materials[1] = 10;
    }

    void Update()
    {
        //materialDisplay[0].text = materials[0].ToString();
        //materialDisplay[1].text = materials[1].ToString();
    }
}
