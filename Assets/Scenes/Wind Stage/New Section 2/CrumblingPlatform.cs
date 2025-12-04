using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; // 타일맵 사용 시 필수

public class CrumblingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float crumbleDelay = 1.0f;
    public float respawnDelay = 2.0f;
    public float shakeAmount = 0.1f;

    [Header("References")]
    [Tooltip("실제로 흔들릴 자식 오브젝트 (Tilemap/Sprite)")]
    public Transform visualTransform; // [NEW] 흔들 대상

    private bool isCrumbling = false;
    private Vector3 originalLocalPos; // 비주얼의 원래 로컬 위치
    private Collider2D platformCollider;
    private TilemapRenderer tilemapRenderer; // (또는 SpriteRenderer)
    private Tilemap tilemap; // 투명도 조절용

    void Start()
    {
        platformCollider = GetComponent<Collider2D>(); // 부모의 콜라이더

        // 자식(Visual)에서 렌더러와 타일맵을 찾습니다.
        if (visualTransform != null)
        {
            originalLocalPos = visualTransform.localPosition; // 자식의 로컬 위치 저장
            tilemapRenderer = visualTransform.GetComponent<TilemapRenderer>();
            tilemap = visualTransform.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError("Visual Transform이 연결되지 않았습니다!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCrumbling)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    StartCoroutine(Crumble());
                    break;
                }
            }
        }
    }

    IEnumerator Crumble()
    {
        isCrumbling = true;
        float timer = 0f;

        while (timer < crumbleDelay)
        {
            // [핵심] 자식 오브젝트만 상하(Y)로 흔들기
            // 부모(Collider)는 가만히 있고, 그림만 흔들립니다.
            float yShake = Random.Range(-1f, 1f) * shakeAmount;
            if (visualTransform != null)
            {
                visualTransform.localPosition = originalLocalPos + new Vector3(0, yShake, 0);
            }
            
            // 투명도 조절
            if (tilemap != null)
            {
                Color color = tilemap.color;
                color.a = Mathf.Lerp(1f, 0.0f, timer / crumbleDelay);
                tilemap.color = color;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // 사라지기
        if (visualTransform != null) visualTransform.localPosition = originalLocalPos; // 위치 복구
        DisablePlatform();

        // 리스폰
        if (respawnDelay > 0)
        {
            yield return new WaitForSeconds(respawnDelay);
            RespawnPlatform();
        }
    }

    void DisablePlatform()
    {
        if (platformCollider != null) platformCollider.enabled = false; // 충돌 끄기
        if (tilemapRenderer != null) tilemapRenderer.enabled = false; // 그림 끄기
    }

    void RespawnPlatform()
    {
        isCrumbling = false;
        if (platformCollider != null) platformCollider.enabled = true;
        if (tilemapRenderer != null) tilemapRenderer.enabled = true;
        
        if (tilemap != null)
        {
            Color color = tilemap.color;
            color.a = 1f;
            tilemap.color = color;
        }
    }
}