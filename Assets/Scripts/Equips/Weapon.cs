using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
    //Make this one of those virtual classes and have the methods set to fire1 and fire 2, maybe others too.
    //Then just comment the current projectile/raycast shoot code below for easy access when needed.
    //This should allow fore more creativity with weapons and such yadda yadda.
{
    [SerializeField]
    AudioSource audioPlayer;
    [SerializeField]
    AudioClip shootSound1, shootSound2;

    public GameObject Projectile1;
    public float Projectile1ThrowForce;

    public GameObject Projectile2;
    public float Projectile2ThrowForce;

    [SerializeField]
    float ray1Range, ray1Damage, ray2Range, ray2Damage;
    [SerializeField]
    GameObject ray1HitEffect, ray2HitEffect;

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

        if (camera.GetComponent<CamControl>().freeCursor == true) //if we don't want the camera to move we also probably don't want to be able to shoot
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(fire1_Projectile)
                ShootProjectile(Projectile1, Projectile1ThrowForce);
            if(fire1_Raycast)
                Castaray(ray1Range, ray1Damage, ray1HitEffect);

            if(shootSound1)
                audioPlayer.PlayOneShot(shootSound1);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(fire2_Projectile)
                ShootProjectile(Projectile2, Projectile2ThrowForce);
            if (fire2_Raycast)
                Castaray(ray2Range, ray2Damage, ray2HitEffect);

            if (shootSound2)
                audioPlayer.PlayOneShot(shootSound2);
        }

        //Thanks to Plai on YouTube
        float smooth = 16, swayMult = 4;
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMult, mouseY = Input.GetAxisRaw("Mouse Y") * swayMult;
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
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

    void Castaray(float range, float damage, GameObject hitEffect)
    {
        Ray ray = GetComponent<Weapon>().camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit))
            endPoint = hit.point;
        else
            endPoint = ray.GetPoint(1000);

        if (hit.transform)
        {
            /*if (hit.transform.GetComponent<Rigidbody>()) //push the object
            {

            }*/

            if(hit.transform.GetComponent<Health>())
            {
                hit.transform.GetComponent<Health>().health -= damage;
            }
            else if (hit.transform.GetComponent<Dummy>())
            {
                hit.transform.GetComponent<Dummy>().health -= damage;
            }

            Instantiate(hitEffect, endPoint, Quaternion.identity, hit.transform);
        }
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
