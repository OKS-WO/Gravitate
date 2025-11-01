using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMoveElement : MonoBehaviour
{
    public GameObject element;
	bool isStageEnd = false;
	Rigidbody2D rigid;
	private void Awake()
	{
		rigid = element.GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		if (isStageEnd)
		{
			rigid.velocity = Vector2.up * 1f;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isStageEnd = true;
		}

		Invoke("moveto1_1stage", 3f);
		
	}

	void moveto1_1stage()
	{
		SceneManager.LoadScene("1_1stage");
	}
}
