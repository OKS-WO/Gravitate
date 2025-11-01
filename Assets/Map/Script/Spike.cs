using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject destroyParticle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("UnBreakable") && !collision.gameObject.CompareTag("Player"))
        {
            Vector2 pos = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            Instantiate(destroyParticle, pos, Quaternion.identity, null);
            Destroy(collision.gameObject);

        }
    }
}
