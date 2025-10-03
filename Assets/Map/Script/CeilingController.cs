using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingController : MonoBehaviour
{
    public GameObject[] ceilingFragments;
    public float breakForce = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision) 
    {


        if (!collision.gameObject.CompareTag("Player"))
        {
            BreakCeiling();
        }
    }
    public void BreakCeiling()
    {
        foreach (GameObject fragment in ceilingFragments)
        {

            Rigidbody2D rigid = fragment.GetComponent<Rigidbody2D>();


            rigid.bodyType = RigidbodyType2D.Dynamic;

            rigid.velocity = Vector2.zero;
        }

        GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(this);
    }
}