using UnityEngine;

public class AbilitySensor : MonoBehaviour
{
    private EvolvingIsland masterScript; 

    void Start()
    {
        masterScript = GetComponentInParent<EvolvingIsland>();
    }

    // --- 기존 코드 ---
    // 파티클 시스템(예: 불, 물)이 닿았을 때 호출됩니다.
    private void OnParticleCollision(GameObject other)
    {
        if (masterScript != null && !masterScript.isLaunched)
        {
            // 닿은 파티클의 태그(Tag)를 부모로 전달
            masterScript.HandleAbilityHit(LayerMask.LayerToName(other.layer));
        }
    }

    // --- 새로 추가된 코드 ---
    // 물리적 오브젝트(예: 흙 블럭)가 부딪혔을 때 호출됩니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (masterScript == null || masterScript.isLaunched) return;

        // 부딪힌 오브젝트의 태그(Tag)를 부모로 전달
        bool success = masterScript.HandleAbilityHit(LayerMask.LayerToName(collision.gameObject.layer));

        if (success)
        {
            string layerName = LayerMask.LayerToName(collision.gameObject.layer);
            //if(layerName != "Rock")
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (masterScript == null || masterScript.isLaunched) return;

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        
        if (layerName == "Obsidian")
        {
            bool success = masterScript.HandleAbilityHit(layerName);
            
            if (success)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}