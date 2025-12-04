using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FS_checkpointeffect : MonoBehaviour
{
    public GameObject Checkpointparticle;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 Pos = transform.position;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("checkpoint activated");
            Instantiate(Checkpointparticle, Pos, Quaternion.identity);
        }
    }
}
