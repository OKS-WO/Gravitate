using UnityEngine;

public class WeightSensor : MonoBehaviour
{
    public GameObject Bridge;
    public GameObject windZoneObject;
    private string targetTag = "UnBreakable";
    public GameObject activationEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            if (activationEffect != null)
            {
                // 센서의 현재 위치에 파티클 생성
                Instantiate(activationEffect, transform.position, Quaternion.identity);
            }
            // 다리를 활성화
            if (Bridge != null)
            {
                Bridge.SetActive(true);
            }

            // 바람을 비활성화
            if (windZoneObject != null)
            {
                windZoneObject.SetActive(false);
            }

            // 센서가 1회용으로 작동하도록 센서 비활성화
            gameObject.SetActive(false);
        }
    }
}