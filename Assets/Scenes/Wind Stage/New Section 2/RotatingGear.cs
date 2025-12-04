using UnityEngine;

public class RotatingGear : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("회전 속도 (양수: 반시계, 음수: 시계)")]
    public float rotationSpeed = 100f;

    [Tooltip("플레이어를 밀어내는 힘")]
    public float pushForce = 10f; 

    private bool isStopped = false;

    void Update()
    {
        if (!isStopped)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 1. 톱니바퀴가 멈췄으면 힘을 주지 않음
        if (isStopped) return;

        // 2. 플레이어와 충돌 중일 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // A. 톱니바퀴 중심에서 플레이어 방향 벡터 계산
                Vector2 direction = (collision.transform.position - transform.position).normalized;

                // B. 회전 방향에 따른 접선(Tangent) 벡터 계산
                // (반시계 회전이면 왼쪽(-y, x), 시계 회전이면 오른쪽(y, -x) 방향으로 밈)
                Vector2 forceDirection;
                if (rotationSpeed > 0) // 반시계
                {
                    forceDirection = new Vector2(-direction.y, direction.x);
                }
                else // 시계
                {
                    forceDirection = new Vector2(direction.y, -direction.x);
                }

                // C. 힘 가하기 (ForceMode2D.Force를 사용하여 지속적인 힘 전달)
                playerRb.AddForce(forceDirection * pushForce, ForceMode2D.Force);
            }
        }
        // 3. 진흙 블록과 충돌 시 정지
        else if (collision.gameObject.CompareTag("Mud"))
        {
            StopGear();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Mud"))
        {
            StopGear();
        }
    }

    private void StopGear()
    {
        if (isStopped) return;

        isStopped = true;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(0.6f, 0.4f, 0.2f);
        }

        gameObject.tag = "UnBreakable";
        
        Debug.Log("톱니바퀴가 진흙에 의해 멈췄습니다!");
    }
}