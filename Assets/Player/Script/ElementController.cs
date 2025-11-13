using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public GameObject RockPrefab;
    public GameObject WaterPrefab;
    public GameObject AirPrefab;
    public GameObject FirePrefab;
    public GameObject PreviewPrefab;
    

    // ?? ���� ��ġ �������� Vector2�� ���� (X, Y�� ���)
    private Vector2 currentOffset = new Vector2(0f, 1f); // ���� ��ġ: ������Ʈ �ٷ� �� 1f

    private GameObject currentPreview;
    public bool isSetting = false;

    // ���� �̵� ����
    public float gridUnit = 1f;
    private float currentAngle = 0f;

    public bool soil = false;
    public bool water = false;
    public bool air = false;
    public bool fire = false;

    Movement movement;

	private void Awake()
	{
        movement = GetComponent<Movement>();
	}

	void Update()
    {
        // 1. Q Ű�� ������ ���� (��ġ ��� ����)
        if (Input.GetKeyDown(KeyCode.Q) && soil)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && water)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && air)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && fire)
        {
            if (!isSetting)
            {
                isSetting = true;
                InitializePreview();
            }
        }
        // 2. ��ġ ��� ���� (�̸����� �̵� �� ���� ����)
        if (isSetting)
        {
            // ?? Ű���� �Է¿� ���� ������ ������Ʈ
            HandlePlacementInput();
            HandleRotationInput();

            // ���� ��ġ ���: ��ũ��Ʈ ��ġ + ������
            Vector3 targetPosition = (Vector2)transform.position + currentOffset;

            // �̸����� ������Ʈ ��ġ ������Ʈ
            if (currentPreview != null)
            {
                currentPreview.transform.position = targetPosition;
            }

            // 3. Q Ű�� ���� ���� (������Ʈ ����)
            if (Input.GetKeyUp(KeyCode.Q))
            {
                FinalizePlacementRock(targetPosition);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                FinalizePlacementWater(targetPosition);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                FinalizePlacementFire(targetPosition);
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                FinalizePlacementAir(targetPosition);
            }
        }
    }

    // ---------------------------------------------
    // ?? �̸����� ������Ʈ �ʱ�ȭ �Լ�
    private void InitializePreview()
    {
        // �ʱ� ��ġ�� ������Ʈ �ٷ� �� 1f�� ����
        currentOffset = new Vector2(0f, 1f);

        if (PreviewPrefab != null)
        {
            // ���� �̸����Ⱑ ���ٸ� ����
            if (currentPreview == null)
            {
                currentPreview = Instantiate(PreviewPrefab, Vector3.zero, Quaternion.identity);
            }
            // �̹� �ִٸ� Ȱ��ȭ��
            else
            {
                currentPreview.SetActive(true);
            }
        }
    }

    // ?? Ű���� �Է� ó�� �Լ� (�����¿� ������ ����)
    private void HandlePlacementInput()
    {
        // �����¿� �Է� Ȯ��
        // float xInput = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� �¿� ȭ��ǥ
        // float yInput = Input.GetAxisRaw("Vertical");   // W/S �Ǵ� ���� ȭ��ǥ
        // if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     currentOffset.x += xInput * gridUnit;
        // }
        // if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     currentOffset.y += yInput * gridUnit;
        // }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentOffset.x -= gridUnit;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentOffset.x += gridUnit;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentOffset.y += gridUnit;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentOffset.y -= gridUnit;
        }
    }

    private void HandleRotationInput()
    {
        // GetAxisRaw�� ����ϸ� Ű�� ������ ���� 1, -1, 0 ���� ���� �� �ֽ��ϴ�.
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // ���⿡ ���� ������ �����մϴ�.
        if (yInput < 0) // ���� (W �Ǵ� �� ȭ��ǥ)
        {
            currentAngle = 180f;
        }
        else if (yInput > 0) // �Ʒ��� (S �Ǵ� �Ʒ� ȭ��ǥ)
        {
            currentAngle = 0f; // �Ǵ� 270f
        }
        else if (xInput > 0) // ������ (D �Ǵ� ������ ȭ��ǥ)
        {
            currentAngle = 270f;
        }
        else if (xInput < 0) // ���� (A �Ǵ� ���� ȭ��ǥ)
        {
            currentAngle = 90f;
        }
    }

    // ?? ���� ��ġ �� ���� �Լ�
    private void FinalizePlacementRock(Vector3 finalPosition)
    {
        if (RockPrefab != null)
        {
            movement.playSound("SET");
            Instantiate(RockPrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // ���� ����� ���� �̸����� ��Ȱ��ȭ �� ����
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    private void FinalizePlacementWater(Vector3 finalPosition)
    {
        if (WaterPrefab != null)
        {
            movement.playSound("SET");
            Instantiate(WaterPrefab, finalPosition, Quaternion.Euler(0f, 0f, 0f));
        }

        if (currentPreview != null)
        {
            // ���� ����� ���� �̸����� ��Ȱ��ȭ �� ����
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    private void FinalizePlacementAir(Vector3 finalPosition)
    {
        if (AirPrefab != null)
        {
            movement.playSound("SET");
            Instantiate(AirPrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // ���� ����� ���� �̸����� ��Ȱ��ȭ �� ����
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    private void FinalizePlacementFire(Vector3 finalPosition)
    {
        if (FirePrefab != null)
        {
            movement.playSound("SET");
            Instantiate(FirePrefab, finalPosition, Quaternion.identity);
        }

        if (currentPreview != null)
        {
            // ���� ����� ���� �̸����� ��Ȱ��ȭ �� ����
            Destroy(currentPreview);
            currentPreview = null;
        }

        isSetting = false;
    }

    
}