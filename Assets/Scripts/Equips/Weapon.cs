using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
    //Make this one of those virtual classes and have the methods set to fire1 and fire 2, maybe others too.
    //Then just comment the current projectile/raycast shoot code below for easy access when needed.
    //THis should allow fore more creativity with weapons and such yadda yadda.
{
    public GameObject Projectile1;
    public float Projectile1ThrowForce;

    public GameObject Projectile2;
    public float Projectile2ThrowForce;

    public Transform startPos;
    public new Camera camera;
    public GameObject Holder = null;

    [SerializeField]
    bool fire1_Projectile, fire2_Projectile, fire1_Raycast, fire2_Raycast;

    bool pickupable;

    public GameObject selfFab;

    void Update()
    {
        if (!Holder)//no holder no needs
            return;

        if (camera.GetComponent<CamControl>().lockCamera == true) //if we don't want the camera to move we also probably don't want to be able to shoot
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(fire1_Projectile)
                ShootProjectile(Projectile1, Projectile1ThrowForce);
            if(fire1_Raycast)
                Castaray();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(fire2_Projectile)
                ShootProjectile(Projectile2, Projectile2ThrowForce);
            if (fire2_Raycast)
                Castaray();
        }
    }


    void ShootProjectile(GameObject Projectile,float throwForce) //shoot a prefab 
    {
        var proj = Instantiate(Projectile, startPos.position, Quaternion.identity);

        if(Projectile.GetComponent<Explosive>())
        proj.GetComponent<Explosive>().creator = this.gameObject; //set owner of the explosive so no immediate explosion

        Physics.IgnoreCollision(proj.GetComponent<Collider>(), Holder.GetComponent<Collider>());

        if(camera)
        proj.GetComponent<Rigidbody>().velocity = (camera.transform.forward * throwForce) + Holder.GetComponent<Rigidbody>().velocity;
    }

    void Castaray()
    {
        /*Ray ray = camera.ViewportPointToRay(new Vector3(0, 0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            endPoint = hit.point;
        else
            endPoint = ray.GetPoint(1000);
        Debug.DrawRay(startPos.position, (endPoint - startPos.position), Color.red, 50f);*/
    }

    public void dropWeapon() //enable physics stuff
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().isKinematic = false;
        if (GetComponent<Collider>())
            GetComponent<Collider>().enabled = true;

        StartCoroutine(pickupDelay());
    }

    public void pickUpWeapon() //disable physics stuff
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().isKinematic = true;
        if (GetComponent<Collider>())
            GetComponent<Collider>().enabled = false;
    }

    IEnumerator pickupDelay()
    {
        yield return new WaitForSeconds(2);
        pickupable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !pickupable) //has to collide with player and able to be picked up
            return;

        List<GameObject> Items = collision.transform.GetChild(0).GetComponent<Equip>().Items;
        bool hasSelf = false;
        bool foundInsert = false;
        int insertIndex = -1;

       for (int i = 0; i < Items.Count; i++) //check if player already has this item 1and where the first open space is
       {
            if (Items[i] == selfFab)
                hasSelf = true;
            if (!foundInsert && !Items[i])
            {
                insertIndex = i;
                foundInsert = true;
            }

       }

        if (!hasSelf && insertIndex != -1) //if player does not have the item already and if there is space for a new item pick it up
        {
            Items[insertIndex] = selfFab;
            Destroy(gameObject);
        }
            
    }
}
