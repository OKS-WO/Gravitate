using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // [NEW] UI 컴포넌트를 사용하기 위해 필수!

// [참고] 이 클래스 이름은 Movement.cs가 찾는 "ElementController" 이거나,
//       Movement.cs의 16라인(GetComponent<...>)을 이 파일 이름으로 수정해야 합니다.
public class ElementController_3Stage : MonoBehaviour 
{
    [Header("Element Prefabs")]
    public GameObject RockPrefab;    // Q
    public GameObject WaterPrefab;   // W
    public GameObject AirPrefab;     // R (바람) [키 변경]
    public GameObject FirePrefab;    // E (불) [키 변경]
    public GameObject PreviewPrefab;
    
    [Header("UI & Gauge")]
    private UIManager uiManager; // [NEW] UIManager 스크립트 참조
    public int maxGauge = 5;     // [NEW] 최대 게이지 5칸
    public int currentGauge;   // [NEW] 현재 게이지

    [Header("Ability Costs")]
    public int rockCost = 1;     // [NEW]
    public int waterCost = 1;    // [NEW]
    public int airCost = 1;      // [NEW] R (바람)
    public int fireCost = 1;     // [NEW] E (불)

    private Vector2 currentOffset = new Vector2(0f, 1f); 
    private GameObject currentPreview;
    
    // [중요] Movement.cs가 이 변수를 읽어서 이동을 멈출 것입니다.
    public bool isSetting = false; 
    private string currentAbilityKey = "";
    private float gridUnit = 1f;
    private float currentAngle = 0f;

    [Header("Unlocked Abilities")]
    public bool soil = false; 
    public bool water = false; 
    public bool air = false;  
    public bool fire = false; 

    // --- ▼ [NEW] Start 함수 추가 (UI 및 게이지 초기화) ▼ ---
    void Start()
    {
        // UIManager를 씬에서 자동으로 찾습니다.
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager를 씬에서 찾을 수 없습니다!");
        }

        currentGauge = maxGauge; // 게이지를 5칸으로 채움
        UpdateAllUI(); // UI 초기 상태 업데이트
    }
    // --- ▲ ---

    // --- ▼ [수정] 원본(ElementController(UI 추가 전).txt)의 Update 로직 + 게이지/UI 확인 ▼ ---
    void Update()
    {
        // 1. Q (흙) '누르기' (게이지 확인 추가)
        if (Input.GetKeyDown(KeyCode.Q) && soil && currentGauge >= rockCost)
        {
            if (!isSetting)
            {
                isSetting = true; // Movement.cs가 이 값을 보고 이동을 멈춤
                InitializePreview();
                if (uiManager != null) uiManager.HighlightSlot("Q", true); 
                currentAbilityKey = "Q";
            }
        }
        // W (물) '누르기' (게이지 확인 추가)
        if (Input.GetKeyDown(KeyCode.W) && water && currentGauge >= waterCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
                if (uiManager != null) uiManager.HighlightSlot("W", true);
                currentAbilityKey = "W";
            }
        }
        // E (불) '누르기' [키 변경 + 게이지 확인]
        if (Input.GetKeyDown(KeyCode.E) && fire && currentGauge >= fireCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
                if (uiManager != null) uiManager.HighlightSlot("E", true);
                currentAbilityKey = "E";
            }
        }
        // R (바람) '누르기' [키 변경 + 게이지 확인]
        if (Input.GetKeyDown(KeyCode.R) && air && currentGauge >= airCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
                if (uiManager != null) uiManager.HighlightSlot("R", true);
                currentAbilityKey = "R";
            }
        }

        // 2. 조준 중일 때 (isSetting == true)
        if (isSetting)
        {
            // (조준점 이동)
            HandlePlacementInput();
            HandleRotationInput();

            if (Input.GetKeyDown(KeyCode.Q) && soil && currentGauge >= rockCost)
            {
                currentAbilityKey = "Q";
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("Q", true); // Q 활성화
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) && water && currentGauge >= waterCost)
            {
                currentAbilityKey = "W";
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("W", true); // W 활성화
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && fire && currentGauge >= fireCost)
            {
                currentAbilityKey = "E";
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("E", true); // E 활성화
                }
            }
            else if (Input.GetKeyDown(KeyCode.R) && air && currentGauge >= airCost)
            {
                currentAbilityKey = "R";
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", true); // R 활성화
                }
            }

            Vector3 targetPosition = (Vector2)transform.position + currentOffset;
            if (currentPreview != null)
            {
                currentPreview.transform.position = targetPosition;
            }
            
            // 3. 키 '떼기' (생성)
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if(currentAbilityKey == "Q")
                {
                    FinalizePlacementRock(targetPosition);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                if(currentAbilityKey == "W")
                {
                    FinalizePlacementWater(targetPosition);
                }
            }
            if (Input.GetKeyUp(KeyCode.E)) // [키 변경]
            {
                if(currentAbilityKey == "E")
                {
                    FinalizePlacementFire(targetPosition);
                }
            }
            if (Input.GetKeyUp(KeyCode.R)) // [키 변경]
            {
                if(currentAbilityKey == "R")
                {
                    FinalizePlacementAir(targetPosition);
                }
            }
        }
    }
    // --- ▲ ---

    // (미리보기 초기화 함수 - 투명도 50% 적용, 위치 리셋 유지)
    private void InitializePreview()
    {
        currentOffset = new Vector2(0f, 1f); // 원본 로직: 조준 시작 시 위치 초기화

        if (PreviewPrefab != null)
        {
            if (currentPreview == null)
            {
                currentPreview = Instantiate(PreviewPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                currentPreview.SetActive(true);
            }
            // [NEW] 미리보기 색상을 50% 투명도로 초기화
            currentPreview.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    // (HandlePlacementInput, HandleRotationInput 함수는 원본과 동일)
    private void HandlePlacementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { currentOffset.x -= gridUnit; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { currentOffset.x += gridUnit; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { currentOffset.y += gridUnit; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { currentOffset.y -= gridUnit; }
    }
    private void HandleRotationInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (yInput < 0) { currentAngle = 180f; }
        else if (yInput > 0) { currentAngle = 0f; }
        else if (xInput > 0) { currentAngle = 270f; }
        else if (xInput < 0) { currentAngle = 90f; }
    }

    // --- ▼ [NEW] Finalize 함수들 수정: 게이지 소모 로직 및 UI 업데이트 추가 ▼ ---

    private void FinalizePlacementRock(Vector3 finalPosition)
    {
        if (currentGauge >= rockCost) 
        {
            if (RockPrefab != null) Instantiate(RockPrefab, finalPosition, Quaternion.identity);
            currentGauge -= rockCost; 
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementWater(Vector3 finalPosition)
    {
        if (currentGauge >= waterCost)
        {
            if (WaterPrefab != null) Instantiate(WaterPrefab, finalPosition, Quaternion.Euler(0f, 0f, 0f));
            currentGauge -= waterCost;
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementAir(Vector3 finalPosition)
    {
        if (currentGauge >= airCost)
        {
            if (AirPrefab != null) Instantiate(AirPrefab, finalPosition, Quaternion.identity);
            currentGauge -= airCost;
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementFire(Vector3 finalPosition)
    {
        if (currentGauge >= fireCost)
        {
            if (FirePrefab != null) Instantiate(FirePrefab, finalPosition, Quaternion.identity);
            currentGauge -= fireCost;
        }
        CleanupAfterPlacement(true);
    }
    
    // [NEW] 조준 종료 함수 (UI 업데이트 포함)
    private void CleanupAfterPlacement(bool updateGauge)
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview); 
            currentPreview = null;
        }

        isSetting = false; 

        if (uiManager != null)
        {
            if (updateGauge) 
            {
                uiManager.UpdateGaugeUI(currentGauge);
            }
            // 하이라이트 끄기
            uiManager.HighlightSlot("Q", false);
            uiManager.HighlightSlot("W", false);
            uiManager.HighlightSlot("E", false);
            uiManager.HighlightSlot("R", false);
        }

        currentAbilityKey = "";
    }
    public void RestoreAllGauge()
    {
        currentGauge = maxGauge;
        if (uiManager != null)
        {
            uiManager.UpdateGaugeUI(currentGauge);
        }
    }

    // (예: 흙 신전 클리어 시 이 함수를 호출)
    public void UnlockSoil()
    {
        soil = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("Q", true);
        }
    }
    
    // (불 신전 클리어 시...)
    public void UnlockFire() 
    {
        fire = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("E", true); // [키 변경] E = 불
        }
    }
    
    // (물 신전 클리어 시...)
    public void UnlockWater() 
    {
        water = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("W", true);
        }
    }

    // (바람 신전 클리어 시...)
    public void UnlockAir() 
    {
        air = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("R", true); // [키 변경] R = 바람
        }
    }

    // (게임 시작 시 모든 UI를 현재 상태에 맞게 초기화)
    public void UpdateAllUI()
    {
        if (uiManager == null) return;
        uiManager.UpdateGaugeUI(currentGauge);
        uiManager.UnlockAbility("Q", soil);
        uiManager.UnlockAbility("W", water); 
        uiManager.UnlockAbility("E", fire); // E = 불
        uiManager.UnlockAbility("R", air);  // R = 바람
    }
    // --- ▲ ---
}