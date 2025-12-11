using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    [Header("Gauge UI Elements")]
    public Image[] gaugeCrystals; 
    public Sprite gaugeFullSprite;  
    public Sprite gaugeEmptySprite; 

    [Header("Ability Slot Groups")] 
    public UIAblitySlot slotQ; 
    public UIAblitySlot slotW;
    public UIAblitySlot slotE; 
    public UIAblitySlot slotR; 

    [Header("Ability Sprites")]
    public Sprite icon_Soil;   // '흙' 아이콘
    public Sprite icon_Water;  // '물' 아이콘
    public Sprite icon_Fire;   // '불' 아이콘
    public Sprite icon_Air;    // '바람' 아이콘
    // --- ▲ ---

    private Color colorFull = new Color(1f, 1f, 1f, 1f);
    private Color colorDim = new Color(1f, 1f, 1f, 0.2f); 

    public void UpdateGaugeUI(int currentGauge)
    {
        for (int i = 0; i < gaugeCrystals.Length; i++)
        {
            if (i < currentGauge) {
                gaugeCrystals[i].sprite = gaugeFullSprite;
            } else {
                gaugeCrystals[i].sprite = gaugeEmptySprite;
            }
        }
    }

    public void UnlockAbility(string key, bool isUnlocked)
    {
        UIAblitySlot targetSlot = null;
        Sprite targetSprite = null;

        switch (key.ToUpper())
        {
            case "Q": // 흙
                targetSlot = slotQ;
                targetSprite = icon_Air;
                break;
            case "W": // 물
                targetSlot = slotW;
                targetSprite = icon_Soil;
                break;
            case "E": // 불
                targetSlot = slotE;
                targetSprite = icon_Water;
                break;
            case "R": // 바람
                targetSlot = slotR;
                targetSprite = icon_Fire;
                break;
        }

        if (targetSlot != null)
        {
            targetSlot.SetState(isUnlocked, targetSprite);
        }
    }

    public void HighlightSlot(string key, bool showHighlight)
    {
        UIAblitySlot targetSlot = null;
        switch (key.ToUpper())
        {
            case "Q": targetSlot = slotQ; break;
            case "W": targetSlot = slotW; break;
            case "E": targetSlot = slotE; break;
            case "R": targetSlot = slotR; break;
        }

        if (targetSlot != null)
        {
            targetSlot.SetHighlight(showHighlight, colorFull, colorDim);
        }
    }
    public void HighlightCombinedAbility(string key1, string key2, bool highlight)
    {
        HighlightSlot(key1, highlight);
        HighlightSlot(key2, highlight);
    }
}