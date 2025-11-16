using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpplatformsound : MonoBehaviour
{
    AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audio.clip = clip;
        audio.Play();
    }
}
