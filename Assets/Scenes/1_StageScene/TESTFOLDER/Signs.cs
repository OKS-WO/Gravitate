using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour
{
    public GameObject signs;
	public GameObject canvas;
	public bool isColl = false;

	private void Update()
	{
		if (isColl && Input.GetKey(KeyCode.X))
		{
			signs.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5f);
			canvas.SetActive(true);
		}
		else
		{
			signs.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 30f);
			canvas.SetActive(false);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isColl = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isColl = false;
		}
	}
}
