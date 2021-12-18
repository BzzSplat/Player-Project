using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveplanet : MonoBehaviour
{
    public int speed = 10;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        endPosition = new Vector3(-10, 0, 0);
    }

    void FixedUpdate()
    {
        if(rBody.position == endPosition)
        {
            //StartCoroutine(Vector3LerpCoroutine(this.gameObject, startPosition, speed));
            //transform.position = startPosition;
        }
        if(rBody.position == startPosition)
        {
            //StartCoroutine(Vector3LerpCoroutine(this.gameObject, endPosition, speed));
            //transform.position = endPosition;
        }
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector2 startPosition = obj.transform.position;
        float time = 0f;

        while (rBody.position != target)
        {
            rBody.MovePosition(Vector2.Lerp(startPosition, target, (time / Vector2.Distance(startPosition, target)) * speed));
            time += Time.deltaTime;
            yield return null;
        }
    }
}
