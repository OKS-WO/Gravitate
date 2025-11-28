using UnityEngine;

public class MovingCloudTarget : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("이동 속도")]
    public float moveSpeed = 2.0f;

    [Tooltip("이동 거리 (한쪽 방향으로의 최대 거리)")]
    public float moveRange = 3.0f;

    [Tooltip("체크하면 상하 이동, 해제하면 좌우 이동")]
    public bool isVertical = false;

    [Header("Transformation")]
    [Tooltip("물에 맞았을 때 생성될 얼음 구름 프리팹 (고정형)")]
    public GameObject iceCloudPrefab;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // 시작 위치 기억
    }

    void Update()
    {
        // 1. 주기적인 이동 (PingPong과 Sin 함수 활용)
        float movement = Mathf.Sin(Time.time * moveSpeed) * moveRange;

        if (isVertical)
        {
            // 상하 이동
            transform.position = startPosition + new Vector3(0, movement, 0);
        }
        else
        {
            // 좌우 이동
            transform.position = startPosition + new Vector3(movement, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 구름 위에 닿으면
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 구름의 자식으로 설정 (구름 따라 움직임)
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 플레이어가 구름에서 떨어지거나 점프하면
        if (collision.gameObject.CompareTag("Player"))
        {
            // 자식 관계 해제 (원래대로 독립)
            collision.transform.SetParent(null);
        }
    }

    // 2. 물 파티클 충돌 감지
    private void OnParticleCollision(GameObject other)
    {
        // 물 능력(Water)에 맞았는지 확인
        if (other.CompareTag("Water"))
        {
            FreezeAndTransform();
        }
    }

    // 3. 고정 및 변신 함수
    private void FreezeAndTransform()
    {
        if (iceCloudPrefab != null)
        {
            // (A) 현재 위치(움직이다 멈춘 그 위치)에 얼음 구름 생성
            Instantiate(iceCloudPrefab, transform.position, Quaternion.identity);
            
            // (선택) 얼음 생성 효과음이나 파티클 추가
            // Instantiate(freezeEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Ice Cloud Prefab이 연결되지 않았습니다!");
        }

        // (B) 움직이던 일반 구름(자신)은 파괴
        Destroy(gameObject);
    }
}