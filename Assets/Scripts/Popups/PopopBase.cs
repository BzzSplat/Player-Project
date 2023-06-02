using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopopBase : MonoBehaviour
{
    public CamControl cam;

    public void close()
    {
        cam.trapMouse();
        Destroy(gameObject);
    }
}
