using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject continueMenu;

    private void Awake()
    {
		loadGame();
    }
    public void playGame()
	{
		SceneManager.LoadScene("Tutorial Stage");
	}

	public void continueGame()
	{
		if (continueMenu != null)
		{
			continueMenu.SetActive(true);
			mainMenu.SetActive(false);
		}
	}

	public void quitGame()
	{
		saveGame();
		Application.Quit();
	}

	public void loadGame()
	{
        mainMenu.SetActive(true);
        continueMenu.SetActive(false);
        if (PlayerPrefs.HasKey("visitedStageNum"))
		{
            restartManager.visitedStageNum = PlayerPrefs.GetInt("visitedStageNum");
            Debug.Log("Load visited stage number : " + restartManager.visitedStageNum);
        }
	}

	public void saveGame()
	{
		Debug.Log("save");
        PlayerPrefs.SetInt("visitedStageNum", restartManager.visitedStageNum);
    }

	public void deleteData()
	{
		Debug.Log("delete all data");
		restartManager.visitedStageNum = 0;
		PlayerPrefs.DeleteAll();
	}
}
