using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform_3Stage : MonoBehaviour
{
    public float jumpForce = 50f;
    public Vector2 direction = Vector2.up;

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Rock"))
		{
			Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
			rigid.AddForce(direction * jumpForce, ForceMode2D.Impulse);
			if (collision.gameObject.CompareTag("Player"))
			{
				collision.gameObject.GetComponent<Movement>().playSound("JUMPPLATFORM");
			}
		}
	}
}
