using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingCloud : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("밟은 후 떨어지기 시작할 때까지의 지연 시간 (초)")]
    public float fallDelay = 0.5f;

    [Tooltip("떨어지기 시작한 후 오브젝트가 파괴될 때까지의 시간 (초)")]
    public float destroyDelay = 3.0f;

    [Tooltip("떨어지는 속도 (중력 계수)")]
    public float gravityScale = 1.0f;

    // --- ▼ [NEW] 변신할 얼음 구름 프리팹 변수 추가 ▼ ---
    [Header("Transformation")]
    [Tooltip("물 파티클에 맞으면 변신할 얼음 구름 프리팹")]
    public GameObject iceCloudPrefab;
    // --- ▲ ---

    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 시작할 때는 공중에 떠 있어야 하므로 Kinematic으로 설정
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    // 1. 플레이어가 밟았을 때 (떨어지는 로직)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f) // 플레이어가 위에서 밟음
                {
                    // (선택사항: 밟는 순간 태그 변경 기능이 필요하다면 여기에 추가)
                    // gameObject.tag = "Seesaw"; 
                    
                    StartCoroutine(Fall());
                    break;
                }
            }
        }
    }

    // --- ▼ [NEW] 2. 물 파티클에 맞았을 때 (변신 로직) ▼ ---
    private void OnParticleCollision(GameObject other)
    {
        // 부딪힌 파티클의 태그가 "Water"인지 확인
        if (other.CompareTag("Water"))
        {
            TransformToIce();
        }
    }

    private void TransformToIce()
    {
        if (iceCloudPrefab != null)
        {
            // 현재 위치와 회전값 그대로 '얼음 구름' 생성
            Instantiate(iceCloudPrefab, transform.position, transform.rotation);
            
            // (선택) 효과음이나 이펙트 생성 가능
        }
        
        // '일반 구름(자기 자신)' 즉시 파괴 (떨어지는 로직 취소됨)
        Destroy(gameObject);
    }
    // --- ▲ ---

    IEnumerator Fall()
    {
        isFalling = true;
        
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = gravityScale;
        
        Destroy(gameObject, destroyDelay);
    }
}