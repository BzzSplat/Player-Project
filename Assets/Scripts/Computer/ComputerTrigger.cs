using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTrigger : MonoBehaviour
{
    [SerializeField]
    public WireTask task;
    public string triggerName;

    public void Trigger()
    {
        try
        {
            task.workFunc.Invoke();
        }
        catch { }
    }
}
