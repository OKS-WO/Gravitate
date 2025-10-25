using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineElement : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		
		if (gameObject.CompareTag("Rock"))
		{
			Debug.Log(other.tag);
			if (other.CompareTag("Fire"))
			{
				gameObject.tag = "UnBreakable";
				gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
			}
			else if (other.CompareTag("Water"))
			{
				gameObject.tag = "Mud";
			}
		}
	}
}
