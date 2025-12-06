using UnityEngine;

public class BackgroundActivator : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;      // 플레이어
    public Transform mainCamera;  // 메인 카메라

    [Header("Settings")]
    [Tooltip("이 높이 이상 올라가면 배경이 따라오기 시작함")]
    public float activationHeight = 10f;

    [Tooltip("X축(가로)도 따라갈지 여부 (보통 배경은 Y축만 따라가므로 체크 해제 권장)")]
    public bool followX = false; 

    private bool isActive = false;
    private Vector3 offset;       // 카메라와 배경 사이의 거리 차이
    private Vector3 initialPosition; // 맨 처음 위치 저장

    void Start()
    {
        // 1. 카메라 자동 찾기
        if (mainCamera == null) mainCamera = Camera.main.transform;

        // 2. 시작 위치 저장
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player == null || mainCamera == null) return;

        // --- 1. 활성화 조건 체크 ---
        if (!isActive)
        {
            if (player.position.y >= activationHeight)
            {
                // [핵심] 활성화되는 순간, 현재 위치와 카메라 위치의 차이(오프셋)를 계산해서 저장
                // 이렇게 하면 배경이 갑자기 튀지 않습니다.
                offset = transform.position - mainCamera.position;
                isActive = true;
            }
            else
            {
                // 활성화 전에는 원래 위치 고정
                transform.position = initialPosition;
            }
        }
        else
        {
            // --- 2. 활성화 후 (카메라 따라다니기) ---
            
            // 플레이어가 다시 아래로 내려가면 비활성화
            if (player.position.y < activationHeight)
            {
                isActive = false;
                transform.position = initialPosition; // 원위치 복귀
                return;
            }

            // 카메라 위치 + 저장해둔 오프셋 = 배경 위치
            float targetY = mainCamera.position.y + offset.y;
            float targetX = followX ? (mainCamera.position.x + offset.x) : initialPosition.x;

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}