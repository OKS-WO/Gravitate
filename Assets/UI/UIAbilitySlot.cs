using UnityEngine;
using UnityEngine.UI;

// 이 스크립트를 'Slot_Q', 'Slot_W', 'Slot_E', 'Slot_R' (Empty 오브젝트)에 붙여넣습니다.
public class UIAblitySlot : MonoBehaviour
{
    [Header("연결 (자식 오브젝트)")]
    public Image backgroundImage; // 'Background' (원형 슬롯) 이미지
    public Image iconImage;       // 'Icon' (원소 아이콘) 이미지
    public Image lockedImage;     // 'Locked' (잠금 슬롯) 이미지
    
    // --- ▼ [NEW] 키 라벨 그룹 변수 추가 ▼ ---
    [Tooltip("Q,W,E,R 글자가 포함된 작은 원슬롯 그룹")]
    public GameObject keyLabelGroup; // 'Key_Label_Group' 오브젝트 연결
    // --- ▲ ---

    private bool isUnlocked = false; // 현재 해금 상태

    void Start()
    {
        // 시작 시 초기 상태 (잠금)로 설정
        SetState(false, null); 
    }

    // UIManager가 호출할 함수
    public void SetState(bool unlocked, Sprite elementIcon)
    {
        isUnlocked = unlocked;

        if (isUnlocked)
        {
            // [능력이 있을 때]
            backgroundImage.gameObject.SetActive(true); // 원형 배경 보이기
            iconImage.gameObject.SetActive(true);     // 원소 아이콘 보이기
            lockedImage.gameObject.SetActive(false);  // 잠금 슬롯 숨기기
            
            // --- ▼ [NEW] 키 라벨 그룹 보이기 ▼ ---
            if (keyLabelGroup != null)
            {
                keyLabelGroup.SetActive(true);
            }
            // --- ▲ ---

            iconImage.sprite = elementIcon; // 원소 아이콘 이미지 변경
        }
        else
        {
            // [능력이 없을 때]
            backgroundImage.gameObject.SetActive(false); // 원형 배경 숨기기
            iconImage.gameObject.SetActive(false);     // 원소 아이콘 숨기기
            lockedImage.gameObject.SetActive(true);    // 잠금 슬롯 보이기
            
            // --- ▼ [NEW] 키 라벨 그룹 숨기기 ▼ ---
            if (keyLabelGroup != null)
            {
                keyLabelGroup.SetActive(false);
            }
            // --- ▲ ---
        }
    }

    // (SetHighlight 함수는 기존과 동일)
    public void SetHighlight(bool highlight, Color fullColor, Color dimColor)
    {
        if (isUnlocked)
        {
            iconImage.color = highlight ? dimColor : fullColor;
        }
    }
}