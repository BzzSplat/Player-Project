using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDelete : MonoBehaviour
{
    [SerializeField]
    float lifeTime;

    void Start()
    {
        StartCoroutine(life(lifeTime));
    }

    IEnumerator life(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
