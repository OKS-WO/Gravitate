using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingFireballScript : MonoBehaviour
{
    Rigidbody2D rigid2d;

    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    public float rtspeed = 0;
    public float direction = 1f;
    public float rtradius = 3;
    public float rtoffset = 0;
    public GameObject centerobj;

    private Vector3 centerpos;
    private void Start()
    {
        centerpos = centerobj.transform.position;
    }

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rtspeed * direction / Mathf.PI);
        rigid2d.velocity = new Vector3(1, 0, 0);

        timer += Time.deltaTime;
        float mx, my;
        mx = centerpos.x + rtradius * Mathf.Cos(direction * rtspeed * timer + rtoffset * Mathf.PI);
        my = centerpos.y + rtradius * Mathf.Sin(direction * rtspeed * timer + rtoffset * Mathf.PI);


        transform.position = (new Vector3(mx, my, 0));
    }

           
}
