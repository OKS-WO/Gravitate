using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;
    public bool dashReady;

    [Space]

    [Header("Collision")]
    public Vector2 bottomOffset = new Vector2(0f, -0.5f);
    public Vector2 groundBoxSize = new Vector2(0.5f, 0.2f); // 박스의 가로, 세로 크기

    public Vector2 leftOffset = new Vector2(-0.5f, 0f);
    public Vector2 leftBoxSize = new Vector2(0.2f, 0.5f); // 박스의 가로, 세로 크기

    public Vector2 rightOffset = new Vector2(0.5f, 0f);
    public Vector2 rightBoxSize = new Vector2(0.2f, 0.5f); // 박스의 가로, 세로 크기


    void Update()
    {
        // Physics2D.OverlapBox를 사용하여 isGround 변수 업데이트
        isGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundBoxSize, 0f, groundLayer);
        isLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, leftBoxSize, 0f, groundLayer);
        isRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, rightBoxSize, 0f, groundLayer);
        if (isGround||isLeftWall||isRightWall) dashReady = true;
    }

    // 개발 편의를 위해 충돌 감지 범위를 시각적으로 표시
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, groundBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, leftBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, rightBoxSize);
    }
}