using System.Collections;
using UnityEngine;

public class BoomerangObstacle : MonoBehaviour
{
    // 발사 타입 정의
    public enum FireType { TargetPlayer, Horizontal, Vertical }

    [Header("Settings")]
    [Tooltip("발사 방식 선택")]
    public FireType fireType = FireType.TargetPlayer;

    [Tooltip("수평/수직 발사 시 방향 (1 = 우/상, -1 = 좌/하)")]
    public float fixedDirection = 1f; 

    [Tooltip("부메랑이 날아가는 속도")]
    public float speed = 10f;

    [Tooltip("부메랑이 도달하려는 최대 거리")]
    public float maxDistance = 15f;

    [Tooltip("되돌아오기 전에 잠시 머무는 시간 (초)")]
    public float waitTime = 0.5f;

    [Tooltip("부메랑의 회전 속도")]
    public float rotationSpeed = 360f;

    private Vector3 startPosition;
    private Transform player;

    void Start()
    {
        startPosition = transform.position;
        
        // 플레이어 찾기
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            StartCoroutine(BoomerangRoutine());
        }
    }

    void Update()
    {
        // 시각적 회전
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator BoomerangRoutine()
    {
        Vector3 targetPosition = Vector3.zero;
        Vector3 direction = Vector3.zero;

        // --- [핵심 수정] 타입에 따른 방향 계산 ---
        switch (fireType)
        {
            case FireType.TargetPlayer:
                // 플레이어 방향으로 발사
                if (player != null)
                    direction = (player.position - startPosition).normalized;
                else
                    direction = Vector3.right; // 플레이어 없으면 오른쪽으로
                break;

            case FireType.Horizontal:
                // 수평 방향 (1: 오른쪽, -1: 왼쪽)
                direction = new Vector3(fixedDirection, 0, 0);
                break;

            case FireType.Vertical:
                // 수직 방향 (1: 위쪽, -1: 아래쪽)
                direction = new Vector3(0, fixedDirection, 0);
                break;
        }
        // ----------------------------------------

        targetPosition = startPosition + direction * maxDistance;

        // 1. 발사 (목표 지점을 향해 이동)
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // 2. 대기
        yield return new WaitForSeconds(waitTime);

        // 3. 복귀 (시작 위치로 이동)
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        // 4. 파괴
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 충돌 처리 (데미지 등)
             // other.GetComponent<PlayerRespawn>().Die(); // 예시
            Debug.Log("플레이어 피격!");
        }
        else if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}