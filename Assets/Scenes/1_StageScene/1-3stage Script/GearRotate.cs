using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotate : MonoBehaviour
{
	public float speed = 1f;
	public float dir = 1f;

	bool isActive = false;
	// Start is called before the first frame update
	private void FixedUpdate()
	{
		if (isActive)
		{
			gameObject.transform.Rotate(Vector3.forward * speed * dir);
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.CompareTag("Steam"))
		{
			isActive = true;
		}
	}
}
