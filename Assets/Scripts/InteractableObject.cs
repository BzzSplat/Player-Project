using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool needsPlayer;
    public GameObject Player;

    public virtual void Interaction() {} //do anyhting you like in it

}
