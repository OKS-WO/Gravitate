using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_bomb_killzone : MonoBehaviour
{
    public float timecnt;
    private float destroyoffset = 0;

    // Update is called once per frame
    void Update()
    {
        destroyoffset += Time.deltaTime;
        if (destroyoffset > timecnt) Destroy(gameObject);
    }
}
