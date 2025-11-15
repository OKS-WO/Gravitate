using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    public Vector2 dir;
    public float power;

    public GameObject gear1;
    public GameObject gear2;
    public float gearDir;
    public float gearSpeed = 30f;

	private void FixedUpdate()
	{
        gear1.transform.Rotate(Vector3.forward * gearDir * gearSpeed * Time.deltaTime);
        gear2.transform.Rotate(Vector3.forward * gearDir * gearSpeed * Time.deltaTime);
    }

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
            gearSpeed = 0f;
        }
    }
}
