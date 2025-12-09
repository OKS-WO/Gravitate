using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class FireStage_bomb_Script : MonoBehaviour
{
    public int explosiontimeseconds = 3;
    public int explosion_particlecnt = 3;
    private float exptimer = 0;
    private bool explosionenabled = false;

    public GameObject FireParticle;
    public GameObject Shockwave;
    public GameObject Damagezone;
    //public AudioClip boomsound;
    public GameObject boomsound;
   

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

    public Color bombcol = new Color(56, 56, 56, 1);
    public void bomb_colorsetting() {
         //defalut color
        float expremaintime = explosiontimeseconds - exptimer;

        if (expremaintime > 1)
        {
            bombcol.r = 255;
        }
        else if (expremaintime <= 1)
        {
            if (expremaintime <= 1 && expremaintime > 0.9f) {
                bombcol.g = 255; bombcol.b = 255;
            }
            else if (expremaintime <= 0.9f && expremaintime > 0.8f)
            {
                bombcol.g = 56; bombcol.b = 56;
            }
            else if (expremaintime <= 0.8f && expremaintime > 0.7f)
            {
                bombcol.g = 255; bombcol.b = 255;
            }
            else if (expremaintime <= 0.7f && expremaintime > 0.6f)
            {
                bombcol.g = 56; bombcol.b = 56;
            }
            else if (expremaintime <= 0.6f)
            {
                bombcol.g = 255; bombcol.b = 255;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().color = bombcol;
    }
    
  
    //private ParticleSystemForceField 

    // Update is called once per frame
    void FixedUpdate() {
        
        if (exptimer > explosiontimeseconds)
        {
            Vector2 pos = gameObject.transform.position;
            
            for (int i = 0; i < explosion_particlecnt; i++)
            {
                Instantiate(FireParticle, pos, Quaternion.Euler(-90f, 0f, 0f));
                Instantiate(FireParticle, pos, Quaternion.Euler(0f, 0f, 0f));
                
            }
            Instantiate(Shockwave, pos, Quaternion.Euler(0f, 0f, 0f));
            Instantiate(Damagezone, pos, Quaternion.Euler(0f, 0f, 0f));
            Instantiate(boomsound, pos, Quaternion.Euler(0f, 0f, 0f));
            Destroy(gameObject);
            
            FS_CamShake.Myinstance.StartCoroutine(FS_CamShake.Myinstance.CamShake());
            new WaitForSecondsRealtime(FS_CamShake.Myinstance.CameraShakeDuration);
            FS_CamShake.Myinstance.StopCoroutine(FS_CamShake.Myinstance.CamShake());
            FS_CamShake.Myinstance.HardCamPosInit();
        }
        if (explosionenabled) {
            exptimer += Time.deltaTime;
            bomb_colorsetting();
            //gameObject.GetComponent<SpriteRenderer>().color = bombcol;
        }
    }



    
}
