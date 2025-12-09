using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class FS_checkpointeffect : MonoBehaviour
{
    public GameObject Checkpointparticle;
    private int iscollided = 0;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        iscollided = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 Pos = transform.position;
        if (collision.tag == "Player" && iscollided == 0)
        {
            Debug.Log("checkpoint activated");
            Instantiate(Checkpointparticle, Pos, Quaternion.Euler(0f, 0f, 0f));
            iscollided ++;
        }
    }
}
