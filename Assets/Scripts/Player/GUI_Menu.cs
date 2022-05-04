using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Menu : MonoBehaviour
{
    public new Camera camera;
    public GameObject menuScreen, materialsCover, costBoard, costPrefab;
    [SerializeField]
    List<GameObject> buttonInspects = new List<GameObject>();
    public Silo silo;
    [SerializeField]
    Health playerHealth;

    [SerializeField]
    Image cross1, cross2, oxygenMeter;
    [SerializeField]
    Text health;
    [SerializeField]
    Text[] materialCounts;

    public void spawnitem(int[] ID, int[] cost, GameObject thing)
    {
        if (!silo)
            return;

        bool costCheck = false;
        for(int i = 0; i < ID.Length; i++) //check if player can afford the item
        {
            if (silo.materials[ID[i]] - cost[i] < 0)
                costCheck = true;
        }
        if (costCheck)
            return;

        for (int i = 0; i < ID.Length; i++)
        {
            silo.materials[ID[i]] -= cost[i]; //payment time
        }

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit))
            endPoint = hit.point;
        else
            endPoint = ray.GetPoint(1000);

        Instantiate(thing, endPoint, Quaternion.identity);
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

        if(menuScreen.activeSelf == true)
        {
            if (silo)
            {
                materialsCover.SetActive(false);
                for (int i = 0; i < materialCounts.Length; i++)
                {
                    materialCounts[i].text = silo.materials[i].ToString();
                }
            }
            else
                materialsCover.SetActive(true);
        }
    }

    public void changeCrosshairColor(Color clr)
    {
        cross1.color = clr;
        cross2.color = clr;
    }

    public void costSelect(int[] ID, int[] cost)
    {
        for(int i = 0; i < ID.Length; i++)
        {
            GameObject newCost = Instantiate(costPrefab, costBoard.transform);
            newCost.GetComponent<Image>().sprite = materialCounts[ID[i]].transform.parent.GetComponent<Image>().sprite;
            newCost.transform.GetChild(0).GetComponent<Text>().text = cost[i].ToString();
            buttonInspects.Add(newCost);
        }
    }

    public void costDeselect()
    {
        foreach (GameObject cost in buttonInspects)
        {
            Destroy(cost);
        }
        buttonInspects.Clear();
    }
}
