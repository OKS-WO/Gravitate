using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    [Header("레이저 양 끝 지점")]
    public Transform emitterA; 
    public Transform emitterB; 

    [Header("충돌 오브젝트")]
    [Tooltip("씬에 미리 배치해 둔 'Spike' 태그 오브젝트")]
    public GameObject spikeObject; 

    [Header("타이밍 설정 (초)")]
    public float startDelay = 0f; 
    public float warningTime = 1.0f;  
    public float damageTime = 0.5f;   
    public float offTime = 2.0f;      

    [Header("비주얼 및 설정")]
    public Material warningMaterial; 
    public Material damageMaterial; 

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        if (spikeObject == null)
        {
            Debug.LogError(gameObject.name + ": 'Spike Object'가 연결되지 않았습니다! 인스펙터 창을 확인하세요.");
        }
        else
        {
            spikeObject.SetActive(false); 
        }

        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            UpdateBeamPosition();
            lineRenderer.material = warningMaterial;
            lineRenderer.enabled = true;
            
            if (spikeObject != null) spikeObject.SetActive(false); 
            
            yield return new WaitForSeconds(warningTime);

            lineRenderer.material = damageMaterial;
            
            if (spikeObject != null)
            {
                spikeObject.SetActive(true); 
            }
            
            yield return new WaitForSeconds(damageTime);

            lineRenderer.enabled = false;
            
            if (spikeObject != null)
            {
                spikeObject.SetActive(false); 
            }

            yield return new WaitForSeconds(offTime);
        }
    }
    
    void UpdateBeamPosition()
    {
        lineRenderer.SetPosition(0, emitterA.position);
        lineRenderer.SetPosition(1, emitterB.position);
    }
}