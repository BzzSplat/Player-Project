using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloPopup : MonoBehaviour
{
    [SerializeField]
    Text[] materials;
    public Silo silo;
    public CamControl cam;

    void Update()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].text = silo.materials[i].ToString();
        }
    }

    public void close()
    {
        cam.trapMouse();
        Destroy(gameObject);
    }
}
