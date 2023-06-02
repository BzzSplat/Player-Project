using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    Vector3 spawnLocation;
    public float healthMax = 100;
    public float health = 100;
    bool dead = false;

    public bool canBreathe = true, suffocating = false;
    IEnumerator suffocate;
    public float oxygenMax = 10;
    public float oxygen = 10;

    [SerializeField]
    Image oxyImg, hpImg;
    [SerializeField]
    Text oxyTxt, hpTxt;

    [SerializeField]
    bool MattMode = true;
    [SerializeField]
    AudioSource soundPlayer;
    [SerializeField]
    AudioClip[] mattSounds;


    private void Start()
    {
        suffocate = suffocateCoro();
        spawnLocation = transform.position;

        if (MattMode)
        {
            StartCoroutine("mattTalk");
            transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
    }

    void Update()

    {
        transform.GetChild(2).Rotate(0, 90 * Time.deltaTime, 0);

        if (health < 1 && dead == false)//die
        {
            dead = true;
            StartCoroutine(death());
        }
        else if (health < 0)
            health = 0;

        if (!canBreathe && !suffocating)
        {
            suffocating = true;
            StartCoroutine(suffocate);
            Debug.Log(gameObject.name + " can't breathe!");
        }
        else if (suffocating && canBreathe)
        {
            StopCoroutine(suffocate);
            Debug.Log(gameObject.name + " can breathe again.");
            suffocating = false;
            oxygen = oxygenMax;
        }

        oxyImg.fillAmount = oxygen / oxygenMax;
        hpImg.fillAmount = health / healthMax;
        oxyTxt.text = oxygen.ToString();
        hpTxt.text = health.ToString();
    }

    IEnumerator suffocateCoro()
    {
        yield return new WaitForSeconds(1);
        while (!canBreathe)
        {
            if (oxygen > 0)
            {
                yield return new WaitForSeconds(1);
                oxygen--;
            }
            else
            {
                yield return new WaitForSeconds(1);
                health -= 7;
            }
        }
    }

    IEnumerator death()
    {
        GetComponent<Rigidbody>().freezeRotation = false;
        if (MattMode)
        {
            soundPlayer.clip = mattSounds[0];
            soundPlayer.Play();
        }

        yield return new WaitForSeconds(10);

        transform.position = spawnLocation;
        health = 100;
        dead = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        transform.rotation = Quaternion.identity;
    }

    IEnumerator mattTalk()
    {
        while (MattMode)
        {
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            if (!dead)
            {
            int x = Random.Range(1, 5);
            soundPlayer.clip = mattSounds[x];
            soundPlayer.Play();
            }
        }
    }
}
