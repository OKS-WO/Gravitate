using UnityEngine;

public class BoomerangSpawner : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public float spawnInterval = 3.0f; // 3초마다 발사

    [Header("Spawn Settings")]
    [Tooltip("이 스포너에서 발사할 방식 선택")]
    // BoomerangObstacle 스크립트에 정의된 Enum을 가져와서 인스펙터에 표시합니다.
    public BoomerangObstacle.FireType fireType = BoomerangObstacle.FireType.TargetPlayer;

    [Tooltip("수평/수직 발사 시 방향 (1 = 우/상, -1 = 좌/하)")]
    public float fixedDirection = 1f;

    void Start()
    {
        InvokeRepeating("SpawnBoomerang", 0f, spawnInterval);
    }

    void SpawnBoomerang()
    {
        if (boomerangPrefab == null) return;

        // 1. 부메랑 생성
        GameObject obj = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
        
        // 2. 생성된 부메랑의 스크립트를 가져옴
        BoomerangObstacle boomerang = obj.GetComponent<BoomerangObstacle>();
        
        // 3. 스포너의 설정을 부메랑에게 전달 (덮어쓰기)
        if (boomerang != null)
        {
            boomerang.fireType = this.fireType;
            boomerang.fixedDirection = this.fixedDirection;
        }
    }
}