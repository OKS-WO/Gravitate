using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce : MonoBehaviour
{
    public float windStrength = 5f;          

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.tag);
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var externalForces = ps.externalForces;
            externalForces.enabled = true;
            externalForces.multiplier = windStrength;
            externalForces.multiplier = windStrength;
            Debug.Log("파티클 시스템에 바람 효과 적용됨: " + other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps != null)
        {

            var externalForces = ps.externalForces;
            externalForces.enabled = false;
        }
    }
}
