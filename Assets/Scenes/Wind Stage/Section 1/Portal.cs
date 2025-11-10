using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("이 포탈과 연결된 '출구' 포탈의 Transform")]
    public Transform exitPortal; // 1. 출구 포탈을 연결할 슬롯

    // 2. 'Is Trigger'가 켜진 콜라이더에 무언가 들어왔을 때 1번 실행
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 3. 들어온 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 4. 플레이어의 위치를 'exitPortal'의 위치로 즉시 변경(순간이동)
            other.transform.position = exitPortal.position;
        }

        // (만약 흙 블럭 등 다른 오브젝트도 이동시키려면 'other.CompareTag("EarthBlock")' 등 추가)
    }
}