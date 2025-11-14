using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject player;
    public float time = 0.1f;
    public float z_Axis = 20f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.z = z_Axis;
        pos = Vector3.Lerp(pos, player.transform.position, time * Time.fixedDeltaTime);
        gameObject.transform.position = pos;
    }
}
