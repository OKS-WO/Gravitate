using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueMenu : MonoBehaviour
{
    public GameObject[] stage;
    public GameObject mainMenu;
    public GameObject conMenu;

    private void Update()
    {
        int n = restartManager.visitedStageNum;
        n = n <= stage.Length ? n : stage.Length;
        for (int i = 0; i < n + 1; i++)
        {
            stage[i].SetActive(true);
        }
    }

    public void continueStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

    public void backToTitle()
    {
        mainMenu.SetActive(true);
        conMenu.SetActive(false);
    }
}
