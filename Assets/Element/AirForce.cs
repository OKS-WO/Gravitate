using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    public float force = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);

        Vector2 vec = new Vector2(gameObject.transform.position.x - collision.transform.position.x, gameObject.transform.position.y - collision.transform.position.y);
        vec = vec.normalized;

        vec *= -force;

        Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.AddForce(vec);
        }
    }
}
