using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [Header("Destruction Settings")]
    [Tooltip("이 Y좌표보다 아래로 떨어지면 파괴됩니다. (예: -20)")]
    public float destructionHeightY = -20f;

    [Tooltip("파괴될 때 생성할 파티클 이펙트 (선택 사항)")]
    public GameObject destructionEffect; 
    
    private bool isDestroyed = false; // 중복 파괴 방지

    // --- [!! 1. 수정된 함수: OnTriggerEnter2D !!] ---
    // (Is Trigger = true로 설정했으므로, OnCollisionEnter2D 대신 이 함수가 호출됨)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed) return;

        // A. "Fire" 태그(Tag)를 가진 트리거와 닿았는지 확인
        // (DestructibleSpike.cs의 기능)
        if (other.CompareTag("Fire"))
        {
            DestroyRock();
            return; // 이미 파괴됨
        }

        // B. "Player" 태그(Tag)를 가진 오브젝트와 닿았는지 확인
        // (FallingRockCollision.cs의 기능)
        if (other.CompareTag("Player"))
        {
            DestroyRock();
            return; // 이미 파괴됨
        }
        
        // "Player"나 "Fire"가 아닌 다른 것(구름, 바닥)은
        // Is Trigger 상태이므로 그냥 통과하고, 이 스크립트도 무시합니다.
    }

    // --- 2. Y 높이 감시 (Update) ---
    void Update()
    {
        if (isDestroyed) return;

        // 현재 Y좌표가 파괴 높이보다 낮은지 확인
        if (transform.position.y < destructionHeightY)
        {
            DestroyRock();
        }
    }

    // --- 3. 공용 파괴 함수 ---
    private void DestroyRock()
    {
        isDestroyed = true;

        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}