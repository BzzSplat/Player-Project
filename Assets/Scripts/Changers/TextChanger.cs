using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField]
    Text[] texts;
    [SerializeField]
    string[] wordChanges;
    [SerializeField]
    bool changeWords;

    private void Start()
    {
        if (changeWords)
        {
            for (int i = 0; i < texts.Length; i++)
                texts[i].text = wordChanges[i];
        }
    }
}
