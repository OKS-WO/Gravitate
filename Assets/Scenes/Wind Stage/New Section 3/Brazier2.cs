using UnityEngine;

public class Brazier2 : MonoBehaviour
{
    [Header("Targets")]
    [Tooltip("화로에 불이 붙으면 나타날 불꽃 파티클/오브젝트 (자식)")]
    public GameObject fireEffect; 

    [Tooltip("화로가 작동시킬 움직이는 구름 오브젝트")]
    public MovingCloudTarget2 targetCloud; 

    private bool isLit = false; 

    void Start()
    {
        // 게임 시작 시 불꽃 숨기기
        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
        }
    }

    // 'Fire' 파티클과 충돌 시
    private void OnParticleCollision(GameObject other)
    {
        if (!isLit && other.CompareTag("Fire"))
        {
            Ignite();
        }
    }

    void Ignite()
    {
        isLit = true;

        // 1. 숨겨진 불꽃 이펙트 보이기
        if (fireEffect != null)
        {
            fireEffect.SetActive(true);
        }

        // 2. 연결된 구름 움직이기 시작
        if (targetCloud != null)
        {
            targetCloud.StartMoving();
        }
        
        // (선택) 점화 효과음 재생
        // AudioSource.PlayClipAtPoint(igniteSound, transform.position);
    }
}