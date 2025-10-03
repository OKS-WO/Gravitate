using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseRope : MonoBehaviour
{
    Rigidbody2D rigid;
    public float antiGravityMagnitude = 50.0f;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 antiGravityForce = Vector2.up * antiGravityMagnitude;

        rigid.AddForce(antiGravityForce, ForceMode2D.Force);
    }
}
