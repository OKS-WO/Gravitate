using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatformAnim : MonoBehaviour
{
	Animator anim;
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (anim != null)
		{
			anim.SetBool("isActive", true);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (anim != null)
		{
			anim.SetBool("isActive", false);
		}
	}
}
