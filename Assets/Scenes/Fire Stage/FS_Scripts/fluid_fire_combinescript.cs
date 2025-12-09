using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluid_fire_combinescript : MonoBehaviour
{
    public GameObject steamparticle;

    private Vector2 pos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pos = gameObject.transform.position;
        if (collision.gameObject.CompareTag("Fire"))
        {
            Instantiate(steamparticle, pos, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}
