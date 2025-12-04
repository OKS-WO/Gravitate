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
            //최종 버전에서 다시 추가 예정, 현재 체크포인트 정상 작동 유무 확인 위해 비워둠
            //if (i < n) stage[i].SetActive(true);
            //else stage[i].SetActive(false);
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

    public void continue3thStage(int num)
    {
        restartManager.setRespawn = true;
        switch (num)
        {
            case 1:
                restartManager.respawnPoint = new Vector2(-144f, -3.5f);
                Debug.Log(restartManager.respawnPoint + " select 3-1 stage");
                break;

            case 2:
                restartManager.respawnPoint = new Vector2(-57f, 75f);
                Debug.Log(restartManager.respawnPoint + " select 3-2 stage");
                break;

            case 3:
                restartManager.respawnPoint = new Vector2(0, 0);
                break;

            default:break;
        }
        SceneManager.LoadScene("wind stage");
    }
}
