using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    public Camera cam;
    public Vector3 pos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam.transform.position = pos;
        }
    }
}
