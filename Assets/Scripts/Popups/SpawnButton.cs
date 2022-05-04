using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField]
    GameObject thing;
    [SerializeField]
    GUI_Menu guiScript;
    [SerializeField]
    int[] ID, cost;

    private void Start()
    {
        guiScript = transform.parent.parent.parent.parent.GetComponent<GUI_Menu>();
    }

    public void callSpawn()
    {
        guiScript.spawnitem(ID, cost, thing);
    }

    public void showCost()
    {
        guiScript.costSelect(ID, cost);
    }

    public void hideCost()
    {
        guiScript.costDeselect();
    }
}
