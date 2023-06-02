using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public SpawnPad spawn;
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
            spawn.respawnPlayer(gameObject);
        }

        if (!canBreathe && !suffocating)
        {
            suffocating = true;
            StartCoroutine(suffocate);
        }
        else if (suffocating && canBreathe)
        {
            StopCoroutine(suffocate);
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
