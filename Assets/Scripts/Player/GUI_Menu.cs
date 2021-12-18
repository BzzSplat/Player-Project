using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Menu : MonoBehaviour
{
    public GameObject cube;
    public new Camera camera;
    public GameObject menuScreen;
    [SerializeField]
    ResourceManager materials;

    public void spawnCube()
    {
        if (materials.rawMetal <= 0)
            return;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit))
            endPoint = hit.point;
        else
            endPoint = ray.GetPoint(1000);

        Instantiate(cube, endPoint, Quaternion.identity);
        materials.rawMetal--;//spawn cost
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            menuScreen.SetActive (true);
            camera.GetComponent<CamControl>().freeMouse();
            camera.GetComponent<CamControl>().lockCamera = true;
        }
            
        if (Input.GetKeyUp(KeyCode.Q))
        {
            menuScreen.SetActive(false);
            camera.GetComponent<CamControl>().trapMouse();
            camera.GetComponent<CamControl>().lockCamera = false;
        } 
    }
}
