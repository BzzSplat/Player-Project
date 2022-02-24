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
        if (Input.GetKeyDown(KeyCode.Alpha1) && Items[0])
        {
            SwitchEquip(Items[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Items[1])
        {
            SwitchEquip(Items[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Items[2])
        {
            SwitchEquip(Items[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Items[3])
        {
            SwitchEquip(Items[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && Items[4])
        {
            SwitchEquip(Items[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && Items[5])
        {
            SwitchEquip(Items[5]);
        }

        if (Input.GetKeyDown(KeyCode.X))
            dropItem();
    }

    void SwitchEquip(GameObject newItem)
    {
        if (CurrentItem && transform.GetChild(1).GetChild(0))
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

    void dropItem()//spawn new welder item, then enable its physicsy stuff
    {
        if (CurrentItem == Items[0])//can't drop your hands
            return;
        Debug.Log("Dropping " + CurrentItem);

        int itemIndex = 0;

        GameObject dropped = Instantiate(CurrentItem, spawnPos);
        if (CurrentItem)
        {
            StartCoroutine(delOldItem(transform.GetChild(1).GetChild(0).gameObject));
            for(int i = 0; i < Items.Count; i++)
            {
                if (CurrentItem == Items[i])
                    //Items[i] = null;
                    itemIndex = i;
            }
        }

        dropped.GetComponent<Weapon>().camera = GetComponent<Camera>();
        dropped.GetComponent<Weapon>().dropWeapon();
        dropped.GetComponent<Weapon>().selfFab = Items[itemIndex]; //do this otherwise it sets selfFab to itself and not the prefab
        dropped.transform.parent = null;
        dropped.GetComponent<Rigidbody>().velocity = GetComponentInParent<Rigidbody>().velocity;

        Items[itemIndex] = null;

        SwitchEquip(Items[0]);
    }
}

