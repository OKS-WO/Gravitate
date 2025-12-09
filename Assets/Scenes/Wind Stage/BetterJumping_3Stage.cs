using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping_3Stage : MonoBehaviour
{
    Rigidbody2D rigid;
    public float highJumpMultiplier = 1.3f;
    public float lowJumpMultiplier = 3.5f;
    public float fallMultiplier = 3.0f;

    Movement_3Stage move;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        move = GetComponent<Movement_3Stage>();
    }


    void FixedUpdate()
    {
        if (!move.isDashing&&!move.isClimbing)
        {
            if (rigid.velocity.y < 0)
            {
                rigid.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
            }
            else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rigid.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
            }
            else if (rigid.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            {
                rigid.velocity += Vector2.up * Physics2D.gravity.y * highJumpMultiplier * Time.fixedDeltaTime;
            }
        }
    }
}
