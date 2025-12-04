using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FireStage_bomb_Script : MonoBehaviour
{
    public int explosiontimeseconds = 3;
    public float explosion_radius = 3.0f;
    public int explosion_particlecnt = 3;
    private float explosiondelta = 0;
    private bool explosionenabled = false;

    public GameObject FireParticle;


    // Start is called before the first frame update
    void Start() { }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            explosionenabled = true;
        }
        else if (other.CompareTag("Water"))
        {
            explosionenabled = false;
        }
    }

    public GameObject Shockwave;
    //private ParticleSystemForceField 

    // Update is called once per frame
    void FixedUpdate() {
        
        if (explosiondelta > explosiontimeseconds)
        {
            Vector2 pos = gameObject.transform.position;
            for (int i = 0; i < explosion_particlecnt; i++)
            {
                Instantiate(FireParticle, pos, Quaternion.Euler(-90f, 0f, 0f));
            }
            Instantiate(Shockwave, pos, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject);
            //new WaitForSecondsRealtime(1.0f);
            
        }
        if (explosionenabled) { explosiondelta += Time.deltaTime; }

    }



    
}
