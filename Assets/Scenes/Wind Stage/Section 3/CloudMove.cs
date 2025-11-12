using UnityEngine;

public class ActivateOnTouch : MonoBehaviour
{
    private Animator animator;
    private bool hasBeenActivated = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenActivated || !collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb == null) return;

        if (playerRb.velocity.y <= 0.1f)
        {
            if (animator != null)
            {
                animator.enabled = true;
                hasBeenActivated = true; // 한 번만 실행되도록 상태 변경
            }
        }
    }
}