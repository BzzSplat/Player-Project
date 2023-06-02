using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadiusColorHeight : MonoBehaviour
{
    [SerializeField]
    float min, max;
    [SerializeField]
    Gradient grad;
    [SerializeField]
    Texture2D tex;
    const int texRes = 50;

    private void Update()
    {
        GetComponent<MeshRenderer>().sharedMaterial.SetVector("_elevationMinMax", new Vector4(min, max));
        UpdateColors();
    }

    private void UpdateColors()
    {
        Color[] colors = new Color[texRes];
        for (int i = 0; i < texRes; i++)
        {
            colors[i] = grad.Evaluate(i / (texRes - 1f));
        }
        tex.SetPixels(colors);
        tex.Apply();
        GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_texture", tex);
    }
}
