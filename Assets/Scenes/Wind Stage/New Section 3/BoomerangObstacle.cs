using System.Collections;
using UnityEngine;

public class BoomerangObstacle : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("부메랑이 날아가는 속도")]
    public float speed = 10f;

    [Tooltip("부메랑이 도달하려는 최대 거리 (플레이어를 지나치게 하려면 길게 설정)")]
    public float maxDistance = 15f;

    [Tooltip("되돌아오기 전에 잠시 머무는 시간 (초)")]
    public float waitTime = 0.5f;

    [Tooltip("부메랑의 회전 속도")]
    public float rotationSpeed = 360f;

    private Vector3 startPosition;
    private Transform player;
    private bool isReturning = false;

    void Start()
    {
        startPosition = transform.position;
        
        // 플레이어 찾기 (Tag가 "Player"인 오브젝트)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // 코루틴 시작
            StartCoroutine(BoomerangRoutine());
        }
    }

    void Update()
    {
        // 시각적 회전 효과
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator BoomerangRoutine()
    {
        // 1. 목표 지점 설정 (플레이어의 현재 위치보다 조금 더 뒤쪽)
        // (플레이어 위치 - 시작 위치) 벡터를 구해서 최대 거리만큼 연장
        Vector3 direction = (player.position - startPosition).normalized;
        Vector3 targetPosition = startPosition + direction * maxDistance;

        // 2. 발사 (목표 지점을 향해 이동)
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // 3. 대기 (잠시 멈춤)
        yield return new WaitForSeconds(waitTime);

        // 4. 복귀 (시작 위치로 이동)
        isReturning = true;
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        // 5. 복귀 완료 후 파괴 (또는 비활성화)
        Destroy(gameObject); 
    }

    // 플레이어 충돌 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 데미지 주기 or 리스폰
            // other.GetComponent<PlayerHealth>().TakeDamage(1);
            Debug.Log("플레이어 피격!");
        }
        // (선택 사항) 흙 블록에 맞으면 부메랑 파괴
        else if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}