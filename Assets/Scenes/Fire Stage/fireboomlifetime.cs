using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireboomlifetime : MonoBehaviour
{
    public float lifetimelimits =2.0f;
    private float timecnt = 0;
    // Update is called once per frame
    void Update()
    {
        timecnt += Time.deltaTime;
        if (timecnt > lifetimelimits){
            Destroy(gameObject);
        }
    }
}
