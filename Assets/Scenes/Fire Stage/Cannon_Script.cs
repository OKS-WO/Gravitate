using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon_Script : MonoBehaviour
{
    private int randnum = 0;
    private int timelapse = 0;
    private int timemarker = 0;
    private int shotrecent = 0;
    public int Shooting_Chance_Per_Frame;
    // Start is called before the first frame update

    public float spawnX = 0; 
    public float spawnY = 0;

    Vector2 spawnpos = new Vector2(-10, -48);

    void Start() { spawnpos.x = spawnX; spawnpos.y = spawnY; }

    public GameObject Fireball;

    // Update is called once per frame
    void Update()
    {
        timelapse += 1;
        System.Random random = new System.Random();
       
        if (randnum <= Shooting_Chance_Per_Frame && shotrecent == 0){
            Instantiate(Fireball, spawnpos, Quaternion.Euler(0f, 0f, 0f));
            shotrecent = 1;
            timemarker = timelapse;
        }
        //연속 대포 발사 제한
        if (timelapse > timemarker + 480)
        {
            shotrecent = 0;
            randnum = (int)random.Next(0, 100);
        }
    }
}
