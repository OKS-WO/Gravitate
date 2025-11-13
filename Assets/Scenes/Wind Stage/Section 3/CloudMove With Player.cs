using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Kinematic)
        {
            Debug.LogWarning(gameObject.name + "의 Rigidbody 2D가 Kinematic이 아닙니다. 애니메이션이 작동하지 않을 수 있습니다.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f) 
                {
                    collision.transform.SetParent(transform);
                    break; 
                }
            }
        }
    }

    // 플레이어가 발판에서 점프하거나 떨어졌을 때
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 1. 부딪혔던 오브젝트가 "Player" 태그를 가졌는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 2. 플레이어를 '자식' 관계에서 해제합니다.
            collision.transform.SetParent(null);
        }
    }
}