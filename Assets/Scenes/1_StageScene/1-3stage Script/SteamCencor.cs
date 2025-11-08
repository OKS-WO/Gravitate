using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCencor : MonoBehaviour
{
    public GameObject bridge;

    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 currentPos;
    public Vector2 desirePos;
    public float desireTime = 0.05f;

    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = bridge.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = Vector2.Lerp(rigid.position, desirePos, desireTime);
        rigid.MovePosition(newPos);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Steam"))
        {
            currentPos = rigid.position;
            desirePos = endPos;
        }
    }
}
