using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPwPF : MonoBehaviour
{
    public float maxupspeed = 1.0f;
    Rigidbody2D rigid2d;
    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    private float upspeed = 0;
    private bool ispowered = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        rigid2d.velocity = new Vector3(0, upspeed, 0);
        upspeed -= (Time.deltaTime / 2);
        if(upspeed < 0) upspeed = 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Steam")) {
            upspeed += 2 * Time.deltaTime;
            if (upspeed > maxupspeed) upspeed = maxupspeed;
        }
    }
}
