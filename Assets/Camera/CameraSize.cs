using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    Camera cam;

    public float defaultZoom = 8.0f;
    public float zoomOut = 15.0f;
    public float zoomSpeed = 3.0f;
    private float curZoom;
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            curZoom = zoomOut;
        }
        else
        {
            curZoom = defaultZoom;
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, curZoom, zoomSpeed * Time.deltaTime);
    }
}
