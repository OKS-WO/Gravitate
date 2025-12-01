using System.Collections;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    [Header("감시할 대상 (프로펠러)")]
    [Tooltip("여기에 파괴되어야 할 프로펠러들을 모두 넣으세요.")]
    public GameObject[] propellers; 

    [Header("제어할 대상 (계단)")]
    [Tooltip("순서대로 나타날 계단 오브젝트들을 넣으세요.")]
    public GameObject[] stairs; 

    [Header("설정")]
    public float activeTime = 2.0f; // 계단이 켜져있는 시간
    public float delayTime = 0.5f;  // 다음 계단이 켜지기 전 대기 시간

    private bool isPatternStarted = false;

    void Start()
    {
        // 1. 게임 시작 시 모든 계단을 숨김
        foreach (GameObject stair in stairs)
        {
            if (stair != null) stair.SetActive(false);
        }
    }

    void Update()
    {
        // 2. 패턴이 아직 시작 안 됐고, 모든 프로펠러가 파괴되었는지 확인
        if (!isPatternStarted && CheckAllPropellersDestroyed())
        {
            isPatternStarted = true;
            StartCoroutine(StairRoutine());
        }
    }

    // 프로펠러가 모두 파괴되었는지 확인하는 함수
    private bool CheckAllPropellersDestroyed()
    {
        foreach (GameObject prop in propellers)
        {
            // 하나라도 살아있다면(null이 아니라면) 아직 파괴 안 된 것임
            if (prop != null) 
            {
                return false;
            }
        }
        // 루프를 다 돌았는데 살아있는게 없다면 모두 파괴된 것
        return true;
    }

    // 계단 무한 반복 코루틴
    IEnumerator StairRoutine()
    {
        Debug.Log("모든 프로펠러 파괴됨! 계단 패턴 시작.");
        
        while (true)
        {
            foreach (GameObject stair in stairs)
            {
                if (stair == null) continue;

                // 켜기
                stair.SetActive(true);
                yield return new WaitForSeconds(activeTime);

                // 끄기
                stair.SetActive(false);
                
                // 대기
                if (delayTime > 0) yield return new WaitForSeconds(delayTime);
            }
        }
    }
}