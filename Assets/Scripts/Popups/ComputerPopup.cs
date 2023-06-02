using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerPopup : PopopBase
{
    public Computer comp;
    [SerializeField]
    Transform taskArea;
    [SerializeField]
    GameObject miniPop;

    private void Start()
    {
        foreach (WireTask t in comp.tasks)
        {
            GameObject taskPop = Instantiate(miniPop, taskArea);
            taskPop.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = t.triggerName + $" ({t.triggerObjectName})";
            taskPop.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = t.workName + $" ({t.workObjectName})";
            taskPop.GetComponent<TaskMiniPop>().comp = comp;
        }
    }
}
