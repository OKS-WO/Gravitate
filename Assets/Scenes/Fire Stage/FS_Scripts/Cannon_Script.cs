using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon_Script : MonoBehaviour
{
    private int randnum = 0;
    private float timecnt = 0;
    private int shotrecent = 0;
    private int shotstartcmd = 0;

    public float Shooting_Start_Offset = 0f;
    public int Shooting_Chance_Per_Frame = 1;
    public float minimumShootingGapSec = 1.2f;
    // Start is called before the first frame update

    Vector2 spawnpos;

    void Start() {
        spawnpos = gameObject.transform.position; spawnpos.x += 0.3f; spawnpos.y += 0.3f;
    }

    public GameObject Fireball;

    // Update is called once per frame
    void Update()
    {
        if (timecnt > Shooting_Start_Offset) shotstartcmd = 1;
        timecnt += Time.deltaTime;
        System.Random random = new System.Random();
        randnum = (int)random.Next(0, 100);

        if (randnum <= Shooting_Chance_Per_Frame && shotrecent == 0 && shotstartcmd == 1){
            Instantiate(Fireball, spawnpos, Quaternion.Euler(0f, 0f, 0f));
            shotrecent = 1;
        }
        //연속 대포 발사 제한
        if (timecnt > minimumShootingGapSec && shotrecent == 1)
        {
            shotrecent = 0;
            timecnt = 0;
        }
    }
}
