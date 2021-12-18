using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    GameObject CurrentItem;
    public List<GameObject> Items = new List<GameObject>();
    [SerializeField]
    Transform spawnPos;

    private void Start()
    {
        SwitchEquip(Items[0]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchEquip(Items[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchEquip(Items[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchEquip(Items[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchEquip(Items[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchEquip(Items[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SwitchEquip(Items[5]);
        }
    }

    void SwitchEquip(GameObject newItem)
    {
        Debug.Log("Switching to " + newItem);
        if (CurrentItem)
            StartCoroutine(delOldItem(transform.GetChild(1).GetChild(0).gameObject));

        CurrentItem = newItem;
        GameObject equipped = Instantiate(newItem, spawnPos);
        equipped.GetComponent<Weapon>().camera = GetComponent<Camera>();
        equipped.GetComponent<Weapon>().Holder = transform.parent.gameObject;
    }

    IEnumerator delOldItem(GameObject oldItem)
    {
        Destroy(oldItem);
        yield return null;
    }
}

