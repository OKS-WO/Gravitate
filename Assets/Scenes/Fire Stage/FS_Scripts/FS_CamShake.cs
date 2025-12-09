using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_CamShake : MonoBehaviour
{
    public static FS_CamShake Myinstance;

    private void Awake()
    {
        Myinstance = this;
    }

    
    
    public float CameraShakeDuration = 0.5f; 
    private float CameraShakeTimeDelta = 0;
    public AnimationCurve CamShakeStrengthCurve;
    public float CamShakeInterval = 0.1f;
    public GameObject Player_ForPosCheck;
    //public bool HardCamPosInit = false;
    Vector3 originPos;

    public void HardCamPosInit()
    {
        transform.position = new Vector3(Player_ForPosCheck.transform.position.x, Player_ForPosCheck.transform.position.y,-10);
        CameraShakeTimeDelta = 0;
    }

    public IEnumerator CamShake()
    {
        while (CameraShakeTimeDelta <= CameraShakeDuration)
        {
            originPos = new Vector3(Player_ForPosCheck.transform.position.x, Player_ForPosCheck.transform.position.y, -10);
            CameraShakeTimeDelta += Time.deltaTime;
            float strength = CamShakeStrengthCurve.Evaluate(CameraShakeTimeDelta / CameraShakeDuration);
            gameObject.transform.position = originPos + Random.insideUnitSphere * strength;
            new WaitForSecondsRealtime(CamShakeInterval);
            yield return null;
        }
        CameraShakeTimeDelta = 0;
    }

   
}
