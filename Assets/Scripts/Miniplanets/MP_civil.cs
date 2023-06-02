using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_civil : MonoBehaviour
{
    [SerializeField]
    GameObject tower;
    public float  age, resources, yearLength, buildings, buildingMult, buildingChance, buildingCost;

    private void Start()
    {
        StartCoroutine("MiniYear");
    }

    private IEnumerator MiniYear ()
    {
        while (true)
        {
            yield return new WaitForSeconds(yearLength);
            age++;

            //update buildings
            if (resources + buildings < 200000000f)
                resources += buildings;
            else
                resources = 2000000000;

            if (Random.Range(0f,1f) <= buildingChance)
                MakeBuilding();
        }
    }

    private void MakeBuilding()
    {
        float x = resources - buildingCost * (buildingMult * buildings + 1);
        if (x < 0)
            return;
        else
            resources = x;
        buildings++;

        GameObject b = Instantiate(tower, transform);
        b.transform.rotation = Random.rotation;
        b.transform.position += b.transform.up * 0.52f;

        print("Made a building, cost: " + (buildingCost * (buildingMult * buildings + 1)));
    }
}
