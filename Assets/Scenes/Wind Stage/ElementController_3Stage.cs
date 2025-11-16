using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트를 사용하기 위해 필수!

public class ElementController_3Stage : MonoBehaviour 
{
    [Header("Element Prefabs")]
    public GameObject RockPrefab;    // Q
    public GameObject WaterPrefab;   // W
    public GameObject AirPrefab;     // R (바람)
    public GameObject FirePrefab;    // E (불)
    public GameObject PreviewPrefab; // 이 프리팹의 'Sprite'가 교체됩니다.
    
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
        // 1. Q (흙) '누르기'
        if (Input.GetKeyDown(KeyCode.Q) && soil && currentGauge >= rockCost)
        {
            if (!isSetting)
            {
                isSetting = true; 
                InitializePreview("Q"); // [수정] "Q" 키 정보를 전달
                if (uiManager != null) uiManager.HighlightSlot("Q", true); 
                currentAbilityKey = "Q";
            }
        }
        // W (물) '누르기'
        if (Input.GetKeyDown(KeyCode.W) && water && currentGauge >= waterCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("W"); // [수정] "W" 키 정보를 전달
                if (uiManager != null) uiManager.HighlightSlot("W", true);
                currentAbilityKey = "W";
            }
        }
        // E (불) '누르기'
        if (Input.GetKeyDown(KeyCode.E) && fire && currentGauge >= fireCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("E"); // [수정] "E" 키 정보를 전달
                if (uiManager != null) uiManager.HighlightSlot("E", true);
                currentAbilityKey = "E";
            }
        }
        // R (바람) '누르기'
        if (Input.GetKeyDown(KeyCode.R) && air && currentGauge >= airCost)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview("R"); // [수정] "R" 키 정보를 전달
                if (uiManager != null) uiManager.HighlightSlot("R", true);
                currentAbilityKey = "R";
            }
        }

        // 2. 조준 중일 때 (isSetting == true)
        if (isSetting)
        {
            HandlePlacementInput();
            HandleRotationInput();

            // --- ▼ [수정] 능력 전환 시 미리보기 아이콘 즉시 변경 ▼ ---
            if (Input.GetKeyDown(KeyCode.Q) && soil && currentGauge >= rockCost)
            {
                currentAbilityKey = "Q";
                UpdatePreviewIcon("Q"); // [NEW] 미리보기 아이콘 변경
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("Q", true); 
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) && water && currentGauge >= waterCost)
            {
                currentAbilityKey = "W";
                UpdatePreviewIcon("W"); // [NEW] 미리보기 아이콘 변경
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("W", true); 
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && fire && currentGauge >= fireCost)
            {
                currentAbilityKey = "E";
                UpdatePreviewIcon("E"); // [NEW] 미리보기 아이콘 변경
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("R", false);
                    uiManager.HighlightSlot("E", true); 
                }
            }
            else if (Input.GetKeyDown(KeyCode.R) && air && currentGauge >= airCost)
            {
                currentAbilityKey = "R";
                UpdatePreviewIcon("R"); // [NEW] 미리보기 아이콘 변경
                if (uiManager != null)
                {
                    uiManager.HighlightSlot("Q", false);
                    uiManager.HighlightSlot("W", false);
                    uiManager.HighlightSlot("E", false);
                    uiManager.HighlightSlot("R", true); 
                }
            }
            // --- ▲ ---

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
            if (Input.GetKeyUp(KeyCode.E)) 
            {
                if(currentAbilityKey == "E")
                {
                    FinalizePlacementFire(targetPosition);
                }
            }
            if (Input.GetKeyUp(KeyCode.R)) 
            {
                if(currentAbilityKey == "R")
                {
                    FinalizePlacementAir(targetPosition);
                }
            }
        }
    }

    // --- ▼ [핵심 수정] 미리보기 초기화 함수 ▼ ---
    private void InitializePreview(string abilityKey) // [수정] 파라미터 추가
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
            
            // [NEW] UpdatePreviewIcon 함수를 호출하여 아이콘 설정
            UpdatePreviewIcon(abilityKey); 
        }
    }

    // [NEW] 미리보기 아이콘과 투명도를 설정하는 함수
    private void UpdatePreviewIcon(string abilityKey)
    {
        if (currentPreview == null || uiManager == null) return;

        SpriteRenderer previewRenderer = currentPreview.GetComponent<SpriteRenderer>();

        // 1. 아이콘 변경
        if (abilityKey == "Q") previewRenderer.sprite = uiManager.icon_Soil;
        else if (abilityKey == "W") previewRenderer.sprite = uiManager.icon_Water;
        else if (abilityKey == "E") previewRenderer.sprite = uiManager.icon_Fire;
        else if (abilityKey == "R") previewRenderer.sprite = uiManager.icon_Air;
        
        // 2. 투명도 50%로 설정
        previewRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }
    // --- ▲ ---

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

    // (Finalize 함수들은 게이지 소모 로직 및 UI 업데이트 추가)
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
    
    // (조준 종료 함수)
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
            uiManager.HighlightSlot("Q", false);
            uiManager.HighlightSlot("W", false);
            uiManager.HighlightSlot("E", false);
            uiManager.HighlightSlot("R", false);
        }

        currentAbilityKey = "";
    }
    
    // (RestoreAllGauge, Unlock..., UpdateAllUI 함수들은 기존과 동일)
    public void RestoreAllGauge()
    {
        currentGauge = maxGauge;
        if (uiManager != null)
        {
            uiManager.UpdateGaugeUI(currentGauge);
        }
    }
    public void UnlockSoil()
    {
        soil = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("Q", true);
        }
    }
    public void UnlockFire() 
    {
        fire = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("E", true); 
        }
    }
    public void UnlockWater() 
    {
        water = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("W", true);
        }
    }
    public void UnlockAir() 
    {
        air = true;
        if (uiManager != null)
        {
            uiManager.UnlockAbility("R", true); 
        }
    }

    public void UpdateAllUI()
    {
        if (uiManager == null) return;
        uiManager.UpdateGaugeUI(currentGauge);
        uiManager.UnlockAbility("Q", soil);
        uiManager.UnlockAbility("W", water); 
        uiManager.UnlockAbility("E", fire); 
        uiManager.UnlockAbility("R", air);  
    }
}