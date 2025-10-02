using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : MonoBehaviour
{
    Movement player;
    public Transform playerTransform;
    public float rotationSpeed = 180f;
    private bool isRotating = false;

    private void Awake()
    {
        player = playerTransform.GetComponent<Movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating && player.numberOfGravityCore >= 0)
        {
            StartCoroutine(RotateMap(90f));
            //player.numberOfGravityCore--;
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating && player.numberOfGravityCore >= 0)
        {
            StartCoroutine(RotateMap(-90f));
            //player.numberOfGravityCore--;
        }
    }

    IEnumerator RotateMap(float angle)
    {
        isRotating = true;
        float startAngle = transform.eulerAngles.z;
        float endAngle = startAngle + angle;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed / 90f; // 90도 회전 시 걸리는 시간 계산
            float currentAngle = Mathf.Lerp(startAngle, endAngle, t);

            // 플레이어 위치를 중심으로 Z축 기준으로 회전
            transform.RotateAround(playerTransform.position, Vector3.forward, currentAngle - transform.eulerAngles.z);

            yield return null;
        }
        isRotating = false;
        transform.eulerAngles = new Vector3(0, 0, endAngle); // 최종 각도를 정확하게 보정
    }
}