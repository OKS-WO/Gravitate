using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// [NEW] AudioSource 컴포넌트가 없으면 자동으로 추가해줍니다.
[RequireComponent(typeof(AudioSource))]
public class SpaceshipExit : MonoBehaviour
{
    [Header("Settings")]
    public float flySpeed = 5.0f;
    public float flyDuration = 3.0f;
    public string nextSceneName = "TitleScene";

    [Header("Effects")]
    public GameObject engineEffect;

    [Tooltip("발사 시 재생할 효과음")]
    public AudioClip launchSound; // [NEW] 효과음 파일 넣는 곳

    private bool isLaunched = false;
    private AudioSource audioSource; // [NEW] 오디오 재생기

    void Start()
    {
        if (engineEffect != null) engineEffect.SetActive(false);
        
        // [NEW] AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLaunched) return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LaunchSequence(collision.gameObject));
        }
    }

    IEnumerator LaunchSequence(GameObject player)
    {
        isLaunched = true;

        // 카메라 분리
        if (Camera.main != null && Camera.main.transform.parent == player.transform)
        {
            Camera.main.transform.SetParent(null);
        }

        // 플레이어 숨기기
        player.SetActive(false);
        
        // 엔진 이펙트 켜기
        if (engineEffect != null) engineEffect.SetActive(true);

        // --- ▼ [NEW] 사운드 재생 ▼ ---
        if (launchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(launchSound);
        }
        // -----------------------------

        // 위로 날아가기
        float timer = 0f;
        while (timer < flyDuration)
        {
            transform.Translate(Vector3.right * flySpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null; 
        }

        SceneManager.LoadScene(nextSceneName);
    }
}