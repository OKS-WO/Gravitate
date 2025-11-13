using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapTrigger : MonoBehaviour
{
    public Rigidbody2D spikeTrap; // 1단계에서 만든 'SpikeTrap'을 연결할 슬롯
    public float fallSpeedMultiplier = 10f;
    private bool isTriggered = false; // 함정이 한 번만 발동하도록 체크
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Rock"))
        {
            ActivateTrap();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire") || other.CompareTag("Water"))
        {
            ActivateTrap();
        }
    }
    private void ActivateTrap()
    {
        // 이미 발동했다면 중복 실행 방지
        if (isTriggered || spikeTrap == null)
        {
            return;
        }

        isTriggered = true;

        TilemapRenderer spikeTilemapRenderer = spikeTrap.gameObject.GetComponent<TilemapRenderer>();

        if (spikeTilemapRenderer != null)
        {
            spikeTilemapRenderer.maskInteraction = SpriteMaskInteraction.None;
        }

        // 함정의 Rigidbody 2D를 'Dynamic'으로 바꿔 중력을 받게 함
        spikeTrap.bodyType = RigidbodyType2D.Dynamic;
        spikeTrap.gravityScale = fallSpeedMultiplier;
        // (선택사항) 센서를 1회용으로 만듬
        // gameObject.SetActive(false); 
    }
}