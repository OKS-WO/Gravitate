using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setUpCollider : MonoBehaviour
{
    public GameObject obj;

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (obj != null)
		{
			obj.tag = "Rope";
			obj.layer = 9;
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (obj != null)
		{
			obj.tag = "Defalut";
			obj.layer = 0;
		}
	}
}
