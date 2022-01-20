using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject laserBeam;
    LineRenderer lineRend;
    [SerializeField]
    Weapon gunScript;

    private void Start()
    {
        lineRend = laserBeam.GetComponent<LineRenderer>();
        lineRend.SetPosition(0, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!gunScript.Holder)
                return;

            if (gunScript.camera.GetComponent<CamControl>().freeCursor == true)
                return;

            Ray ray = gunScript.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            float endPoint = 1000;

            if (Physics.Raycast(ray, out hit))
                if (hit.distance < 1000)
                    endPoint = hit.distance;
                else
                    endPoint = 1000;

            lineRend.SetPosition(1, new Vector3(-0.2f, endPoint, 0.114f));
            StartCoroutine("laserEffect");
        }
    }

    IEnumerator laserEffect()
    {
        laserBeam.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        laserBeam.SetActive(false);
    }
}
