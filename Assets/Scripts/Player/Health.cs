using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Vector3 spawnLocation = new Vector3(2, 1, -6); //eventually will be a gameobject and the position and velocity will be tied to the spawner/cloner
    public float healthMax = 100;
    public float health = 100;

    public bool canBreathe = true, suffocating = false;
    IEnumerator suffocate;
    [SerializeField]
    public float oxygenMax = 10;
    public float oxygen = 10;

    private void Start()
    {
        suffocate = suffocateCoro();
    }

    void Update()
    {
        if (health < 1)//die
        {
            health = 100;
            transform.position = spawnLocation;
        }

        if (!canBreathe && !suffocating)
        {
            suffocating = true;
            StartCoroutine(suffocate);
            Debug.Log("There's no air here!");
        }
        else if (suffocating && canBreathe)
        {
            StopCoroutine(suffocate);
            Debug.Log("I can breathe again.");
            suffocating = false;
            oxygen = oxygenMax;
        }

    }

    IEnumerator suffocateCoro()
    {
        yield return new WaitForSeconds(1);
        while (!canBreathe)
        {
            if(oxygen > 0)
            {
                yield return new WaitForSeconds(1);
                oxygen--;
            }
            else
            {
                yield return new WaitForSeconds(1);
                health -= 7;
            }
        }
    }
}
