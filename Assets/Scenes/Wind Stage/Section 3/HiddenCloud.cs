using UnityEngine;
using UnityEngine.Tilemaps; 

public class HiddenCloudTrap : MonoBehaviour
{
    [Tooltip("함정이 발동될 때 변할 색상 (Alpha값을 100~150 정도로 설정)")]
    public Color transparentColor = new Color(1f, 1f, 1f, 0.5f); // 50% 투명도

    [Tooltip("물 능력으로 변하게 될 '단단한 구름' 프리팹")]

    private Tilemap tilemap;
    private Collider2D physicalCollider;
    private bool isSolid = false;
    private Color originalColor;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (tilemap != null)
        {
            originalColor = tilemap.color; // 시작할 때 원래 색상 저장
        }
        
        physicalCollider = GetComponent<TilemapCollider2D>(); 
    }

    // 플레이어가 밟았을 때 호출
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSolid || !collision.gameObject.CompareTag("Player") || tilemap == null)
        {
            return;
        }

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb == null) return;

        float playerY = collision.transform.position.y;
        float cloudY = transform.position.y; 

        if (playerRb.velocity.y <= 1.0f) 
        {
            // 함정 발동: 투명하게 변경
            tilemap.color = transparentColor;

            // 밟을 수 없도록 물리 콜라이더를 끔
            if (physicalCollider != null)
            {
                physicalCollider.enabled = false;
            }
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (isSolid || !other.CompareTag("Water"))
        {
            return;
        }
        BecomeSolid();
    }

    private void BecomeSolid()
    {
        isSolid = true;

        if (tilemap != null)
        {
            tilemap.color = originalColor; 
        }
        if (physicalCollider != null)
        {
            physicalCollider.enabled = true; 
        }
    }
    
    // 플레이어가 죽어서 리셋될 때 원래대로 돌리는 함수
    public void ResetTrap()
    {
        if (tilemap != null)
        {
            tilemap.color = originalColor;
        }

        if (physicalCollider != null)
        {
            physicalCollider.enabled = true; 
        }
        isSolid = false;
    }
}