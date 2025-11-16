using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn_3Stage : MonoBehaviour
{
    public Vector2 respawnPoint;
    public float deathTime = 3.0f;
    public GameObject deathParticle;
    private ElementController_3Stage elementController;
    Movement_3Stage movement;

    private GameObject lastActivatedCheckpoint = null;



    void Start()
    {
        elementController = GetComponent<ElementController_3Stage>();
        movement = GetComponent<Movement_3Stage>();


        if (elementController == null)
        {
            Debug.LogError("PlayerRespawn: ElementController 스크립트를 찾을 수 없습니다!");
        }

        GameObject initialSpawn = GameObject.FindWithTag("CheckPoint"); 
        if (initialSpawn != null)
        {
            respawnPoint = initialSpawn.transform.position;
            // 마지막으로 활성화된 체크포인트
            lastActivatedCheckpoint = initialSpawn; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            if (collision.gameObject == lastActivatedCheckpoint)
            {
                // 같은 체크포인트라면, 게이지 회복 없이 함수를 종료 ( 리스폰 위치는 갱신 )
                respawnPoint = new Vector2(collision.transform.position.x, collision.transform.position.y);
                return; 
            }

            respawnPoint = new Vector2(collision.transform.position.x, collision.transform.position.y);

            lastActivatedCheckpoint = collision.gameObject;

            if (elementController != null)
            {
                elementController.RestoreAllGauge();
            }
        }

        if (collision.CompareTag("DestructibleSpike"))
        {
            movement.playSound("DIE");
            Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity, null);
            StartCoroutine(killTime());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("DestructibleSpike"))
        {
            movement.playSound("DIE");
            Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity, null);
            StartCoroutine(killTime());
        }
    }
    IEnumerator killTime()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<Movement_3Stage>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        yield return new WaitForSeconds(deathTime);

        if (elementController != null)
        {
            elementController.RestoreAllGauge();
        }

        gameObject.transform.position = respawnPoint;
        gameObject.GetComponent<Movement_3Stage>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
