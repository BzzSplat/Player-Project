using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : InteractableObject
{
    [SerializeField]
    AudioClip[] songs;
    [SerializeField]
    AudioSource soundPlayer;

    bool isPlaying = false;

    // Start is called before the first frame update
    public override void Interaction()
    {
        if (isPlaying)
        {
            soundPlayer.Stop();
            isPlaying = false;
        }
        else
        {
            int x = Random.Range(0, songs.Length);
            soundPlayer.clip = songs[x];
            soundPlayer.Play();

            isPlaying = true;
        }
    }
}
