using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam; // 메인 카메라
    public float parallaxEffect; // 0~1 사이 값 (1=카메라와 같이 움직임, 0=안 움직임)

    void Start()
    {
        startpos = transform.position.x;
        // 스프라이트의 가로 길이를 가져옵니다. (무한 반복 배경일 경우 필요)
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        // 카메라가 이동한 거리 계산
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // 배경 이동
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // (선택) 배경이 화면 밖으로 나가면 위치를 리셋하여 무한 스크롤 구현
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}