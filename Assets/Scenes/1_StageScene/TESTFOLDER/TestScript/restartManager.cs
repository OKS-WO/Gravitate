using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartManager : MonoBehaviour
{
    public static Vector3 respawnPoint;
    public static bool setRespawn = false;

    public static restartManager Instance;

    public static CursorMode cursormode;
    public enum ReMode
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


}
