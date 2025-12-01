using UnityEngine;

public class MovingCloudTarget2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;
    public float moveRange = 3.0f;
    public bool isVertical = true; 

    // --- ▼ [NEW] 움직임 활성화 변수 ▼ ---
    [Tooltip("체크하면 게임 시작 시 바로 움직임, 해제하면 멈춰있음")]
    public bool isMoving = false; // 화로에 불을 붙이기 전에는 false여야 합니다.
    // --- ▲ ---

    [Header("Transformation")]
    [Tooltip("물에 맞으면 변신할 얼음 구름 프리팹 (고정형)")]
    public GameObject iceCloudPrefab;

    private Vector3 startPosition;
    private float startTime; // 움직이기 시작한 시간

    void Start()
    {
        startPosition = transform.position;
        // 이미 움직이도록 설정되어 있다면 시작 시간을 현재로 설정
        if (isMoving) startTime = Time.time;
    }

    void Update()
    {
        if (isMoving)
        {
            float timeElapsed = Time.time - startTime;
            
            float movement = (Mathf.Sin(timeElapsed * moveSpeed - Mathf.PI / 2) + 1f) * 0.5f * moveRange;

            if (isVertical)
                transform.position = startPosition + new Vector3(0, movement, 0);
            else
                transform.position = startPosition + new Vector3(movement, 0, 0);
        }
    }

    // --- ▼ [핵심 수정] 이 함수가 없어서 오류가 났습니다. 꼭 추가해주세요! ▼ ---
    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            startTime = Time.time; // 현재 시간부터 움직임 시작
        }
    }
    // --- ▲ ---

    // --- 플레이어 탑승 처리 (같이 움직이기) ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    // --- 물 파티클 충돌 감지 (얼음 구름 변신) ---
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            FreezeAndTransform();
        }
    }

    private void FreezeAndTransform()
    {
        if (iceCloudPrefab != null)
        {
            Instantiate(iceCloudPrefab, transform.position, Quaternion.identity);
        }
        
        // 플레이어가 타고 있을 수 있으므로 부모 관계 해제 후 파괴
        transform.DetachChildren(); 
        Destroy(gameObject);
    }
}