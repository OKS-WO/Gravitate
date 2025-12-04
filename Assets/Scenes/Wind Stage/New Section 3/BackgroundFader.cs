using UnityEngine;

public class BackgroundFader : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;

    // --- ▼ [수정] 여러 개의 배경을 담을 수 있도록 배열([])로 변경 ▼ ---
    [Tooltip("점점 투명해질 밝은 배경들 (리스트)")]
    public SpriteRenderer[] brightBGs; 
    
    // (어두운 배경은 뒤에 가만히 있으면 되므로 굳이 스크립트로 제어 안 해도 됩니다.
    // 만약 어두운 배경도 투명도를 조절하고 싶다면 똑같이 배열로 만드세요.)
    // public SpriteRenderer[] darkBGs; 

    [Header("Settings")]
    [Tooltip("배경 전환이 시작되는 높이 (Y좌표)")]
    public float startHeight = 50f;

    [Tooltip("배경 전환이 완전히 끝나는 높이 (Y좌표)")]
    public float endHeight = 100f;

    void Start()
    {
        Debug.Log("등록된 밝은 배경 개수: " + brightBGs.Length);
    }

    void Update()
    {
        if (player == null || brightBGs == null) return;

        // 1. 진행률 계산 (0.0 ~ 1.0)
        float currentY = player.position.y;
        float t = Mathf.InverseLerp(startHeight, endHeight, currentY);
        float newAlpha = 1f - t; // 1.0(불투명) -> 0.0(투명)

        // 2. 등록된 모든 밝은 배경들의 투명도를 한꺼번에 조절
        foreach (SpriteRenderer bg in brightBGs)
        {
            if (bg != null)
            {
                Color color = bg.color;
                color.a = newAlpha;
                bg.color = color;
            }
        }
    }
}