using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableObject
{
    public override void Interaction()
    {
        GetComponent<ComputerTrigger>().Trigger();
    }
}
