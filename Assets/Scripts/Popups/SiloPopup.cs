using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloPopup : MonoBehaviour
{
    [SerializeField]
    Text[] materials;
    public Silo silo;
    public CamControl cam;
    [SerializeField]
    Transform machinesMenu;
    [SerializeField]
    GameObject miniPop, miniCounter;
    [SerializeField]
    Sprite[] icons;

    void Update()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].text = silo.materials[i].ToString();
        }
    }

    public void close()
    {
        cam.trapMouse();
        Destroy(gameObject);
    }

    private void Start() //check silo connections and add them to scroll menu
    {
        foreach(Machine machine in silo.connections)
        {
            GameObject pop = Instantiate(miniPop, machinesMenu);
            pop.transform.GetChild(0).GetComponent<Text>().text = machine.name;

            int[,] info = machine.getWorkInfo();
            for (int y = 0; y < info.GetLength(0); y++)
            {
                GameObject miniC = Instantiate(miniCounter, pop.transform.GetChild(1));
                miniC.GetComponent<Image>().sprite = icons[info[y, 0]];
                miniC.transform.GetChild(0).GetComponent<Text>().text = info[y, 1].ToString();
            }

        }
    }

}
