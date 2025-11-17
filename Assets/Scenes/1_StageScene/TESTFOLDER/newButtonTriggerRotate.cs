using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newButtonTriggerRotate : MonoBehaviour
{
    public LayerMask buttonTriggerLayer = 1;
    public Vector2 bottomOffset = new Vector2(0f, 0f);
    public Vector2 groundBoxSize = new Vector2(1f, 1f); // 박스의 가로, 세로 크기

    public GameObject bri;
    public float targetAngle = 90f;    // 최종 목표 Z축 각도
    public float rotationSpeed = 90f;  // 초당 회전 속도 (도/초)
    public Transform rotationPivot;

    private Rigidbody2D rig;
    private Quaternion initialRotation; // 0도 회전 (초기 위치)
    private Quaternion finalRotation;   // 목표 회전 (targetAngle)
    private Quaternion desiredRotation; // 현재 목표 Quaternion

    public bool isActive;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rig = bri.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        initialRotation = Quaternion.Euler(bri.transform.eulerAngles.x, bri.transform.eulerAngles.y, 0f);
        finalRotation = Quaternion.Euler(bri.transform.eulerAngles.x, bri.transform.eulerAngles.y, targetAngle);

        desiredRotation = initialRotation;

    }

	private void Update()
	{
        isActive = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundBoxSize, 0f, buttonTriggerLayer);

        if (isActive)
        {
            desiredRotation = finalRotation;
        }
        else
        {
            desiredRotation = initialRotation;
        }

        if (anim != null)
            anim.SetBool("isPress", isActive);
    }

	void FixedUpdate()
    {
        if (rotationPivot == null)
        {
            Debug.LogError("Rotation Pivot이 할당되지 않았습니다!");
            return;
        }

        Quaternion newRotation = Quaternion.RotateTowards(
            rig.transform.rotation,
            desiredRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        Vector3 offset = rig.transform.position - rotationPivot.position;

        Vector3 newOffset = newRotation * Quaternion.Inverse(rig.transform.rotation) * offset;

        Vector3 newPosition = rotationPivot.position + newOffset;

        rig.MovePosition(newPosition);
        rig.MoveRotation(newRotation);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, groundBoxSize);
    }
}
