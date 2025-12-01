using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject continueMenu;
    public void playGame()
	{
		SceneManager.LoadScene("Tutorial Stage");
	}

	public void continueGame()
	{
		if (continueMenu != null)
		{
			Debug.Log("continueMenu");
			continueMenu.SetActive(true);
			mainMenu.SetActive(false);
		}
	}

	public void quitGame()
	{
		Application.Quit();
	}
}
