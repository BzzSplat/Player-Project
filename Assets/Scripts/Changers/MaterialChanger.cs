using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    Renderer rend;
    [SerializeField]
    Color mainColor, emmissionColor;
    [SerializeField]
    bool useMainColor, useEmissionColor, continuousUpdate, scroll;
    [SerializeField]
    Vector2 scrollSpeed;
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        work();
    }

    private void Update()
    {
        if(scroll)
            rend.material.SetTextureOffset("_MainTex", rend.material.mainTextureOffset + (scrollSpeed * Time.deltaTime));

        if (continuousUpdate)
            work();
    }

    void work()
    {
        if (useMainColor)
            rend.material.color = mainColor;

        if (useEmissionColor)
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", emmissionColor);
        }
    }
}
