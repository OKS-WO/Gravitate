using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newButtonTrigger : MonoBehaviour
{
    public LayerMask buttonTriggerLayer;
    public Vector2 bottomOffset = new Vector2(0f, 0f);
    public Vector2 groundBoxSize = new Vector2(1f, 1f); // 박스의 가로, 세로 크기

    public GameObject bridge;

    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 currentPos;
    public Vector2 desirePos;
    public float desireTime = 0.1f;
    public bool loop = true;
    public bool isActive = false;

    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = bridge.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	private void Update()
	{
        isActive = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundBoxSize, 0f, buttonTriggerLayer);

        if (isActive)
        {
            desirePos = endPos;
            anim.SetBool("isPress", true);
        }
        else
        {
            if (loop)
            {
                desirePos = startPos;
                anim.SetBool("isPress", false);
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        Vector2 newPos = Vector2.Lerp(rigid.position, desirePos, desireTime);
        rigid.MovePosition(newPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, groundBoxSize);
    }
}
