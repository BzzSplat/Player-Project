using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Menu : MonoBehaviour
{
    public new Camera camera;
    public GameObject menuScreen;
    [SerializeField]
    ResourceManager materials;
    [SerializeField]
    Health playerHealth;

    [Header("Base Components"), SerializeField]
    Image cross1, cross2, oxygenMeter;
    [SerializeField]
    Text health;


    [Header("Spawn Menu")]
    [SerializeField]
    List<GameObject> spawnables = new List<GameObject>();
    [SerializeField]
    List<int> spawnCosts = new List<int>();


    public void spawnitem(int index)
    {
        if (materials.materials[1] < spawnCosts[index])
            return;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit))
            endPoint = hit.point;
        else
            endPoint = ray.GetPoint(1000);

        Instantiate(spawnables[index], endPoint, Quaternion.identity);
        materials.materials[1] -= spawnCosts[index];//spawn cost
    }

    void Update()
    {
        health.text = playerHealth.health.ToString() + "hp";
        oxygenMeter.fillAmount = playerHealth.oxygen / playerHealth.oxygenMax;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            menuScreen.SetActive (true);
            camera.GetComponent<CamControl>().freeMouse();
        }
            
        if (Input.GetKeyUp(KeyCode.Q))
        {
            menuScreen.SetActive(false);
            camera.GetComponent<CamControl>().trapMouse();
        }

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.transform.GetComponent<InteractableObject>())
            {
                changeCrosshairColor(Color.green);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.transform.GetComponent<InteractableObject>().needsPlayer)
                        hit.transform.GetComponent<InteractableObject>().Player = camera.transform.parent.gameObject;
                    hit.transform.GetComponent<InteractableObject>().Interaction();
                }

            }
        }
        else
        {
            changeCrosshairColor(Color.white);
        }

    }

    public void changeCrosshairColor(Color clr)
    {
        cross1.color = clr;
        cross2.color = clr;
    }
}
