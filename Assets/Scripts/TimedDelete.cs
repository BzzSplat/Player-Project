using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDelete : MonoBehaviour
{
    [SerializeField]
    float lifeTime;
    [SerializeField]
    bool deleteOnTrigger;

    void Start()
    {
        if(!deleteOnTrigger)
            StartCoroutine(life(lifeTime));
    }

    public void trigger ()
    {
        StartCoroutine(life(lifeTime));
    }

    IEnumerator life(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
