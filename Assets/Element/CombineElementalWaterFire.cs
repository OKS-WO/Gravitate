using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineElementalWaterFire : MonoBehaviour
{
    public GameObject steamPrefab;

	private void OnParticleCollision(GameObject other)
	{

		if (other.CompareTag("Water"))
		{
			Vector3 pos = gameObject.transform.position;
			Instantiate(steamPrefab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
		}
	}
}
