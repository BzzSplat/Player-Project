using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool needsPlayer = true;
    public GameObject Player;

    public bool hasInports = false;
    public Miner Inports;

    public virtual void Interaction() {} //do anyhting you like in it

    public virtual GameObject popupMenu(GameObject menu, GameObject playerCanvas) //open a menu of your choice on the player's screen
    {
        GameObject popup = Instantiate(menu, playerCanvas.transform);
        Camera camera = playerCanvas.GetComponentInParent<Camera>();
        camera.GetComponent<CamControl>().freeMouse();

        if(popup.GetComponent<ProducerMenu>())
            popup.GetComponent<ProducerMenu>().cam = camera.GetComponent<CamControl>();
        if (popup.GetComponent<ConverterMenu>())
            popup.GetComponent<ConverterMenu>().cam = camera.GetComponent<CamControl>();
        return popup;
    }
}
