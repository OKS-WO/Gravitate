using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigerRotate : MonoBehaviour
{
    public GameObject bri;
    public float targetAngle = 90f;    // 최종 목표 Z축 각도
    public float rotationSpeed = 90f;  // 초당 회전 속도 (도/초)
    public Transform rotationPivot; 

    private Rigidbody2D rig;
    private Quaternion initialRotation; // 0도 회전 (초기 위치)
    private Quaternion finalRotation;   // 목표 회전 (targetAngle)
    private Quaternion desiredRotation; // 현재 목표 Quaternion

    // Start is called before the first frame update
    void Start()
    {
        rig = bri.GetComponent<Rigidbody2D>();

        // 초기 0도 회전값(initialRotation)과 목표 90도 회전값(finalRotation)을 미리 계산
        initialRotation = Quaternion.Euler(bri.transform.eulerAngles.x, bri.transform.eulerAngles.y, 0f);
        finalRotation = Quaternion.Euler(bri.transform.eulerAngles.x, bri.transform.eulerAngles.y, targetAngle);

        // 시작 시 목표는 초기 위치
        desiredRotation = initialRotation;

        // ?? 다리 오브젝트의 Rigidbody2D는 Kinematic이어야 합니다.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotationPivot == null)
        {
            Debug.LogError("Rotation Pivot이 할당되지 않았습니다!");
            return;
        }

        // 1. RotateTowards로 새로운 회전 각도를 계산합니다.
        Quaternion newRotation = Quaternion.RotateTowards(
            rig.transform.rotation,
            desiredRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        // 2. 현재 오브젝트 위치와 회전 중심점의 벡터를 계산합니다.
        // ?? 오브젝트의 피벗에서 경첩까지의 상대 벡터를 계산합니다.
        Vector3 offset = rig.transform.position - rotationPivot.position;

        // 3. 이 벡터를 새로운 회전각(newRotation)만큼 회전시킵니다.
        // 이것이 회전 후 오브젝트의 새로운 위치 오프셋이 됩니다.
        Vector3 newOffset = newRotation * Quaternion.Inverse(rig.transform.rotation) * offset;

        // 4. 새로운 위치를 계산합니다. (경첩 위치 + 새로운 오프셋)
        Vector3 newPosition = rotationPivot.position + newOffset;

        // 5. Rigidbody를 새 위치와 회전으로 이동시킵니다.
        rig.MovePosition(newPosition);
        rig.MoveRotation(newRotation);
    }

    // ?? OnCollision 대신 OnTrigger를 권장하며, 여기서는 OnCollision을 유지합니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            desiredRotation = finalRotation; // 목표 회전: 90도
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
            desiredRotation = initialRotation; // 목표 회전: 0도 (원래 위치)
        
    }
}
