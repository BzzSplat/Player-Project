using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject creator;
    public GameObject explosion;
    [SerializeField]
    bool contactExplosive;

    private void OnCollisionEnter(Collision collision)
    {
        if (contactExplosive && collision.gameObject != creator)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
