using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField]
    Color mainColor, emmissionColor;
    [SerializeField]
    bool useMainColor, useEmissionColor;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        if (useMainColor)
            rend.material.color = mainColor;

        if (useEmissionColor)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", emmissionColor);
        }
    }
}
