using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Targets")]
    public GameObject cam; // 메인 카메라

    [Header("Settings")]
    [Tooltip("0 ~ 1 사이의 값. 1에 가까울수록 카메라를 따라다님 (멀리 있는 배경)")]
    [Range(0f, 1f)]
    public float parallaxEffect;

    [Tooltip("구름이 스스로 흘러가는 속도 (바람 효과)")]
    public float autoMoveSpeed = 0f; 

    private float length, startpos;

    void Start()
    {
        // 1. 시작 위치 저장 (이것 때문에 게임 시작 시 위치가 튀지 않습니다)
        startpos = transform.position.x;
        
        // 2. 이미지의 가로 길이 계산 (무한 스크롤용)
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
        // 3. 카메라 자동 찾기 (연결 안 했을 경우)
        if (cam == null) cam = Camera.main.gameObject;
    }

    void LateUpdate()
    {
        // 1. 카메라 이동에 따른 거리 계산
        // (1 - parallaxEffect)만큼만 이동 = 멀리 있는 물체는 덜 움직임
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // 2. 자동 이동(바람) 효과 추가
        if (autoMoveSpeed != 0)
        {
            startpos += autoMoveSpeed * Time.fixedDeltaTime;
        }

        // 3. 배경 위치 갱신
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // 4. 무한 스크롤 (배경이 화면 밖으로 나가면 위치 재조정)
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}