using UnityEngine;
// (만약 MoveFire가 Tilemap이라면 using UnityEngine.Tilemaps;가 필요합니다)

public class Brazier : MonoBehaviour
{
    [Tooltip("불 능력을 사용하면 활성화될 자식 오브젝트(MoveFire)의 Animator")]
    public Animator fireAnimator; 

    [Tooltip("화로가 작동시킬 '구름 엘리베이터'의 Animator")]
    public Animator cloudElevatorAnimator; 

    private bool isLit = false; 

    void Start()
    {
        if (fireAnimator == null)
        {
            Debug.LogWarning(gameObject.name + "의 fireAnimator 변수에 'MoveFire'의 Animator가 연결되지 않았습니다.");
        }
    }

    // (OnParticleCollision, OnTriggerEnter2D 함수는 기존과 동일)
    private void OnParticleCollision(GameObject other)
    {
        if (isLit || !other.CompareTag("Fire"))
        {
            return;
        }
        ActivateBrazier();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLit || !other.CompareTag("UnBreakable")) // 흑요석 태그
        {
            return;
        }
        ActivateBrazier();
    }

    // 불 붙이는 함수
    private void ActivateBrazier()
    {
        if (isLit) return; 
        isLit = true;

        if (fireAnimator != null)
        {
            SpriteRenderer fireRenderer = fireAnimator.gameObject.GetComponent<SpriteRenderer>();

            if (fireRenderer != null)
            {
                fireRenderer.maskInteraction = SpriteMaskInteraction.None;
            }
            fireAnimator.SetTrigger("Ignite"); 
        }

        // 4. 구름의 "IsMoving" 불리언을 true로 바꿔 '움직이는' 애니메이션 재생
        if (cloudElevatorAnimator != null)
        {
            cloudElevatorAnimator.SetBool("IsMoving", true);
        }
    }

    // (선택 사항: 플레이어가 죽어서 리셋될 때 호출할 함수)
    public void ResetBrazier()
    {
        isLit = false;
        
        // --- ▼ [핵심 수정] Renderer -> SpriteRenderer로 변경 ▼ ---
        if (fireAnimator != null)
        {
            SpriteRenderer fireRenderer = fireAnimator.gameObject.GetComponent<SpriteRenderer>();
            if (fireRenderer != null)
            {
                // 1. 다시 'Visible Inside Mask'로 설정하여 숨깁니다.
                fireRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            
            // 2. 애니메이션 상태도 리셋합니다.
            fireAnimator.ResetTrigger("Ignite"); 
        }

        if (cloudElevatorAnimator != null)
        {
            cloudElevatorAnimator.SetBool("IsMoving", false); // 구름 멈춤
        }
    }
}