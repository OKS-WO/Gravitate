using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    public Camera mainCam;
    public Camera subCam;

	private void Awake()
	{
		mainCam.enabled = true;
		subCam.enabled = false;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			mainCam.enabled = false;
			subCam.enabled = true;
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			mainCam.enabled = true;
			subCam.enabled = false;
		}
	}

}
