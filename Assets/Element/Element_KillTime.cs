using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public float killTime = 10.0f;
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, killTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
