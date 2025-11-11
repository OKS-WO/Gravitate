using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMoveElement : MonoBehaviour
{
    public GameObject element;
	public string targetStage;
	public float nextStageTime = 3.0f;
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
			if(element!=null)
				rigid.velocity = Vector2.up * 1f;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isStageEnd = true;
		}

		Invoke("moveStage", nextStageTime);
		
	}

	void moveStage()
	{
		SceneManager.LoadScene(targetStage);
	}
}
