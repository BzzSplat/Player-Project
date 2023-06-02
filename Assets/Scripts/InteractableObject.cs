using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool needsPlayer;
    public GameObject Player;

    public virtual void Interaction() {} //do anyhting you like in it

    public GameObject displayPopup (GameObject popup)
    {
        Transform spawn = Player.transform.GetChild(0).GetChild(2);
        Player.transform.GetChild(0).GetComponent<CamControl>().freeMouse();

        GameObject Pops = Instantiate(popup, spawn);
        Pops.GetComponent<PopopBase>().cam = Player.transform.GetChild(0).GetComponent<CamControl>();
        return popup;
    }

}
