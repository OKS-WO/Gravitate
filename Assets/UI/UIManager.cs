using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스를 사용하기 위해 필요

public class UIManager : MonoBehaviour
{
    // --- 게이지 관련 변수 ---
    [Header("Gauge UI Elements")]
    public Image[] gaugeCrystals; 
    public Sprite gaugeFullSprite;  
    public Sprite gaugeEmptySprite; 

    // --- 능력 슬롯 관련 변수 ---
    [Header("Ability Slot Images")]
    public Image slotQ_Image;
    public Image slotW_Image;
    public Image slotE_Image;
    public Image slotR_Image;

    [Header("Ability Slot Sprites")]
    public Sprite q_Locked;
    public Sprite q_Unlocked;
    public Sprite w_Locked;
    public Sprite w_Unlocked;
    public Sprite e_Locked; // (인스펙터에서 연결 필요)
    public Sprite e_Unlocked; // (인스펙터에서 연결 필요)
    public Sprite r_Locked; // (인스펙터에서 연결 필요)
    public Sprite r_Unlocked; // (인스펙터에서 연결 필요)

    [Header("Ability Highlights (Optional)")]
    public Image q_Highlight; 
    public Image w_Highlight;
    public Image e_Highlight; // (인스펙터에서 하이라이트 이미지 연결)
    public Image r_Highlight; // (인스펙터에서 하이라이트 이미지 연결)

    // --- 다른 스크립트에서 호출할 함수들 ---

    /**
     * 게이지 UI를 현재 게이지 수치에 맞게 업데이트합니다.
     */
    public void UpdateGaugeUI(int currentGauge)
    {
        for (int i = 0; i < gaugeCrystals.Length; i++)
        {
            if (i < currentGauge)
            {
                gaugeCrystals[i].sprite = gaugeFullSprite;
            }
            else
            {
                gaugeCrystals[i].sprite = gaugeEmptySprite;
            }
        }
    }

    /**
     * 특정 능력 슬롯의 아이콘을 (잠금/해제) 상태로 변경합니다.
     */
    public void UnlockAbility(string key, bool isUnlocked)
    {
        switch (key.ToUpper())
        {
            case "Q":
                slotQ_Image.sprite = isUnlocked ? q_Unlocked : q_Locked;
                break;
            case "W":
                slotW_Image.sprite = isUnlocked ? w_Unlocked : w_Locked;
                break;
            case "E":
                slotE_Image.sprite = isUnlocked ? e_Unlocked : e_Locked;
                break;
            case "R":
                slotR_Image.sprite = isUnlocked ? r_Unlocked : r_Locked;
                break;
        }
    }

    /**
     * 플레이어가 키를 누르고 있을 때 슬롯을 하이라이트합니다.
     */
    public void HighlightSlot(string key, bool showHighlight)
    {
        Image targetSlot = null;
        switch (key.ToUpper())
        {
            case "Q": targetSlot = q_Highlight; break;
            case "W": targetSlot = w_Highlight; break;
            case "E": targetSlot = e_Highlight; break;
            case "R": targetSlot = r_Highlight; break;
        }

        if (targetSlot != null)
        {
            targetSlot.gameObject.SetActive(showHighlight);
        }
    }

    // --- ▼ [오류 원인] 이 함수가 UIManager.cs에 누락되어 있습니다. ▼ ---
    /**
     * 조합 능력 사용 시 두 슬롯을 하이라이트합니다.
     */
    public void HighlightCombinedAbility(string key1, string key2, bool highlight)
    {
        HighlightSlot(key1, highlight);
        HighlightSlot(key2, highlight);
        // (추가) 조합 시 게이지 깜빡이는 효과도 여기서 구현 가능
    }
    // --- ▲ ---
}