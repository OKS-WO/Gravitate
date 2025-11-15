using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lebberActive : MonoBehaviour
{
    public GameObject bridge;
    public Vector2 startPos;
    public Vector2 endPos;
    public float desireTime;

    public float startAngle = 45f;
    public float endAngle = -45f;

    public bool isColl = false;
    public bool isActive = true;

    public GameObject collider;

    Rigidbody2D rigid;

    void Start()
    {
        startPos = bridge.gameObject.transform.position;
        rigid = bridge.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isColl)
        {
            if (isActive)
            {
                gameObject.transform.Rotate(new Vector3(0f, 0f, startAngle));
                isActive = false;
                if (collider != null)
                {
                    collider.tag = "Default";
                    collider.layer = 0;
                }
            }
            else
            {
                gameObject.transform.Rotate(new Vector3(0f, 0f, endAngle));
                isActive = true;
                if (collider != null)
                {
                    collider.tag = "Rope";
                    collider.layer = 9;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 newPos = Vector2.Lerp(rigid.position, endPos, desireTime);
            rigid.MovePosition(newPos);
        }
        else
        {
            Vector2 newPos = Vector2.Lerp(rigid.position, startPos, desireTime);
            rigid.MovePosition(newPos);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColl = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isColl = false;
        }
    }
}
