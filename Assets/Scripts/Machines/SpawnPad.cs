using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPad : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab, siloPrefab, startCam;
    AudioSource sounds;

    private void Start()
    {
        sounds = GetComponent<AudioSource>();
    }

    public void respawnPlayer(GameObject player)
    {
        Vector3 pos = transform.position + (transform.up * 1);
        Instantiate(player, pos, Quaternion.identity).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity; //spawn copy of new player
        sounds.Play();
        Destroy(player); //remove old player
    }

    public void clonePlayer(GameObject player)
    {
        Vector3 pos = transform.position + (transform.up * 1);
        Instantiate(player, pos, Quaternion.identity).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity; //spawn copy of new player
        sounds.Play();
    }

    public void startPlayer()
    {
        Vector3 pos = transform.position + (transform.up * 1);
        GameObject p = Instantiate(playerPrefab, pos, Quaternion.identity); //spawn a new player
        p.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        p.GetComponent<Health>().spawn = this;
        sounds.Play();
        Destroy(startCam);
        Instantiate(siloPrefab, pos, Quaternion.identity).GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity; //give the player a silo
    }
}
