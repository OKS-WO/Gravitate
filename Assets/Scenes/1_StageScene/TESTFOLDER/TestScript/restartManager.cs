using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartManager : MonoBehaviour
{
    public static Vector3 pos = Vector3.zero;

    public static void toCheckPoint()
    {
        if (pos != Vector3.zero)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            pos = new Vector3(pos.x, pos.y, 0f);
            player.transform.position = pos;
        }
    }
}
