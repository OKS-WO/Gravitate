using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveScript : MonoBehaviour
{
    public GameObject soudplay;
    lebberActive lebber;

	private void Awake()
	{
		lebber = GetComponent<lebberActive>();
	}

	private void Update()
	{
		soudplay.SetActive(lebber.isActive);
	}
}
