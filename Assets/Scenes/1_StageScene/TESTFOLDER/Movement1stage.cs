using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1stage : MonoBehaviour
{
    Rigidbody2D rigid;
    Collision coll;
    SpriteRenderer spriteRenderer;
    ElementController_3Stage element;

    HingeJoint2D hinJoint;
    Rigidbody2D nearRopeRigid;


    public float speed = 10.0f;
    public float jumpSpeed = 10.0f;
    public float dashSpeed = 20.0f;

    public bool isDashing = false;
    public bool isClimbing = false;
    public bool isWallJumping = false;
    public bool isSwing = false;
    public int numberOfGravityCore = 0;

    public AudioClip jump_clip;
    public AudioClip die_clip;
    public AudioClip walk_clip;
    public AudioClip jumpplatform_clip;
    public AudioClip elementalSet_clip;

    AudioSource audioSource;

    private float xInput = 0f;
    private float yInput = 0f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hinJoint = GetComponent<HingeJoint2D>();
        element = GetComponent<ElementController_3Stage>();

        // AudioSource 컴포넌트 찾기
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!coll.isRope)
                jump();
            else if (coll.isRope == true && !coll.isGround)
            {
                StartCoroutine(wallJump(xInput));
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && coll.dashReady && !coll.isGround)
        {
        }

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.C) && coll.isRope && !isWallJumping)
        {
            if (hinJoint.connectedBody == null)
            {
                hinJoint.enabled = true;
                hinJoint.connectedBody = nearRopeRigid;
            }
        }
        else
        {
            hinJoint.connectedBody = null;
            hinJoint.enabled = false;
        }

        if (coll.isAir && coll.dashReady)
        {
        }
    }

    private void FixedUpdate()
    {
        if (isDashing == false)
        {
            move();
        }
        if (coll.isRope)
        {
            isClimbing = true;
            climbing();
        }
        else isClimbing = false;
    }

    void move()
    {
        if (coll.isGround && !element.isSetting)
        {
            Vector2 desireVector = new Vector2(xInput * speed, rigid.velocity.y);
            rigid.velocity = Vector2.Lerp(rigid.velocity, desireVector, 10.0f * Time.deltaTime);
        }
        else if (Mathf.Abs(rigid.velocity.x) < speed + 5f && !element.isSetting)
        {
            Vector2 desireVector = new Vector2(xInput * speed, rigid.velocity.y);
            rigid.velocity = Vector2.Lerp(rigid.velocity, desireVector, 10.0f * Time.deltaTime);
        }
        if (element.isSetting)
        {
            Vector2 desireVector = new Vector2(0, 0);
            rigid.velocity = Vector2.Lerp(rigid.velocity, desireVector, 10.0f * Time.deltaTime);
        }
        if (xInput == 1)
        {
            spriteRenderer.flipX = false;
        }
        if (xInput == -1)
        {
            spriteRenderer.flipX = true;
        }
    }

    void jump()
    {
        if (coll.isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
            playSound("JUMP");
        }
        else if (coll.isLeftWall || coll.isRightWall || (coll.isRope && hinJoint.connectedBody != null))
        {
            hinJoint.connectedBody = null;
            int dir = coll.isLeftWall ? 1 : -1;
            playSound("JUMP");
            StartCoroutine(wallJump(dir));
        }
    }

    IEnumerator wallJump(float dir)
    {
        isWallJumping = true;
        rigid.velocity = new Vector2(dir * speed * 0.8f, jumpSpeed);
        playSound("JUMP");
        yield return new WaitForSeconds(0.5f);
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

    // playSound 함수 추가
    public void playSound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = jump_clip;
                break;
            case "DIE":
                audioSource.clip = die_clip;
                break;
            case "WALK":
                audioSource.clip = walk_clip;
                break;
            case "JUMPPLATFORM":
                audioSource.clip = jumpplatform_clip;
                break;
            case "SET":
                audioSource.clip = elementalSet_clip;
                break;
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope"))
        {
            nearRopeRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
