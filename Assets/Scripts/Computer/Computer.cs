using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : InteractableObject
{
    public int maxTasks;
    public List<WireTask> tasks = new List<WireTask>();
    [SerializeField]
    GameObject popup;

    public override void Interaction()
    {
        displayPopup(popup).GetComponent<ComputerPopup>().comp = this;
    }
}
