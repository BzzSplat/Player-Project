using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WireTask
{
    public UnityEvent workFunc;
    public string triggerName, triggerObjectName, workName, workObjectName;

    public WireTask(string tN, string tON, string wN, string wON, UnityEvent wF)
    {
        triggerName = tN;
        triggerObjectName = tON;
        workName = wN;
        workObjectName = wON;
        workFunc = wF;
    }
}
