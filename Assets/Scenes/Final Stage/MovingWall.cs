using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0f, 3f, 0f); // 위로 3만큼
    public float speed = 3f;

    bool opened = false;
    Vector3 startPos;
    Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }

    public void Open()
    {
        if (opened) return;   // 한 번만 열리게
        opened = true;
        targetPos = startPos + openOffset; // 위로 이동
    }
}