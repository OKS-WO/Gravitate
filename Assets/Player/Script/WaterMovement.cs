using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public GameObject WaterPrefab;
    public Vector3 spawnOffset = new Vector3(0f, -1f, 0);

    public bool isSpawner = true;
    public float initialDelay = 1f;
    public float repeatRate = 1f;

    // ?? 2. 생성 상태 추적 변수 (중복 중단 방지)
    private bool isGenerating = true;
    public bool isCollision = false;

    void Start()
    {
        // ?? 3. isSpawner가 true일 때만 반복 호출을 시작합니다.
        if (isSpawner)
        {
            InvokeRepeating("createWater", initialDelay, repeatRate);
        }
    }

	void createWater()
    {
        if (isGenerating)
        {
            Vector3 finalPosition = transform.position;

            GameObject newWater = Instantiate(WaterPrefab, finalPosition, Quaternion.identity);

            WaterMovement waterMovement = newWater.GetComponent<WaterMovement>();
            if (waterMovement != null)
            {
                waterMovement.isSpawner = false;
            }

			if (isCollision)
			{
                stopCreateWater();
			}
            transform.position += spawnOffset;
        }
    }

    void stopCreateWater()
    {

        CancelInvoke("createWater");
        isGenerating = false;
        Debug.Log("플레이어를 제외한 오브젝트와 닿아 물 생성이 중지되었습니다.");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player") && !collision.CompareTag("Water"))
        {
            isCollision = true;
        }
    }
}