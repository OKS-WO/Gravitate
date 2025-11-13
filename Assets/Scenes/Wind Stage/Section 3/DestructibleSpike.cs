using UnityEngine;
// using UnityEngine.SceneManagement; // 씬 재시작 시 필요할 수 있음

public class DestructibleSpike : MonoBehaviour
{
    private bool hasExploded = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded || !other.CompareTag("Fire"))
        {
            return;
        }
        Explode();
    }
    private void Explode()
    {
        hasExploded = true;
        Destroy(gameObject);
    }
}