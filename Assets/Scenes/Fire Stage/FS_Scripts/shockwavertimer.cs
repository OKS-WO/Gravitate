using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwavertimer : MonoBehaviour
{
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f) Destroy(gameObject);
    }
}
