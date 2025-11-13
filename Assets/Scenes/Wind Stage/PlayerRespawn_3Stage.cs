using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn_3Stage : MonoBehaviour
{
    public Vector2 respawnPoint;
    public float deathTime = 3.0f;
    public GameObject deathParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            respawnPoint = new Vector2(collision.transform.position.x, collision.transform.position.y);
        }

        if (collision.CompareTag("DestructibleSpike"))
        {
            Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity, null);
            StartCoroutine(killTime());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("DestructibleSpike"))
        {
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
        gameObject.transform.position = respawnPoint;
        gameObject.GetComponent<Movement_3Stage>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
