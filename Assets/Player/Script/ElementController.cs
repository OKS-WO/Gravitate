using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public GameObject RockPrefab;
    public GameObject WaterPrefab;
    public GameObject AirPrefab;
    public GameObject PreviewPrefab;
    

    // ?? 생성 위치 오프셋을 Vector2로 관리 (X, Y만 사용)
    private Vector2 currentOffset = new Vector2(0f, 1f); // 시작 위치: 오브젝트 바로 위 1f

    private GameObject currentPreview;
    public bool isSetting = false;

    // 격자 이동 단위
    public float gridUnit = 1f;

    void Update()
    {
        // 1. Q 키를 누르는 순간 (배치 모드 시작)
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E))
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
            }
        }

        // 2. 배치 모드 유지 (미리보기 이동 및 최종 생성)
        if (isSetting)
        {
            // ?? 키보드 입력에 따른 오프셋 업데이트
            HandlePlacementInput();

            // 현재 위치 계산: 스크립트 위치 + 오프셋
            Vector3 targetPosition = (Vector2)transform.position + currentOffset;

            // 미리보기 오브젝트 위치 업데이트
            if (currentPreview != null)
            {
                currentPreview.transform.position = targetPosition;
            }

            // 3. Q 키를 떼는 순간 (오브젝트 생성)
            if (Input.GetKeyUp(KeyCode.Q))
            {
                FinalizePlacementR(targetPosition);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                FinalizePlacementW(targetPosition);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                FinalizePlacementE(targetPosition);
            }
        }
    }

    // ---------------------------------------------
    // ?? 미리보기 오브젝트 초기화 함수
    private void InitializePreview()
    {
        // 초기 위치를 오브젝트 바로 위 1f로 설정
        currentOffset = new Vector2(0f, 1f);

        if (PreviewPrefab != null)
        {
            // 기존 미리보기가 없다면 생성
            if (currentPreview == null)
            {
                currentPreview = Instantiate(PreviewPrefab, Vector3.zero, Quaternion.identity);
            }
            // 이미 있다면 활성화만
            else
            {
                currentPreview.SetActive(true);
            }
        }
    }

    // ?? 키보드 입력 처리 함수 (상하좌우 오프셋 변경)
    private void HandlePlacementInput()
    {
        // 상하좌우 입력 확인
        float xInput = Input.GetAxisRaw("Horizontal"); // A/D 또는 좌우 화살표
        float yInput = Input.GetAxisRaw("Vertical");   // W/S 또는 상하 화살표
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentOffset.x += xInput * gridUnit;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentOffset.y += yInput * gridUnit;
        }
    }

    // ?? 최종 배치 및 정리 함수
    private void FinalizePlacementR(Vector3 finalPosition)
    {
        if (RockPrefab != null)
        {
            Instantiate(RockPrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // 다음 사용을 위해 미리보기 비활성화 및 정리
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    private void FinalizePlacementW(Vector3 finalPosition)
    {
        if (RockPrefab != null)
        {
            Instantiate(WaterPrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // 다음 사용을 위해 미리보기 비활성화 및 정리
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    private void FinalizePlacementE(Vector3 finalPosition)
    {
        if (RockPrefab != null)
        {
            Instantiate(AirPrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // 다음 사용을 위해 미리보기 비활성화 및 정리
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }
}