using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTool : MonoBehaviour
{
    [SerializeField]
    bool readyToMake;
    [SerializeField]
    ComputerTrigger trigger;
    [SerializeField]
    ComputerWorker worker;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !readyToMake)
        {
            if (retriveReference())
                readyToMake = trigger && worker;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && readyToMake)
        {
            if (createTask())
            {
                readyToMake = false;
                trigger = null;
                worker = null;
            }

        }
    }

    bool retriveReference() //get the info of the computer hit, if didn't hit a computer return false
    {
        Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<ComputerTrigger>())
            {
                trigger = hit.transform.GetComponent<ComputerTrigger>();
                return true;
            }
            else if (hit.transform.GetComponent<ComputerWorker>())
            {
                worker = hit.transform.GetComponent<ComputerWorker>();
                return true;
            }
        }
        return false;
    }

    bool createTask()
    {
        Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f) && hit.transform.GetComponent<Computer>())
        {
            if (hit.transform.GetComponent<Computer>().tasks.Count < hit.transform.GetComponent<Computer>().maxTasks) //check if the computer can hold a new task
            {
                WireTask task = new WireTask(trigger.triggerName, trigger.gameObject.name, worker.workFuncName, worker.gameObject.name, worker.workFunc);

                hit.transform.GetComponent<Computer>().tasks.Add(task);
                trigger.task = task;

                return true;
            }
        }
        return false;
    }
}
