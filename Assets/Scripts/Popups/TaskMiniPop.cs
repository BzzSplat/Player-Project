using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMiniPop : MonoBehaviour
{
    public Computer comp;
    public void deleteTask()
    {
        comp.tasks.RemoveAt(transform.GetSiblingIndex());
        Destroy(gameObject);
    }
}
