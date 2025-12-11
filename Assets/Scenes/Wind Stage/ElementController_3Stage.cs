using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementController_3Stage : MonoBehaviour 
{
    [Header("Element Prefabs")]
    public GameObject RockPrefab;    // 흙
    public GameObject WaterPrefab;   // 물
    public GameObject AirPrefab;     // 바람
    public GameObject FirePrefab;    // 불
    public GameObject PreviewPrefab;
    
    [Header("UI & Gauge")]
    private UIManager uiManager; 
    public int maxGauge = 5;
    public int currentGauge;

    [Header("Ability Costs")]
    public int rockCost = 1;
    public int waterCost = 1;
    public int airCost = 1; 
    public int fireCost = 1; 

    private Vector2 currentOffset = new Vector2(0f, 1f); 
    private GameObject currentPreview;
    public bool isSetting = false; 
    private string currentAbilityKey = "";
    private float gridUnit = 1f;
    private float currentAngle = 0f;

    [Header("Unlocked Abilities")]
    public bool soil = false; 
    public bool water = false; 
    public bool air = false;
    public bool fire = false; 

    Movement_3Stage movement;

    void Start()
    {
        movement = GetComponent<Movement_3Stage>();
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager를 씬에서 찾을 수 없습니다!");
        }

        currentGauge = maxGauge; 
        UpdateAllUI();
    }

    void Update()
    {
        // --- 1. 능력 선택 (키 매핑 변경) ---

        // Q: 바람 (Air) [변경]
        if (Input.GetKeyDown(KeyCode.Q) && air && currentGauge >= airCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("Q");
                if (uiManager != null) uiManager.HighlightSlot("Q", true);
                currentAbilityKey = "Q";
            }
        }
        // W: 흙 (Soil/Rock) [변경]
        if (Input.GetKeyDown(KeyCode.W) && soil && currentGauge >= rockCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("W");
                if (uiManager != null) uiManager.HighlightSlot("W", true);
                currentAbilityKey = "W";
            }
        }
        // E: 물 (Water) [변경]
        if (Input.GetKeyDown(KeyCode.E) && water && currentGauge >= waterCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("E");
                if (uiManager != null) uiManager.HighlightSlot("E", true);
                currentAbilityKey = "E";
            }
        }
        // R: 불 (Fire) [변경]
        if (Input.GetKeyDown(KeyCode.R) && fire && currentGauge >= fireCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("R");
                if (uiManager != null) uiManager.HighlightSlot("R", true);
                currentAbilityKey = "R";
            }
        }

        // 2. 조준 중일 때 (isSetting == true)
        if (isSetting)
        {
            HandlePlacementInput();
            HandleRotationInput();

            // --- 능력 전환 (조준 중 다른 키 누름) ---
            
            // Q: 바람 [변경]
            if (Input.GetKeyDown(KeyCode.Q) && air && currentGauge >= airCost)
            {
                currentAbilityKey = "Q";
                UpdatePreviewIcon("Q"); 
                if (uiManager != null)
                {
                    ResetHighlights();
                    uiManager.HighlightSlot("Q", true); 
                }
            }
            // W: 흙 [변경]
            else if (Input.GetKeyDown(KeyCode.W) && soil && currentGauge >= rockCost)
            {
                currentAbilityKey = "W";
                UpdatePreviewIcon("W");
                if (uiManager != null)
                {
                    ResetHighlights();
                    uiManager.HighlightSlot("W", true); 
                }
            }
            // E: 물 [변경]
            else if (Input.GetKeyDown(KeyCode.E) && water && currentGauge >= waterCost)
            {
                currentAbilityKey = "E";
                UpdatePreviewIcon("E");
                if (uiManager != null)
                {
                    ResetHighlights();
                    uiManager.HighlightSlot("E", true); 
                }
            }
            // R: 불 [변경]
            else if (Input.GetKeyDown(KeyCode.R) && fire && currentGauge >= fireCost)
            {
                currentAbilityKey = "R";
                UpdatePreviewIcon("R");
                if (uiManager != null)
                {
                    ResetHighlights();
                    uiManager.HighlightSlot("R", true); 
                }
            }

            Vector3 targetPosition = (Vector2)transform.position + currentOffset;
            if (currentPreview != null)
            {
                currentPreview.transform.position = targetPosition;
            }
            
            // 3. 키 '떼기' (최종 생성) [변경]
            
            // Q 뗌 -> 바람 생성
            if (Input.GetKeyUp(KeyCode.Q) && currentAbilityKey == "Q")
            {
                FinalizePlacementAir(targetPosition);
            }
            // W 뗌 -> 흙 생성
            if (Input.GetKeyUp(KeyCode.W) && currentAbilityKey == "W")
            {
                FinalizePlacementRock(targetPosition);
            }
            // E 뗌 -> 물 생성
            if (Input.GetKeyUp(KeyCode.E) && currentAbilityKey == "E")
            {
                FinalizePlacementWater(targetPosition);
            }
            // R 뗌 -> 불 생성
            if (Input.GetKeyUp(KeyCode.R) && currentAbilityKey == "R")
            {
                FinalizePlacementFire(targetPosition);
            }
        }
    }

    // 하이라이트 초기화 헬퍼 함수
    private void ResetHighlights()
    {
        if (uiManager == null) return;
        uiManager.HighlightSlot("Q", false);
        uiManager.HighlightSlot("W", false);
        uiManager.HighlightSlot("E", false);
        uiManager.HighlightSlot("R", false);
    }

    private void InitializePreview(string abilityKey)
    {
        currentOffset = new Vector2(0f, 1f);
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
            
            UpdatePreviewIcon(abilityKey);
        }
    }

    // [변경] 미리보기 아이콘 매핑 수정
    private void UpdatePreviewIcon(string abilityKey)
    {
        if (currentPreview == null || uiManager == null) return;
        SpriteRenderer previewRenderer = currentPreview.GetComponent<SpriteRenderer>();

        // 키에 맞는 아이콘으로 변경
        if (abilityKey == "Q") previewRenderer.sprite = uiManager.icon_Air;   // Q -> 바람 아이콘
        else if (abilityKey == "W") previewRenderer.sprite = uiManager.icon_Soil;  // W -> 흙 아이콘
        else if (abilityKey == "E") previewRenderer.sprite = uiManager.icon_Water; // E -> 물 아이콘
        else if (abilityKey == "R") previewRenderer.sprite = uiManager.icon_Fire;  // R -> 불 아이콘
        
        previewRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void HandlePlacementInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) currentOffset.x -= gridUnit;
        if (Input.GetKeyDown(KeyCode.RightArrow)) currentOffset.x += gridUnit;
        if (Input.GetKeyDown(KeyCode.UpArrow)) currentOffset.y += gridUnit;
        if (Input.GetKeyDown(KeyCode.DownArrow)) currentOffset.y -= gridUnit;
    }

    private void HandleRotationInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (yInput < 0) currentAngle = 180f;
        else if (yInput > 0) currentAngle = 0f;
        else if (xInput > 0) currentAngle = 270f;
        else if (xInput < 0) currentAngle = 90f;
    }

    // 생성 함수들은 그대로 유지 (호출하는 곳만 변경됨)
    private void FinalizePlacementRock(Vector3 finalPosition)
    {
        if (currentGauge >= rockCost) 
        {
            if (RockPrefab != null)
            {
                movement.playSound("SET");
                Instantiate(RockPrefab, finalPosition, Quaternion.identity);
            }
            currentGauge -= rockCost;
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementWater(Vector3 finalPosition)
    {
        if (currentGauge >= waterCost)
        {
            if (WaterPrefab != null) 
            {
                movement.playSound("SET");
                Instantiate(WaterPrefab, finalPosition, Quaternion.Euler(0f, 0f, 0f));
            }
            currentGauge -= waterCost;
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementAir(Vector3 finalPosition)
    {
        if (currentGauge >= airCost)
        {
            if (AirPrefab != null) 
            {
                movement.playSound("SET");
                Instantiate(AirPrefab, finalPosition, Quaternion.identity);
            }
            currentGauge -= airCost;
        }
        CleanupAfterPlacement(true);
    }
    private void FinalizePlacementFire(Vector3 finalPosition)
    {
        if (currentGauge >= fireCost)
        {
            if (FirePrefab != null) 
            {
                movement.playSound("SET");
                Instantiate(FirePrefab, finalPosition, Quaternion.identity);
            }
            currentGauge -= fireCost;
        }
        CleanupAfterPlacement(true);
    }
    
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
            ResetHighlights(); // 위에서 만든 헬퍼 함수 사용
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

    // [변경] UI 해금 매핑 수정
    // Unlock 함수가 호출될 때 UI의 어떤 슬롯을 열지 결정합니다.
    public void UnlockSoil()
    {
        soil = true;
        if (uiManager != null) uiManager.UnlockAbility("W", true); // 흙 -> W 슬롯
    }
    public void UnlockFire() 
    {
        fire = true;
        if (uiManager != null) uiManager.UnlockAbility("R", true); // 불 -> R 슬롯
    }
    public void UnlockWater() 
    {
        water = true;
        if (uiManager != null) uiManager.UnlockAbility("E", true); // 물 -> E 슬롯
    }
    public void UnlockAir() 
    {
        air = true;
        if (uiManager != null) uiManager.UnlockAbility("Q", true); // 바람 -> Q 슬롯
    }

    // [변경] 초기 UI 업데이트 매핑 수정
    public void UpdateAllUI()
    {
        if (uiManager == null) return;
        uiManager.UpdateGaugeUI(currentGauge);
        
        uiManager.UnlockAbility("Q", air);  // Q 슬롯은 바람 상태 표시
        uiManager.UnlockAbility("W", soil); // W 슬롯은 흙 상태 표시
        uiManager.UnlockAbility("E", water); // E 슬롯은 물 상태 표시
        uiManager.UnlockAbility("R", fire); // R 슬롯은 불 상태 표시
    }
}