using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_prefab_script : MonoBehaviour
{
    Rigidbody2D rigid2d;
    //Collision coll2d;
    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {}

    
    public float movespeed = 0;
    public GameObject SteamEffectPreFab;
    public int SteamEffectcnt = 5;
    public int FireEffectcnt = 10;
    public float limittime = 15.0f;

    // Update is called once per frame
    void Update()
    {
        rigid2d.velocity = new Vector3(movespeed, 0, 0);
        limittime -= Time.deltaTime;
        if (limittime < 0) { Destroy(gameObject); } //prevents memory overload
    }

    

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("update:fireball has collisioned with particle");
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
                //Instantiate(FireEffectPreFab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
                Destroy(gameObject);
                Debug.Log("fb + ub");
            }
        }
    }
}
