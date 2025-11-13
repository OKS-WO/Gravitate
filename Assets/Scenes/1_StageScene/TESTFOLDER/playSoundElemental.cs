using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundElemental : MonoBehaviour
{
    AudioSource audio;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = sound;
        audio.Play();
    }

}
