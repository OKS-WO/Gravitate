using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setPlayerPosition : MonoBehaviour
{
    private void Start()
    {
            StartCoroutine(MoveToPositionAfterDelay());
    }

    IEnumerator MoveToPositionAfterDelay()
    {
        yield return null;
        if(restartManager.setRespawn)
            transform.position = restartManager.respawnPoint;
        Debug.Log(restartManager.setRespawn);
    }
}
