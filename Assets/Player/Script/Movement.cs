using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rigid;
    Collision coll;

    public float speed = 10.0f;
    public float jumpSpeed = 10.0f;
    public float dashSpeed = 20.0f;

    public bool isDashing = false;
    public bool isClimbing = false;
    public bool isWallJumping = false;
    public int numberOfGravityCore = 0;
    // Start is called before the first frame update

    private float xInput = 0f;
    private float yInput = 0f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.X) && coll.dashReady && !coll.isGround)
        {
            StartCoroutine(dash());
        }

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (isDashing == false)
        {
            move();
        }
        if (!coll.isGround && (coll.isLeftWall || coll.isRightWall))
        {
            isClimbing = true;
            climbing();
        }
        else isClimbing = false;
    }

    void move()
    {
        Vector2 curVector = new Vector2(xInput * speed, rigid.velocity.y);

        rigid.velocity = Vector2.Lerp(rigid.velocity, curVector, 10.0f * Time.deltaTime);
    }

    void jump()
    {
        if (coll.isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
        }
        else if (coll.isLeftWall || coll.isRightWall)
        {
            int dir = coll.isLeftWall ? 1 : -1;
            StartCoroutine(wallJump(dir));
        }
    }

    IEnumerator wallJump(int dir)
    {
        isWallJumping = true;
        rigid.velocity = new Vector2(dir * speed*2, jumpSpeed);
        yield return new WaitForSeconds(0.15f);
        isWallJumping = false;
    }

    IEnumerator dash()
    {
        float dashRate = 1.0f;
        coll.dashReady = false;
        isDashing = true;
        rigid.gravityScale = 0f;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x * y != 0) dashRate = 0.85f;
        if (x == 0 && y == 0) y = 1f;
        
        rigid.velocity = new Vector2(x * dashSpeed * dashRate, y * dashSpeed * dashRate);
        yield return new WaitForSeconds(0.1f);
        while (rigid.velocity.magnitude > 0.3f)
        {
            rigid.velocity = Vector2.Lerp(rigid.velocity, Vector2.zero, 10f * Time.deltaTime);
            yield return null; 
        }
        isDashing = false;
        rigid.velocity = Vector2.zero;
        isDashing = false;
        rigid.gravityScale = 1.0f;
    }

    void climbing()
    {
        if (!isWallJumping)
        {
            if (yInput == 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }
            else
            {
                rigid.velocity = new Vector2(rigid.velocity.x, yInput * speed * 0.5f);
            }
        }
    }
}
