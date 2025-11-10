using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool hasExploded = false;

    void Start()
    {
        
    }

    // 불 파티클이 닿았을 때
    private void OnParticleCollision(GameObject other)
    {
        // 이미 폭발했거나, 부딪힌 게 "Fire" 태그가 아니면 무시
        if (hasExploded || !other.CompareTag("Fire"))
        {
            return;
        }
        
        Explode();
    }


    // 폭발 함수
    private void Explode()
    {
        hasExploded = true;
        Destroy(gameObject); 
    }
}