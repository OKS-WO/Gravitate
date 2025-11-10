using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject fallingRockPrefab;

    public float spawnInterval = 1.5f;

    public float spawnAreaWidth = 10f;

    public int spawnCount = 5;

    void OnEnable()
    {
        // 낙석 생성 코루틴을 시작합니다.
        StartCoroutine(SpawnRocks());
    }

    IEnumerator SpawnRocks()
    {
        // 이 오브젝트가 활성화되어 있는 동안 무한 반복
        while (true) 
        {
            // 이번 스폰 주기에 몇 개의 낙석을 떨어뜨릴지 랜덤으로 결정 
            //int rockCount = Random.Range(1, 3); 
            int rockCount = spawnCount;

            // 결정된 개수(rockCount)만큼 반복
            for (int i = 0; i < rockCount; i++)
            {
                // 3. 각 낙석의 랜덤한 X 위치를 계산
                float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
                Vector3 spawnPosition = transform.position + new Vector3(randomX, 0, 0);

                // 4. 낙석 프리팹을 해당 위치에 생성
                Instantiate(fallingRockPrefab, spawnPosition, Quaternion.identity);

                //  미세한 시간차
                yield return new WaitForSeconds(0.2f); 
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}