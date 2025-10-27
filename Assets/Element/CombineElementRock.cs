using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineElement : MonoBehaviour
{
	public GameObject mudPrefab;
	private void OnParticleCollision(GameObject other)
	{
		
		if (gameObject.CompareTag("Rock"))
		{
			if (other.CompareTag("Fire"))
			{
				gameObject.tag = "UnBreakable";
				gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
			}
			else if (other.CompareTag("Water") && mudPrefab != null)
			{
				gameObject.tag = "Mud";
				Vector3 pos = gameObject.transform.position;
				pos.y += 1f;
				Instantiate(mudPrefab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
				Destroy(gameObject);
            }
		}
	}
}
