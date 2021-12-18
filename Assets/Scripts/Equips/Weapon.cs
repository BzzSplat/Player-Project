using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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

    void Update()
    {
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


    void ShootProjectile(GameObject Projectile,float throwForce)
    {
        


        var proj = Instantiate(Projectile, startPos.position, Quaternion.identity);

        if(Projectile.GetComponent<Explosive>())
        proj.GetComponent<Explosive>().creator = this.gameObject; //set owner of the explosive so no immediate explosion

        Physics.IgnoreCollision(proj.GetComponent<Collider>(), Holder.GetComponent<Collider>());

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
}
