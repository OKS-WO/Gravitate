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
        for(int i = 0; i < stage.Length; i++)
        {
            if (i < n) stage[i].SetActive(true);
            else stage[i].SetActive(false);
        }
        Debug.Log(n);
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

    public void continue3thStage(int num)
    {
        restartManager.setRespawn = true;
        switch (num)
        {
            case 1:
                restartManager.respawnPoint = new Vector2(0, 0);
                break;

            case 2:
                restartManager.respawnPoint = new Vector2(0, 0);
                break;

            case 3:
                restartManager.respawnPoint = new Vector2(0, 0);
                break;

            default:break;
        }
    }
}
