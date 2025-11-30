using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_prefab_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public float movespeed = 0.012f;
    public GameObject SteamEffectPreFab;
    public GameObject FireEffectPreFab;
    public int SteamEffectcnt = 5;
    public int FireEffectcnt = 10;

    // Update is called once per frame
    void Update()
    {
       // Vector3 rotationValue = new Vector3(0, 0, 0.5f);
        transform.Translate(Vector2.right * movespeed);
       // transform.Rotate(rotationValue);
    }

    

    private void OnParticleCollision(GameObject other)
    {
        if (gameObject.CompareTag("Fire"))
        {
            if (other.CompareTag("Water") && SteamEffectPreFab != null)
            {
                Vector3 pos = gameObject.transform.position;
                for (int i=0;i<SteamEffectcnt; i++)
                    Instantiate(SteamEffectPreFab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("update:fireball has collisioned with rigidbody");
        if (this.gameObject.CompareTag("Fire"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.tag = "Spike";
                Debug.Log("fb + pl");
            }
            if (collision.gameObject.CompareTag("UnBreakable"))
            {
                Vector3 pos = gameObject.transform.position;
                Instantiate(FireEffectPreFab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
                Destroy(gameObject);
                Debug.Log("fb + ub");
            }
        }
    }
}
