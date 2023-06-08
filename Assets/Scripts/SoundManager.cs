using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Musics;


    private void Start()
    {
        GetComponent<AudioSource>().clip = Musics[Random.Range(0, Musics.Length)];
        GetComponent<AudioSource>().Play();
    }

    private void FixedUpdate()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = Musics[Random.Range(0, Musics.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
