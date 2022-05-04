using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveplanet : MonoBehaviour
{
    public int speed;
    Vector3 startPosition;
    Vector3 endPosition;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed, 0, 0);
        startPosition = this.transform.position;
        endPosition = new Vector3(-10, 0, 0);
    }

    void FixedUpdate()
    {

        /*if(rb.position == endPosition) //no, movement is dynamic not fixed
        {
            StartCoroutine(Vector3LerpCoroutine(this.gameObject, startPosition, speed));
            transform.position = startPosition;
        }
        if(rb.position == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(this.gameObject, endPosition, speed));
            transform.position = endPosition;
        }*/
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector2 startPosition = obj.transform.position;
        float time = 0f;

        while (rb.position != target)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, target, (time / Vector2.Distance(startPosition, target)) * speed));
            time += Time.deltaTime;
            yield return null;
        }
    }
}
