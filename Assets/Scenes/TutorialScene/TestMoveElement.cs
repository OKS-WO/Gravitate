using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMoveElement : MonoBehaviour
{
    public GameObject element;
	public string targetStage;
	public float nextStageTime = 0.5f;
	bool isStageEnd = false;
	Rigidbody2D rigid;
	private void Awake()
	{
		if(element!=null)
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
			int n = SceneManager.GetActiveScene().buildIndex;
			if (n > restartManager.visitedStageNum)
				restartManager.visitedStageNum = n;
			Debug.Log(n);
		}

		Invoke("moveStage", nextStageTime);
		
	}

	void moveStage()
	{
		restartManager.setRespawn = false;
		SceneManager.LoadScene(targetStage);
	}
}
