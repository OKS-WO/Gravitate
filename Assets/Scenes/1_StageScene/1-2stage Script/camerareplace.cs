using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerareplace : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject subCam;
    public GameObject background;
    public LayerMask playerLayer = 6;

    bool isPlayerIn = false;

    public Vector2 BoxSize = new Vector2(0.9f, 1f); // 박스의 가로, 세로 크기

    // Update is called once per frame
    void Update()
    {
        isPlayerIn = Physics2D.OverlapBox((Vector2)transform.position, BoxSize, 0f, playerLayer);
        mainCam.SetActive(!isPlayerIn);
        subCam.SetActive(isPlayerIn);
        background.SetActive(isPlayerIn);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position, BoxSize);
    }
}
