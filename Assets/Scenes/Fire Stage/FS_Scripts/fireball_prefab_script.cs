using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;
//using static UnityEditor.PlayerSettings;

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
    void Update() {
        rigid2d.velocity = new Vector3(movespeed, 0, 0);
        limittime -= Time.deltaTime;
        if (limittime < 0) { Destroy(gameObject); } //prevents memory overload
    }

    

    private void OnParticleCollision(GameObject other) {
        //Debug.Log("update:fireball has collisioned with particle");
        if (other.CompareTag("Water") && SteamEffectPreFab != null)  {
            Destroy(gameObject);
            new WaitForEndOfFrame();
            Vector3 pos = gameObject.transform.position;
            for (int i = 0; i < SteamEffectcnt; i++)
                Instantiate(SteamEffectPreFab, pos, Quaternion.Euler(-90f, 0f, 0f), null);
        }
    }

    public GameObject fireboom;

    private void OnCollisionEnter2D(Collision2D collision)  {
        //Debug.Log("update:fireball has collisioned with rigidbody");
        Vector3 pos = gameObject.transform.position;
        if (collision.gameObject.CompareTag("Player")) {
            //Debug.Log("fire + player");
            Destroy(this.gameObject);
            new WaitForEndOfFrame();
            for (int i = 0; i < FireEffectcnt; i++)
                Instantiate(fireboom, pos, Quaternion.Euler(0f, 0f, -90f), null);
        }
        if (collision.gameObject.CompareTag("UnBreakable")) {
            Destroy(this.gameObject);
            new WaitForEndOfFrame();
            for (int i = 0; i < FireEffectcnt; i++)
                Instantiate(fireboom, pos, Quaternion.Euler(0f, 0f, -90f), null);
        }
    }
}
