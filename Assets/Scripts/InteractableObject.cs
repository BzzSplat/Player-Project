using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public virtual void Interaction() {} //do anyhting you like in it

    public virtual GameObject popupMenu(GameObject menu, GameObject playerCanvas) //open a menu of your choice on the player's screen
    {
        GameObject popup = Instantiate(menu, playerCanvas.transform);
        Camera camera = playerCanvas.GetComponentInParent<Camera>();
        camera.GetComponent<CamControl>().freeMouse();
        return popup;
    }

    public virtual GameObject popupMenuProducer(GameObject menu, GameObject playerCanvas, int outputID)
    {
        GameObject popup = Instantiate(menu, playerCanvas.transform);
        Camera camera = playerCanvas.GetComponentInParent<Camera>();
        camera.GetComponent<CamControl>().freeMouse();
        return popup;
    }

    public virtual GameObject popupMenuConverter(GameObject menu, GameObject playerCanvas, int[] inputIDs, int outputID)
    {
        GameObject popup = Instantiate(menu, playerCanvas.transform);
        Camera camera = playerCanvas.GetComponentInParent<Camera>();
        camera.GetComponent<CamControl>().freeMouse();
        return popup;
    }
}
