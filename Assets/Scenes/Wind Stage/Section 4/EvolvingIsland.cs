using UnityEngine;
using System.Collections; 

// Evolving Island 위로 이동(Player와 독립)
public class EvolvingIsland : MonoBehaviour
{
    private int currentState = 1;
    public GameObject assetTilemap1; 
    public GameObject assetTilemap2; 
    public GameObject assetTilemap3; 
    public GameObject assetTilemap4; 

    public bool isLaunched = false;

    private Rigidbody2D rb;

    public float launchSpeed = 15f; // 인스펙터에서 100으로 설정됨

    private GameObject playerOnPlatform = null; 
    private Movement playerMovementScript = null; 
    
    private Rigidbody2D playerRigidbody = null;

    [Header("Obstacle Settings")]
    [Tooltip("활성화시킬 낙석 스포너 오브젝트")]
    public GameObject rockSpawner; 

    [Tooltip("낙석이 떨어지기 시작할 Y 높이")]
    public float rockTriggerHeight = 110f;

    private bool spawnerActivated = false; // 스포너가 한 번만 활성화되도록 함

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
        ChangeState(1); 
    }

    void Update()
    {
        // 섬이 발사되었고, 스포너가 아직 활성화되지 않았다면
        if (isLaunched && !spawnerActivated)
        {
            if (transform.position.y >= rockTriggerHeight)
            {
                if (rockSpawner != null)
                {
                    rockSpawner.SetActive(true);
                }
                spawnerActivated = true;
            }
        }

        if(isLaunched && spawnerActivated)
        {
            if(transform.position.y >= 152) isLaunched = false;
        }
    }

    public bool HandleAbilityHit(string tag)
    {
        if (isLaunched) return false;
        switch (currentState)
        {
            case 1: if (tag == "Rock") { ChangeState(2); return true; } break;
            case 2: if (tag == "Obsidian") { ChangeState(3); return true; } break;
            case 3: if (tag == "Water") { ChangeState(4); return true; } break;
            case 4: if (tag == "Steam") { Launch(); return true; } break;
        }
        return false;
    }

    void ChangeState(int state)
    {
        assetTilemap1.SetActive(false);
        assetTilemap2.SetActive(false);
        assetTilemap3.SetActive(false);
        assetTilemap4.SetActive(false);
        currentState = state;
        if (state == 1) assetTilemap1.SetActive(true);
        if (state == 2) assetTilemap2.SetActive(true);
        if (state == 3) assetTilemap3.SetActive(true);
        if (state == 4) assetTilemap4.SetActive(true);
    }
    
    void Launch()
    {
        if (isLaunched) return;

        isLaunched = true; 
        

        if (playerOnPlatform != null)
        {
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            if (playerRigidbody != null)
            {
                playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
                playerRigidbody.velocity = Vector2.zero; // 기존 속도 제거
            }
            
            playerOnPlatform.transform.SetParent(this.transform);
        }
    }

    private void FixedUpdate()
    {
        if (isLaunched)
        {
            transform.Translate(Vector2.up * launchSpeed * Time.fixedDeltaTime, Space.World);
        }
    }

    // --- 4. OnTriggerEnter/Exit 수정 (플레이어 Rigidbody 참조) ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = other.gameObject;
            playerMovementScript = playerOnPlatform.GetComponent<Movement>();
            // 플레이어의 Rigidbody를 미리 찾아 저장
            playerRigidbody = playerOnPlatform.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLaunched)
            {
                if (playerOnPlatform != null) 
                {
                    playerOnPlatform.transform.SetParent(null);
                }
                
                if (playerMovementScript != null)
                {
                    playerMovementScript.enabled = true;
                }

                if (playerRigidbody != null)
                {
                    playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }

            playerOnPlatform = null;
            playerMovementScript = null;
            playerRigidbody = null; 
        }
    }
}