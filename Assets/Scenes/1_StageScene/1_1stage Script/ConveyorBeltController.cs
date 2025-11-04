using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    public Vector2 dir;
    public float power;


    // Start is called before the first frame update
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(power!=0)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Mud"))
        {
            Debug.Log(other.tag);
            this.enabled = false;
            power = 0f;
        }
    }
}
