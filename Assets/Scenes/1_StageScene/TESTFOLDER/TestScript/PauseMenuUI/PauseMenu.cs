using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject PauseMenuUI;

    private void Start()
    {
        Continue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        gameIsPause = false;
        PauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPause = true;
        PauseMenuUI.SetActive(true);
    }

    public void Restart()
    {
        Continue();
        restartManager.setRespawn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckPoint()
    {
        Continue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Continue();
        restartManager.setRespawn = false;
        SceneManager.LoadScene("TitleScene");
    }
}
