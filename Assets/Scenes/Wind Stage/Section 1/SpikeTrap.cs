using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasBeenTriggered = false; // 함정이 발동되었는지 확인

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 시작 시 Kinematic 상태인지 확인
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            // (만약의 경우) Dynamic으로 시작했다면, 이미 발동된 것으로 간주
            hasBeenTriggered = true; 
        }
    }

    void Update()
    {
        // TrapTrigger에 의해 Dynamic으로 변경된 것을 감지
        if (!hasBeenTriggered && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            hasBeenTriggered = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenTriggered)
        {
            Destroy(gameObject);
        }
    }
}