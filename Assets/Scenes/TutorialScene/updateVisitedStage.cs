using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateVisitedStage : MonoBehaviour
{
    public int stageNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int n = restartManager.visitedStageNum;
            restartManager.visitedStageNum = stageNum > n ? stageNum : n;
        }
        Debug.Log(restartManager.visitedStageNum);
    }
}
