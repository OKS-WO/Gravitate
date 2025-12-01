using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FireStage_bomb_Script : MonoBehaviour
{
    public int explosiontimeseconds = 3;
    public float explosion_radius = 3.0f;
    private float explosiontick;
    private bool explosionenabled = false;

    public GameObject FireParticle;


    // Start is called before the first frame update
    void Start() { explosiontick = explosiontimeseconds * 60; ; }

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

    private void Boom()
    {
        Vector2 pos = gameObject.transform.position;for (int i = 0; i < 10; i++)
        {
            Instantiate(FireParticle, pos, Quaternion.Euler(-90f, 0f, 0f));
        }
        Instantiate(Shockwave, pos, Quaternion.Euler(-90f, 0f, 0f));
        new WaitForSecondsRealtime(1.0f);
        //Destroy(Shockwave);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (explosionenabled)
        {
            explosiontick -= 1;
        }
        if (explosiontick < 0)
        {
            Boom();
        }
    }



    
}
